using System;
using System.IO;
using System.IO.Compression;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class UnArchivedWithFileSystemPath : IUnarchivator
    {
        public void Run(IRepo repo, string pathToArchive, string pathToDecompress)
        {
            string fileRes = pathToArchive;
            string folderDest = pathToDecompress;
            if (!File.Exists(fileRes)) return;
            FileStream fs = null;
            FileStream rs = null;
            try
            {
                string fileName = fileRes.Split('\\')[fileRes.Split('\\').Length - 1];
                string name = folderDest + '/' + Guid.NewGuid() + ".txt";
                string format = ".";
                fs = new FileStream(fileRes, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                int formatLen = fs.ReadByte();
                for (int i = 0; i < formatLen; ++i)
                    format += (char)fs.ReadByte();
                if (File.Exists(name + format))
                    File.Delete(name + format);
                rs = new FileStream(name, FileMode.CreateNew);
                while (fs.Position < fs.Length)
                {
                    int bt = fs.ReadByte();
                    if (bt == 0)
                    {
                        bt = fs.ReadByte();
                        for (int j = 0; j < bt; ++j)
                        {
                            byte b = (byte)fs.ReadByte();
                            rs.WriteByte(b);
                        }
                    }
                    else
                    {
                        int value = fs.ReadByte();
                        for (int j = 0; j < bt; ++j)
                            rs.WriteByte((byte)value);
                    }
                }
            }
            finally
            {
                fs?.Close();
                rs?.Close();
            }
        }
    }
}