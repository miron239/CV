using System.Collections.Generic;
using System.Threading.Tasks;
using BackupsExtra.Entities;

namespace BackupsExtra.Interfaces
{
    public interface IRepo
    {
        void Run(List<Storage> storages);
        void Remove(RestorePoint restorePoint);
    }
}