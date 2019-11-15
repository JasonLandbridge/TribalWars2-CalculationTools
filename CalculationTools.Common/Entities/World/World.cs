using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculationTools.Common
{
    public class World : IWorld
    {
        /// <summary>
        /// The 2 character and 2 number world code, e.g. en43, nl23 etc
        /// </summary>
        public string WorldId { get; set; }

        /// <summary>
        /// Every world has a custom given name based on an alphabetical order. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If true then this world is has reached the maximum amount of players and cannot be started on. 
        /// </summary>
        public bool Full { get; set; }

        public bool Maintenance { get; set; }

        public int Recommended { get; set; }

        public bool KeyRequired { get; set; }

        #region Relationships

        /// <summary>
        /// The server this world belongs to, this is language based. 
        /// </summary>
        public Server OnServer { get; set; }
        public string OnServerId { get; set; }

        /// <summary>
        /// The currently used characters on this world. If null then this world has not been started yet by any accounts.
        /// </summary>
        public ICollection<Character> Characters { get; set; }
        #endregion



        #region NotMapped

        [NotMapped]
        public string FullWorldName
        {
            get
            {
                string name = $"({WorldId}) - {Name}";
                if (Full)
                {
                    name += " (FULL)";
                }
                return name;
            }
        }
        #endregion

    }


}
