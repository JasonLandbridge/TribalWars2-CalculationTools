namespace CalculationTools.Common
{
    /// <summary>
    /// In TW2 the world and character class is being used interchangeably.
    /// <para>This class merges those and allows for 1 class to be used</para>
    /// </summary>
    public class CharacterWorld : IWorld, ICharacter
    {
        #region Fields

        private string _characterWorldName = string.Empty;
        private string _characterWorldID = string.Empty;

        #endregion


        public CharacterWorld()
        {

        }

        public CharacterWorld(IWorld world)
        {
            Id = world.Id;
            Name = world.Name;
            Full = world.Full;
        }

        public CharacterWorld(ICharacter character)
        {
            CharacterId = character.CharacterId;
            CharacterName = character.CharacterName;
            CharacterOwnerId = character.CharacterOwnerId;
            CharacterOwnerName = character.CharacterOwnerName;
            AllowLogin = character.AllowLogin;
            WorldId = character.WorldId;
            WorldName = character.WorldName;
        }


        #region Properties

        public string FullWorldName
        {
            get
            {
                string name = $"({WorldId}) - {WorldName}";
                if (Full)
                {
                    name += " (FULL)";
                }
                return name;
            }
        }

        #region IWorld Properties

        public string Id { get => _characterWorldID; set => _characterWorldID = value; }

        public string Name { get => _characterWorldName; set => _characterWorldName = value; }
        public bool Full { get; set; }

        #endregion

        #region ICharacter Properties
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int CharacterOwnerId { get; set; }
        public string CharacterOwnerName { get; set; }
        public bool AllowLogin { get; set; }

        public string WorldId { get => _characterWorldID; set => _characterWorldID = value; }

        public string WorldName { get => _characterWorldName; set => _characterWorldName = value; }

        #endregion



        #endregion


    }
}
