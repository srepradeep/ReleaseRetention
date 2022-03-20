using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseRetention.Models
{
    public class EnvironmentEx
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public EnvironmentEx(string id,string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Project(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
    public class Deployment
    {
        public string Id { get; set; }
        public string ReleaseId { get; set; }
        public string EnvironmentId { get; set; }
        public DateTime DeployedAt { get; set; }
        public Deployment(string id, string releaseId, string environmentId,DateTime deployedAt)
        {
            this.Id= id;
            this.ReleaseId= releaseId;
            this.EnvironmentId= environmentId;  
            this.DeployedAt= deployedAt;
        }
    }
    public class Release
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Version { get; set; }
        public DateTime Created { get; set; }

        public Release(string id,string projectId,string version,DateTime created)
        {
            this.Id = id;
            this.ProjectId = projectId;
            this.Version= version;
            this.Created = created;
        }
    }
}
