using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class UnArchivateWithNoFileSystem : IUnarchivator
    {
        public void Run(IRepo repo, string pathToArchive, string pathToDecompress)
        {
        }
    }
}