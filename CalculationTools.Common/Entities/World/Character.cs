using System.Collections.Generic;

namespace CalculationTools.Common
{
    /// <summary>
    /// The playable characters by the user
    /// </summary>
    public class Character : BasePropertyChanged, ICharacter
    {
        /// <summary>
        /// Id is not the same as the TW2 in-game id, see <see cref="CharacterId"/> for that.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The in-game TW2 character id
        /// </summary>
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int CharacterOwnerId { get; set; }
        public string CharacterOwnerName { get; set; }
        public bool AllowLogin { get; set; }

        #region Relationships

        public Account AccountOwner { get; set; }

        public int? AccountOwnerId { get; set; }

        public World World { get; set; }

        public string WorldId { get; set; }

        public ICollection<Group> Groups { get; set; }
        public ICollection<Village> Villages { get; set; }

        #endregion

        public string FullWorldName => World?.FullWorldName;

    }
}
