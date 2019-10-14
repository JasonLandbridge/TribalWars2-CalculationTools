using CalculationTools.Common;
using CalculationTools.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculationTools.Data
{
    public class PlayerData : BasePropertyChanged, IPlayerData
    {
        public DateTime LastUpdated { get; set; }
        public DateTime LastUpdatedLoginData { get; set; }
        public bool IsLoggedIn { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }

        private List<ICharacter> Characters { get; set; }
        private List<IWorld> Worlds { get; set; }

        public List<CharacterWorld> CharacterWorlds
        {
            get
            {
                List<CharacterWorld> worldList = Worlds.Select(world => new CharacterWorld(world)).ToList();
                worldList.AddRange(Characters.Select(character => new CharacterWorld(character)));
                return worldList;
            }
        }

        public event EventHandler LoginDataIsUpdated;

        public void SetLoginData(ILoginData loginData)
        {
            if (loginData == null) return;

            Name = loginData.Name;
            PlayerId = loginData.PlayerId;
            // Convert from IList<IWorld> to List<World>

            Worlds = loginData.Worlds?.ToList();
            Characters = loginData.Characters?.ToList();


            LastUpdatedLoginData = DateTime.Now;

            LoginDataIsUpdated.Invoke(this, EventArgs.Empty);

        }


    }
}
