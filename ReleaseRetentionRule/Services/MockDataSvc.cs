using Newtonsoft.Json;
using ReleaseRetention.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseRetention.Services
{
    public class MockDataSvc : IDataSvc
    {
        public List<EnvironmentEx> Environments;

        public List<Project> Projects;

        public List<Deployment>  Deployments;

        public List<Release> Releases;

        public MockDataSvc()
        {
        }

        public MockDataSvc(List<EnvironmentEx> environments, List<Project> projects, List<Release> releases,List<Deployment> deployments)
        {
            this.Environments = environments;
            this.Releases = releases;
            this.Projects = projects;
            this.Deployments = deployments;
        }

        public List<Deployment> GetAllDeployments()
        {
            return Deployments;
        }

        public List<EnvironmentEx> GetAllEnvironments()
        {
            return Environments;
        }

        public List<Project> GetAllProjects()
        {
            return Projects;
        }

        public List<Release> GetAllRelease()
        {
            return Releases;
        }

        public EnvironmentEx GetEnvironement(string environment)
        {
            var envFound = Environments.FirstOrDefault(e => e.Id == environment);
            return envFound;
        }

        public Project GetProject(string releaseId)
        {
            var release = Releases.FirstOrDefault(r => r.Id == releaseId);
            var project = Projects.FirstOrDefault(p => p.Id == release.ProjectId);
            return project;
        }
        public void LoadAllData()
        {
            try
            {
                var baseDirectory = System.Environment.CurrentDirectory;

                var releasesDataFile = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\Data\Releases.json"));
                var deploymentDataFile = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\Data\Deployments.json"));
                var projectsDataFile = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\Data\Projects.json"));
                var environmentDataFile = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\Data\Environments.json"));


                var releases = JsonConvert.DeserializeObject<List<Release>>(File.ReadAllText(releasesDataFile));
                var deployments = JsonConvert.DeserializeObject<List<Deployment>>(File.ReadAllText(deploymentDataFile));
                var projects = JsonConvert.DeserializeObject<List<Project>>(File.ReadAllText(projectsDataFile));
                var environments = JsonConvert.DeserializeObject<List<EnvironmentEx>>(File.ReadAllText(environmentDataFile));

                if (releases != null)
                {
                    Releases = releases;
                }
                if (deployments != null)
                {
                    Deployments = deployments;
                }
                if (projects != null)
                {
                    Projects = projects;
                }
                if (environments != null)
                {
                    Environments = environments;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}
