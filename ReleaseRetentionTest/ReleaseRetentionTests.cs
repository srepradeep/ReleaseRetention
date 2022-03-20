using Xunit;
using ReleaseRetention.Models;
using ReleaseRetention.Services;
using NSubstitute;
using System.Collections.Generic;
using System;

namespace ReleaseRetentionTests
{
    public class GIVERN_a_ReleaseRetentionFinderService
    {
        [Fact]
        public void WHEN_a_single_release_deployed_to_one_environemnt_and_keep_one_release_THEN_should_return_one_release()
        {
            var project1= new Project("Project-1", "Random Quotes");
            var environment1 = new EnvironmentEx("Environment-1", "Staging");
            var release1 = new Release("Release-1", "Project-1", "1.0.0", new DateTime(2000, 01, 01, 09, 0, 0));
            var deployment1 = new Deployment("Deployment-1", "Release-1", "Environment-1", new DateTime(2000, 01, 01, 10, 0, 0));


            var dataSvc = Substitute.For<MockDataSvc>(new List<EnvironmentEx> { environment1 },
                                                      new List<Project> { project1 },
                                                      new List<Release> { release1 },
                                                      new List<Deployment> { deployment1 });

            ReleaseFinderSvc service = Substitute.For<ReleaseFinderSvc>(dataSvc);

            var expectedResult = new List<string>() { "Release-1" };
            var actualResult = service.FindReleases(1);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void WHEN_2_release_one_project_deployed_to_same_environment_and_keep_one_release_THEN_should_return_one_release()
        {
            var project1 = new Project("Project-1", "Random Quotes");
            var environment1 = new EnvironmentEx("Environment-1", "Staging");
            var environment2 = new EnvironmentEx("Environment-2", "Production");
            var release1 = new Release("Release-1", "Project-1", "1.0.0", new DateTime(2000, 01, 01, 09, 0, 0));
            var release2 = new Release("Release-2", "Project-1", "1.0.1", new DateTime(2000, 01, 02, 09, 0, 0));
            var deployment1 = new Deployment("Deployment-1", "Release-1", "Environment-1", new DateTime(2000, 01, 01, 10, 0, 0));
            var deployment2 = new Deployment("Deployment-2", "Release-2", "Environment-1", new DateTime(2000, 01, 02, 10, 0, 0));


            var dataSvc = Substitute.For<MockDataSvc>(new List<EnvironmentEx> { environment1, environment2 },
                                                      new List<Project> { project1 },
                                                      new List<Release> { release1,release2 },
                                                      new List<Deployment> { deployment1,deployment2 });

            ReleaseFinderSvc service = Substitute.For<ReleaseFinderSvc>(dataSvc);

            var expectedResult = new List<string>() { "Release-2" };
            var actualResult = service.FindReleases(1);

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void WHEN_2_release_one_project_deployed_to_different_environment_and_keeping_one_release_THEN_should_return_two_release()
        {
            var project1 = new Project("Project-1", "Random Quotes");
            var environment1 = new EnvironmentEx("Environment-1", "Staging");
            var environment2 = new EnvironmentEx("Environment-2", "Production");
            var release1 = new Release("Release-1", "Project-1", "1.0.0", new DateTime(2000, 01, 01, 09, 0, 0));
            var release2 = new Release("Release-2", "Project-1", "1.0.1", new DateTime(2000, 01, 02, 09, 0, 0));
            var deployment1 = new Deployment("Deployment-1", "Release-1", "Environment-1", new DateTime(2000, 01, 01, 10, 0, 0));
            var deployment2 = new Deployment("Deployment-2", "Release-2", "Environment-2", new DateTime(2000, 01, 02, 10, 0, 0));


            var dataSvc = Substitute.For<MockDataSvc>(new List<EnvironmentEx> { environment1, environment2 },
                                                      new List<Project> { project1 },
                                                      new List<Release> { release1, release2 },
                                                      new List<Deployment> { deployment1, deployment2 });

            ReleaseFinderSvc service = Substitute.For<ReleaseFinderSvc>(dataSvc);

            var expectedResult = new List<string>() { "Release-2", "Release-1" };
            var actualResult = service.FindReleases(1);

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void WHEN_2_release_with_2_project_and_one_deployed_to_same_environment_and_keeping_one_release_THEN_should_return_one_release()
        {
            var project1 = new Project("Project-1", "Random Quotes");
            var project2 = new Project("Project-2", "Pet Shop");
            var environment1 = new EnvironmentEx("Environment-1", "Staging");
            var environment2 = new EnvironmentEx("Environment-2", "Production");
            var release1 = new Release("Release-1", "Project-2", "1.0.0", new DateTime(2000, 01, 01, 09, 0, 0));
            var release2 = new Release("Release-2", "Project-2", "1.0.0", new DateTime(2000, 01, 02, 09, 0, 0));
            var deployment1 = new Deployment("Deployment-1", "Release-1", "Environment-1", new DateTime(2000, 01, 01, 10, 0, 0));
            var deployment2 = new Deployment("Deployment-2", "Release-2", "Environment-1", new DateTime(2000, 01, 02, 10, 0, 0));


            var dataSvc = Substitute.For<MockDataSvc>(new List<EnvironmentEx> { environment1, environment2 },
                                                      new List<Project> { project1, project2 },
                                                      new List<Release> { release1, release2 },
                                                      new List<Deployment> { deployment1, deployment2 });

            ReleaseFinderSvc service = Substitute.For<ReleaseFinderSvc>(dataSvc);

            var expectedResult = new List<string>() { "Release-2"};
            var actualResult = service.FindReleases(1);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void WHEN_2_release_with_2_project_and_one_deployed_to_different_environment_and_keeping_one_release_THEN_should_return_two_release()
        {
            var project1 = new Project("Project-1", "Random Quotes");
            var project2 = new Project("Project-2", "Pet Shop");
            var environment1 = new EnvironmentEx("Environment-1", "Staging");
            var environment2 = new EnvironmentEx("Environment-2", "Production");
            var release1 = new Release("Release-1", "Project-2", "1.0.0", new DateTime(2000, 01, 01, 09, 0, 0));
            var release2 = new Release("Release-2", "Project-2", "1.0.0", new DateTime(2000, 01, 02, 09, 0, 0));
            var deployment1 = new Deployment("Deployment-1", "Release-1", "Environment-1", new DateTime(2000, 01, 01, 10, 0, 0));
            var deployment2 = new Deployment("Deployment-2", "Release-2", "Environment-2", new DateTime(2000, 01, 02, 10, 0, 0));


            var dataSvc = Substitute.For<MockDataSvc>(new List<EnvironmentEx> { environment1, environment2 },
                                                      new List<Project> { project1, project2 },
                                                      new List<Release> { release1, release2 },
                                                      new List<Deployment> { deployment1, deployment2 });

            ReleaseFinderSvc service = Substitute.For<ReleaseFinderSvc>(dataSvc);

            var expectedResult = new List<string>() { "Release-2", "Release-1" };
            var actualResult = service.FindReleases(1);

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void WHEN_2_release_with_2_projects_and_each_deployed_to_same_environment_and_keeping_one_release_THEN_should_return_two_release()
        {
            var project1 = new Project("Project-1", "Random Quotes");
            var project2 = new Project("Project-2", "Pet Shop");
            var environment1 = new EnvironmentEx("Environment-1", "Staging");
            var environment2 = new EnvironmentEx("Environment-2", "Production");
            var release1 = new Release("Release-1", "Project-1", "1.0.0", new DateTime(2000, 01, 01, 09, 0, 0));
            var release2 = new Release("Release-2", "Project-2", "1.0.0", new DateTime(2000, 01, 02, 09, 0, 0));
            var deployment1 = new Deployment("Deployment-1", "Release-1", "Environment-1", new DateTime(2000, 01, 01, 10, 0, 0));
            var deployment2 = new Deployment("Deployment-2", "Release-2", "Environment-1", new DateTime(2000, 01, 02, 10, 0, 0));


            var dataSvc = Substitute.For<MockDataSvc>(new List<EnvironmentEx> { environment1, environment2 },
                                                      new List<Project> { project1, project2 },
                                                      new List<Release> { release1, release2 },
                                                      new List<Deployment> { deployment1, deployment2 });

            ReleaseFinderSvc service = Substitute.For<ReleaseFinderSvc>(dataSvc);

            var expectedResult = new List<string>() { "Release-2", "Release-1" };
            var actualResult = service.FindReleases(1);

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void WHEN_2_release_with_2_projects_and_each_deployed_to_different_environment_and_keeping_one_release_THEN_should_return_two_release()
        {
            var project1 = new Project("Project-1", "Random Quotes");
            var project2 = new Project("Project-2", "Pet Shop");
            var environment1 = new EnvironmentEx("Environment-1", "Staging");
            var environment2 = new EnvironmentEx("Environment-2", "Production");
            var release1 = new Release("Release-1", "Project-1", "1.0.0", new DateTime(2000, 01, 01, 09, 0, 0));
            var release2 = new Release("Release-2", "Project-2", "1.0.0", new DateTime(2000, 01, 02, 09, 0, 0));
            var deployment1 = new Deployment("Deployment-1", "Release-1", "Environment-1", new DateTime(2000, 01, 01, 10, 0, 0));
            var deployment2 = new Deployment("Deployment-2", "Release-2", "Environment-2", new DateTime(2000, 01, 02, 10, 0, 0));


            var dataSvc = Substitute.For<MockDataSvc>(new List<EnvironmentEx> { environment1, environment2 },
                                                      new List<Project> { project1, project2 },
                                                      new List<Release> { release1, release2 },
                                                      new List<Deployment> { deployment1, deployment2 });

            ReleaseFinderSvc service = Substitute.For<ReleaseFinderSvc>(dataSvc);

            var expectedResult = new List<string>() { "Release-2", "Release-1" };
            var actualResult = service.FindReleases(1);

            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void WHEN_2_release_with_2_projects_and_each_deployed_to_same_environment_and_keeping_two_release_THEN_should_return_two_release()
        {
            var project1 = new Project("Project-1", "Random Quotes");
            var project2 = new Project("Project-2", "Pet Shop");
            var environment1 = new EnvironmentEx("Environment-1", "Staging");
            var environment2 = new EnvironmentEx("Environment-2", "Production");
            var release1 = new Release("Release-1", "Project-1", "1.0.0", new DateTime(2000, 01, 01, 09, 0, 0));
            var release2 = new Release("Release-2", "Project-1", "1.0.0", new DateTime(2000, 01, 02, 09, 0, 0));
            var deployment1 = new Deployment("Deployment-1", "Release-1", "Environment-1", new DateTime(2000, 01, 01, 10, 0, 0));
            var deployment2 = new Deployment("Deployment-2", "Release-2", "Environment-1", new DateTime(2000, 01, 02, 10, 0, 0));


            var dataSvc = Substitute.For<MockDataSvc>(new List<EnvironmentEx> { environment1, environment2 },
                                                      new List<Project> { project1, project2 },
                                                      new List<Release> { release1, release2 },
                                                      new List<Deployment> { deployment1, deployment2 });

            ReleaseFinderSvc service = Substitute.For<ReleaseFinderSvc>(dataSvc);

            var expectedResult = new List<string>() { "Release-2", "Release-1" };
            var actualResult = service.FindReleases(2);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}