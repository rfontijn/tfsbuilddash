using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TfsBuildMonitor.Core.TFSobjects;

namespace TfsBuildMonitor.Core
{
    public interface ITfsService
    {
        Task<IEnumerable<IGrouping<int, Build>>> GetBuildDefinitionsAsync(string filter = null);
        Task<IEnumerable<IGrouping<int, Build>>> GetBuildsAsync(string ids);
        
    }
}