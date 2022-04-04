using System;

namespace BackupsExtra.Entities
{
    public class TimeProvider
    {
        public virtual DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}