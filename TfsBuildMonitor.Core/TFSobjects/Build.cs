using System.Collections.Generic;

namespace TfsBuildMonitor.Core.TFSobjects
{
    public class Ref
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Ref self { get; set; }
        public Ref web { get; set; }
        public Ref timeline { get; set; }
    }
    public class CsLinks
    {
        public Ref self { get; set; }
        public Ref changes { get; set; }
        public Ref workItems { get; set; }
        public Ref author { get; set; }
        public Ref checkedInBy { get; set; }
    }

    public class ChangeSet
    {
        public List<object> checkinNotes { get; set; }
        public PolicyOverride policyOverride { get; set; }
        public int changesetId { get; set; }
        public string url { get; set; }
        public Author author { get; set; }
        public CheckedInBy checkedInBy { get; set; }
        public string createdDate { get; set; }
        public string comment { get; set; }
        public Links links { get; set; }
    }
    public class PolicyFailure
    {
        public string message { get; set; }
        public string policyName { get; set; }
    }

    public class PolicyOverride
    {
        public string comment { get; set; }
        public List<PolicyFailure> policyFailures { get; set; }
    }

    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string state { get; set; }
        public int revision { get; set; }
    }
    public class Definition
    {
        public string type { get; set; }
        public int revision { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Project project { get; set; }
    }
    public class Queue
    {
        public object pool { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class RequestedFor
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class RequestedBy
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }
    public class CheckedInBy
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class Author
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class LastChangedBy
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class OrchestrationPlan
    {
        public string planId { get; set; }
    }

    public class Logs
    {
        public int id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Repository
    {
        public string id { get; set; }
        public string type { get; set; }
        public object clean { get; set; }
        public bool checkoutSubmodules { get; set; }
    }
    public class AuthoredBy
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
        public bool isContainer { get; set; }
    }

    public class BuildDefinition
    {
        public string quality { get; set; }
        public AuthoredBy authoredBy { get; set; }
        public Queue queue { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
        public int revision { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Project project { get; set; }
    }


    public partial class Build
    {
        public List<string> tags { get; set; }
        public Links _links { get; set; }
        public int id { get; set; }
        public string url { get; set; }
        public Definition definition { get; set; }
        public string buildNumber { get; set; }
        public Project project { get; set; }
        public string uri { get; set; }
        public string sourceBranch { get; set; }
        public string sourceVersion { get; set; }
        public string status { get; set; }
        public Queue queue { get; set; }
        public string queueTime { get; set; }
        public string priority { get; set; }
        public string startTime { get; set; }
        public string finishTime { get; set; }
        public string reason { get; set; }
        public string result { get; set; }
        public RequestedFor requestedFor { get; set; }
        public RequestedBy requestedBy { get; set; }
        public string lastChangedDate { get; set; }
        public LastChangedBy lastChangedBy { get; set; }
        public string parameters { get; set; }
        public OrchestrationPlan orchestrationPlan { get; set; }
        public Logs logs { get; set; }
        public Repository repository { get; set; }
        public bool keepForever { get; set; }
    }

    public class BuildCollection
    {
        public int count { get; set; }
        public List<Build> value { get; set; }
    }

    public class BuildDefinitionCollection
    {
        public int count { get; set; }
        public List<BuildDefinition> value { get; set; }
    }
}