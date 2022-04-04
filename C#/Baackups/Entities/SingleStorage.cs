using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;
using NUnit.Framework;

namespace BackupsExtra.Entities
{
    public class SingleStorage : IArchivator
    {
        public List<Storage> Run(IRepo repo, List<JobObject> jobObjects)
            {
                var storages = new List<Storage>();
                var paths = jobObjects.Select(jobObject => jobObject.Path).ToList();
                storages.Add(new Storage(paths, jobObjects));
                return storages;
            }
        }
}