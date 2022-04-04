using System.Collections.Generic;
using System.Threading.Tasks;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class RepoWithoutFileSystem : IRepo
    {
        public void Run(List<Storage> storages)
        {
        }

        public void Remove(RestorePoint restorePoint)
        {
        }
    }
}