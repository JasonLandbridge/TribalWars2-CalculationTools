namespace CalculationTools.Common
{
    public class World : IWorld
    {
        /// <summary>
        /// Database row identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The 2 character and 2 number world code, e.g. en43, nl23 etc
        /// </summary>
        public string WorldCode { get; set; }

        /// <summary>
        /// Every world has a custom given name based on an alphabetical order. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If true then this world is has reached the maximum amount of players and cannot be started on. 
        /// </summary>
        public bool Full { get; set; }
        public bool AllowLogin { get; set; }

        public bool Maintenance { get; set; }

        public int Recommended { get; set; }

        public bool KeyRequired { get; set; }

        /// <summary>
        /// The server this world belongs to, this is language based. 
        /// </summary>
        public Server Server { get; set; }

        /// <summary>
        /// The currently used character on this world. If null then this world has not been started yet by the player
        /// </summary>
        public Character Character { get; set; }
    }
}
