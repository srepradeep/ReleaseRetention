using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReleaseRetention.Models;

namespace ReleaseRetention.Services
{
    public interface IReleaseFinder
    {
        List<string> FindReleases(int numberOfRelease);
    }
}
