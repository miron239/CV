using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities.Clean
{
    public class ByDateClean : IClean
    {
        private readonly TimeProvider _timeProvider;
        private readonly TimeSpan _differenceInTime;

        public ByDateClean(TimeSpan timeDifference, TimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _differenceInTime = timeDifference;
        }

        public int PointsToClean(List<RestorePoint> restorePoints)
        {
            DateTime edgeTime = _timeProvider.GetUtcNow() - _differenceInTime;
            return restorePoints.Count(point => point.Time < edgeTime);
        }
    }
}