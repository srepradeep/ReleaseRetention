using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReleaseRetention.Models;

namespace ReleaseRetention.Services
{
    public class ReleaseFinderSvc : IReleaseFinder
    {
        private readonly IDataSvc _dataSvc;

        public ReleaseFinderSvc()
        {
        }
        public ReleaseFinderSvc(IDataSvc dataSvc)
        {
            _dataSvc = dataSvc;
        }
        public List<string> FindReleases(int numberOfRelease)
        {
            List<string> releases = new List<string>();

            var allDeployments = _dataSvc.GetAllDeployments();
            var orderedDeployments = allDeployments.OrderByDescending(x => x.DeployedAt).ToList();

            Dictionary<string, ProjectEnvironment> localDict = new Dictionary<string, ProjectEnvironment>();

            foreach (var deployment in orderedDeployments)
            {
                var environemnt = _dataSvc.GetEnvironement(deployment.EnvironmentId);
                var project = _dataSvc.GetProject(deployment.ReleaseId);
               
                if (environemnt != null && project != null)
                {
                    ProjectEnvironment pEnv = new ProjectEnvironment(project, environemnt);
                    var items = localDict.Values.Where(x => x.Project.Id == project.Id && x.Environment.Id == environemnt.Id).ToList();

                    if (!localDict.ContainsKey(deployment.ReleaseId) && (items.Count < numberOfRelease)) 
                    {
                        Console.WriteLine($"{deployment.ReleaseId}, Project={project.Name} kept because it was the most recently deployed to {environemnt.Name}");
                        localDict.Add(deployment.ReleaseId, pEnv);
                        releases.Add(deployment.ReleaseId);
                    }
                }
            }
            return releases;
        }

        private class ProjectEnvironment
        {

            public Project Project { get; set; }

            public EnvironmentEx Environment { get; set; }

            public ProjectEnvironment(Project project, EnvironmentEx environement)
            {
                Project = project;
                Environment = environement;
            }
        }

    }
}
