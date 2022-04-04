using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class SplitStorages : IArchivator
    {
        public List<Storage> Run(IRepo repo, List<JobObject> jobObjects)
        {
            var storages = new List<Storage>();
            foreach (JobObject jobObject in jobObjects)
            {
                storages.Add(new Storage(new List<string>() { jobObject.Path }, jobObjects));
            }

            return storages;
        }
    }
}