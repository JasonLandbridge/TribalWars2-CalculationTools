using System.Collections.Generic;
using CalculationTools.Common;

namespace CalculationTools.Data
{
    public class CharacterData : ICharacterData
    {
        public int GoldCoins { get; set; }
        public int Points { get; set; }
        public int VictoryPoints { get; set; }
        public int TribeId { get; set; }
        public int Rank { get; set; }
        public int Tutorial { get; set; }
        public int TimeLastRestart { get; set; }
        public bool GameOver { get; set; }
        public int LoginAmount { get; set; }
        public bool EmailConfirmed { get; set; }
        public int NewMessages { get; set; }
        public IList<int> NewMessageIds { get; set; }
        public int NewReports { get; set; }
        public IList<IReport> NewReportIds { get; }
        public int NewPosts { get; set; }
        public IList<int> NewThreadIds { get; set; }
        public IList<IVillage> Villages { get; }
        public int ChapelVillage { get; set; }
        public bool HasChapel { get; set; }
        public bool ChapelInQueue { get; set; }
        public IList<string> TribeRights { get; set; }
        public bool HasSecondVillage { get; set; }

        public CharacterData()
        {

        }

        public CharacterData(ICharacterData characterData)
        {

        }
    }
}
