using System.Collections.Generic;
using BackupsExtra.Entities.Logger.File;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities.Snapshot
{
    public interface ISnapshot
    {
        public void Save(List<RestorePoint> restorePoints);
        public List<RestorePoint> RestoreFromArchive();
    }
}