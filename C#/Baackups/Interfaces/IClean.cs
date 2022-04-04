using System.Collections.Generic;
using BackupsExtra.Entities;

namespace BackupsExtra.Interfaces
{
    public interface IClean
    {
        public int PointsToClean(List<RestorePoint> restorePoints);
    }
}