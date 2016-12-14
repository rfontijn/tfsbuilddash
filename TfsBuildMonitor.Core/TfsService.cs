using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TfsBuildMonitor.Core.TFSobjects;

namespace TfsBuildMonitor.Core
{
    public class TfsService : ITfsService
    {
        private readonly string _tfsUrl;
        private readonly ICredentials _credentials;

        public TfsService(string baseUrl, ICredentials credentials)
        {
            _tfsUrl = baseUrl;
            _credentials = credentials;
        }

        public async Task<IEnumerable<IGrouping<int, Build>>> GetBuildDefinitionsAsync(string filter = null)
        {
            var client = new RestClient(_tfsUrl);
            const string requestString = "/build/definitions?api-version=2.0";

            var cancellationTokenSource = new CancellationTokenSource();
            var request = new RestRequest(requestString) { Credentials = _credentials };

            var restResponse = await client.ExecuteGetTaskAsync(request, cancellationTokenSource.Token);
            if (restResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }

            try
            {
                var builds = JsonConvert.DeserializeObject<BuildDefinitionCollection>(restResponse.Content);
                var selectedBuilds = builds.value.Where(b => filter != null && b.name.Contains(filter));

                var buildIds = selectedBuilds.Aggregate("", (current, buildDefinition) => current + (buildDefinition.id + ","));

                var builds2 = GetBuildsAsync(buildIds.TrimEnd(','));

                foreach (var b in builds2.Result)
                {
                    var latestBuild = b.FirstOrDefault();

                    if (latestBuild != null)
                        latestBuild.LastChange = GetChangeSetById(latestBuild.sourceVersion.TrimStart('C')).Result.comment;
                }
                return builds2.Result;
            }
            catch (Exception)
            {
                // TODO
                throw;
            }
        }

        public async Task<ChangeSet> GetChangeSetById(string id)
        {
            var client = new RestClient(_tfsUrl);
            string requestString = $"/tfvc/changesets/{id}?includeDetails=true&api-version=2.0";

            var cancellationTokenSource = new CancellationTokenSource();
            var request = new RestRequest(requestString) { Credentials = _credentials };

            var restResponse = await client.ExecuteGetTaskAsync(request, cancellationTokenSource.Token);

            if (restResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }

            try
            {
                var cs = JsonConvert.DeserializeObject<ChangeSet>(restResponse.Content);
                return cs;
            }
            catch (Exception)
            {
                // TODO
                throw;
            }
        }

        public async Task<IEnumerable<IGrouping<int, Build>>> GetBuildsAsync(string ids)
        {
            var client = new RestClient(_tfsUrl);
            string requestString = $"/build/builds?definitions={ids}&maxBuildsPerDefinition=5&api-version=2.0";

            var cancellationTokenSource = new CancellationTokenSource();
            var request = new RestRequest(requestString) { Credentials = _credentials };

            var restResponse = await client.ExecuteGetTaskAsync(request, cancellationTokenSource.Token);

            if (restResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                return null;
            }

            try
            {
                var build = JsonConvert.DeserializeObject<BuildCollection>(restResponse.Content);
                return build.value.GroupBy(b => b.definition.id);
            }
            catch (Exception)
            {
                // TODO
                throw;
            }
        }
    }
}