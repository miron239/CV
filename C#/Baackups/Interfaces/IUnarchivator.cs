using System.Collections.Generic;
using System.Threading.Tasks;
using BackupsExtra.Entities;

namespace BackupsExtra.Interfaces
{
    public interface IUnarchivator
    {
        void Run(IRepo repo, string pathToArchive, string pathToDecompress);
    }
}