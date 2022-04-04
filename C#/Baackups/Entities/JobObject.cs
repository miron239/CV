using System;

namespace BackupsExtra.Entities
{
    public class JobObject
    {
        public JobObject(Guid id, string path)
        {
            Id = id;
            Path = path;
        }

        public Guid Id { get; }

        public string Path { get; }
    }
}