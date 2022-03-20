using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReleaseRetention.Models;

namespace ReleaseRetention.Services
{
    public interface IDataSvc
    {
        List<Release> GetAllRelease();

        List<Project> GetAllProjects();

        List<Deployment> GetAllDeployments();

        List<EnvironmentEx> GetAllEnvironments();
        Project GetProject(string release);
        EnvironmentEx GetEnvironement(string environment);
    }
}
