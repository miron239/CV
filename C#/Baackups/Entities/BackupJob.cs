using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Entities.Logger;
using BackupsExtra.Entities.Logger.File;
using BackupsExtra.Entities.Snapshot;
using BackupsExtra.Interfaces;
using ILogger = NUnit.Framework.Internal.ILogger;

namespace BackupsExtra.Entities
{
    public class BackupJob
    {
        private List<RestorePoint> _restorePoints = new List<RestorePoint>();
        private List<JobObject> _jobObjects;
        private IRepo _repo;
        private IClean _cleanAlgo;
        private IArchivator _iarchivator;
        private ILoggerService<JobObject> _loggerService;
        private BackupsExtra.Entities.Logger.ILogger _logger;
        private IUnarchivator _unarchivator;

        public BackupJob(IRepo repo, List<JobObject> jobObjects, IArchivator iarchivator, IClean cleanAlgo, Logger.ILogger logger, IUnarchivator unarchivator)
        {
            _repo = repo;
            _jobObjects = jobObjects;
            _iarchivator = iarchivator;
            _cleanAlgo = cleanAlgo;
            _logger = logger;
            _loggerService = new LoggerService<JobObject>(_logger);
            _unarchivator = unarchivator;
        }

        public List<RestorePoint> RestorePoints => _restorePoints;

        public void Start(DateTime dateTime)
        {
            List<Storage> storages = _iarchivator.Run(_repo, _jobObjects);
            var restorePoint = new RestorePoint(_restorePoints.Count, storages, dateTime);
            _restorePoints.Add(restorePoint);
            _repo.Run(restorePoint.Storages);
            Clean();
            _loggerService.LogInformation("Restore point added");
        }

        public void AddJobObject(JobObject jobObject)
        {
            _jobObjects.Add(jobObject);
            Clean();
            _loggerService.LogInformation("Job object Added");
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            _jobObjects.Remove(jobObject);
        }

        public void Unarchive(string pathToDecompress)
        {
            foreach (var restorePoint in _restorePoints)
            {
                foreach (var storage in restorePoint.Storages)
                {
                    foreach (var pathToArchive in storage.UnarchivingPaths)
                    {
                        if (pathToArchive != null)
                        {
                            _unarchivator.Run(_repo, pathToArchive, pathToDecompress);
                        }
                    }
                }
            }
        }

        public void StartJobFromArchive()
        {
            var backup = new Snapshot.Snapshot(_restorePoints);
            _restorePoints = backup.RestoreFromArchive();
        }

        public void SaveBackup()
        {
            var backup = new Snapshot.Snapshot(_restorePoints);
            backup.Save(_restorePoints);
        }

        private void Clean()
        {
            if (_iarchivator.GetType() == new SingleStorage().GetType())
            {
                int suggestedForRemoveCount = _cleanAlgo.PointsToClean(_restorePoints);
                int countOfRestorePoints = _restorePoints.Count;
                for (int j = 0; j < suggestedForRemoveCount; j++)
                {
                    _restorePoints.Remove(_restorePoints.Last());
                }
            }

            if (_iarchivator.GetType() == new SplitStorages().GetType())
            {
                int suggestedForRemoveCount = _cleanAlgo.PointsToClean(_restorePoints);
                int countOfRestorePoints = _restorePoints.Count;
                for (int j = 0; j < suggestedForRemoveCount; j++)
                {
                    _restorePoints.Remove(_restorePoints.Last());
                }

                if (suggestedForRemoveCount > 0)
                {
                    _restorePoints.Remove(_restorePoints.Last());
                    List<Storage> storages = _iarchivator.Run(_repo, _jobObjects);
                    var restorePoint = new RestorePoint(_restorePoints.Count, storages, DateTime.Now);
                    _restorePoints.Add(restorePoint);
                }
            }
        }
    }
}