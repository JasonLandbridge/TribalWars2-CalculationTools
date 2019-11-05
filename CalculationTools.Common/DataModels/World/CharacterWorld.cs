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
        private string _characterWorldWorldCode = string.Empty;

        #endregion


        public CharacterWorld()
        {

        }

        public CharacterWorld(IWorld world)
        {
            // WorldCode = world.Id;
            Name = world.Name;
            Full = world.Full;
        }

        public CharacterWorld(ICharacter character)
        {
            WorldCode = character.WorldId;
            CharacterId = character.CharacterId;
            CharacterName = character.CharacterName;
            CharacterOwnerId = character.CharacterOwnerId;
            CharacterOwnerName = character.CharacterOwnerName;
            AllowLogin = character.AllowLogin;
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

        public string WorldCode { get => _characterWorldWorldCode; set => _characterWorldWorldCode = value; }

        public string Name { get => _characterWorldName; set => _characterWorldName = value; }
        public bool Full { get; set; }

        #endregion

        #region ICharacter Properties
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string WorldId { get; set; }
        public int CharacterOwnerId { get; set; }
        public string CharacterOwnerName { get; set; }
        public bool AllowLogin { get; set; }

        public string WorldName { get => _characterWorldName; set => _characterWorldName = value; }

        #endregion



        #endregion


    }
}
