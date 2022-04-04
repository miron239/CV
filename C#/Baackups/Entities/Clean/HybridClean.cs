using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;
using Banks.Tools;

namespace BackupsExtra.Entities.Clean
{
    public class HybridClean : IClean
    {
        private readonly List<IClean> _cleanAlgos;
        private bool _trueIfMaximum;
        public HybridClean(List<IClean> cleanAlgos, bool trueIfMaximum)
        {
            _cleanAlgos = cleanAlgos ?? throw new BanksExceptions("Invalid algos");
            _trueIfMaximum = trueIfMaximum;
        }

        public int PointsToClean(List<RestorePoint> restorePoints)
        {
            IEnumerable<int> algoResults = _cleanAlgos.Select(algo => algo.PointsToClean(restorePoints));
            return _trueIfMaximum == true ? algoResults.Max() : algoResults.Min();
        }
    }
}