using System;
using System.Collections.Generic;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities.Clean
{
    public class NumberOfRestorePointsClean : IClean
    {
        private uint _maxOfPoints;

        public NumberOfRestorePointsClean(uint maxPoints)
        {
            _maxOfPoints = maxPoints;
        }

        public int PointsToClean(List<RestorePoint> restorePoints) => (int)Math.Max(0, restorePoints.Count - _maxOfPoints);
    }
}