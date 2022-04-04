using System;
using System.Collections.Generic;

namespace BackupsExtra.Entities
{
    public class RestorePoint
    {
        private int _nOfFiles;

        public RestorePoint(int nOfFiles, List<Storage> storages, DateTime creationFile)
        {
            _nOfFiles = nOfFiles;
            Time = creationFile;
            Storages = storages;
        }

        public List<Storage> Storages { get; }
        public DateTime Time { get; }
    }
}