using EntityFrameworkCore.Triggers;
using System;

namespace CalculationTools.Common
{
    public abstract class Trackable
    {
        public virtual DateTime Inserted { get; private set; }
        public virtual DateTime Updated { get; private set; }

        static Trackable()
        {
            Triggers<Trackable>.Inserting += entry => entry.Entity.Inserted = entry.Entity.Updated = DateTime.UtcNow;
            Triggers<Trackable>.Updating += entry => entry.Entity.Updated = DateTime.UtcNow;
        }
    }
}
