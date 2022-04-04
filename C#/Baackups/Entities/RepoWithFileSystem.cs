using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class RepoWithFileSystem : IRepo
    {
        public void Run(List<Storage> storages)
        {
            foreach (Storage storage in storages)
            {
                foreach (string path in storage.PathToFiles)
                {
                    string fileDest = "/Users/mironbabicev/Desktop/OOP/" + Guid.NewGuid() + ".rar";
                    storage.UnarchivingPaths.Add(fileDest);
                    string fileRes = path;
                    FileStream fs = null;
                    FileStream rs = null;
                    try
                    {
                        if (File.Exists(fileDest))
                            File.Delete(fileDest);
                        string format = fileRes[(fileRes.LastIndexOf('.') + 1) ..];
                        fs = new FileStream(fileRes, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        rs = new FileStream(fileDest, FileMode.CreateNew);
                        rs.WriteByte((byte)format.Length);
                        foreach (char t in format)
                            rs.WriteByte((byte)t);

                        var bt = new List<byte>();
                        var nBt = new List<byte>();
                        while (fs.Position < fs.Length)
                        {
                            byte b = (byte)fs.ReadByte();
                            if (bt.Count == 0)
                            {
                                bt.Add(b);
                            }
                            else if (bt[^1] != b)
                            {
                                bt.Add(b);
                                if (bt.Count != 255) continue;
                                rs.WriteByte((byte)0);
                                rs.WriteByte((byte)255);
                                rs.Write(bt.ToArray(), 0, 255);
                                bt.Clear();
                            }
                            else
                            {
                                if (bt.Count != 1)
                                {
                                    rs.WriteByte((byte)0);
                                    rs.WriteByte((byte)(bt.Count - 1));
                                    rs.Write(bt.ToArray(), 0, bt.Count - 1);
                                    bt.RemoveRange(0, bt.Count - 1);
                                }

                                bt.Add(b);
                                while ((b = (byte)fs.ReadByte()) == bt[0])
                                {
                                    bt.Add(b);
                                    if (bt.Count != 255) continue;
                                    rs.WriteByte((byte)255);
                                    rs.WriteByte(bt[0]);
                                    bt.Clear();
                                    break;
                                }

                                if (bt.Count <= 0) continue;
                                rs.WriteByte((byte)bt.Count);
                                rs.WriteByte(bt[0]);
                                bt.Clear();
                                bt.Add(b);
                            }
                        }

                        if (bt.Count > 0)
                        {
                            rs.WriteByte((byte)0);
                            rs.WriteByte((byte)bt.Count);
                            rs.Write(bt.ToArray(), 0, bt.Count);
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

        public void Remove(RestorePoint restorePoint)
        {
            foreach (string path in restorePoint.Storages.SelectMany(storage => storage.PathToFiles))
            {
                File.Delete(path);
            }
        }
    }
}