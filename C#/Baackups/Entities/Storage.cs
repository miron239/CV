using System.Collections.Generic;

namespace BackupsExtra.Entities
{
    public class Storage
    {
        public Storage(List<string> pathToFiles, List<JobObject> archivedFiles)
        {
            PathToFiles = pathToFiles;
            ArchivedFiles = archivedFiles;
            UnarchivingPaths = new List<string>();
        }

        public List<string> UnarchivingPaths { get; set; }
        public List<string> PathToFiles { get; }
        public List<JobObject> ArchivedFiles { get; }
    }
}