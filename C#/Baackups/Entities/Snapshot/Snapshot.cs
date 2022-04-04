using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BackupsExtra.Entities.Snapshot
{
    public class Snapshot : ISnapshot
    {
        private List<RestorePoint> _restorePoints;
        public Snapshot(List<RestorePoint> restorePoints)
        {
            _restorePoints = restorePoints;
        }

        public void Save(List<RestorePoint> restorePoints)
        {
            _restorePoints = restorePoints;

            var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                };
            string snapShotJson = JsonConvert.SerializeObject(_restorePoints, settings);

            using (StreamWriter sw = File.CreateText("/Users/mironbabicev/Desktop/OOP/Compressed/new.txt"))
            {
                sw.WriteLine(snapShotJson);
            }
        }

        public List<RestorePoint> RestoreFromArchive()
        {
            string snapShotJson;
            using (StreamReader sr = File.OpenText("/Users/mironbabicev/Desktop/OOP/Compressed/new.txt"))
            {
                snapShotJson = sr.ReadLine();
            }

            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
            };
            List<RestorePoint> restorePoints = JsonConvert.DeserializeObject<List<RestorePoint>>(snapShotJson, settings);
            return restorePoints;
        }
    }
}