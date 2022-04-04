using System.Collections.Generic;
using BackupsExtra.Entities;

namespace BackupsExtra.Interfaces
{
    public interface IArchivator
    {
        List<Storage> Run(IRepo repo, List<JobObject> jobObjects);
    }
}