using System.Collections.Generic;

namespace CalculationTools.Common
{
    public interface ICharacterData
    {
        int GoldCoins { get; set; }
        int Points { get; set; }
        int VictoryPoints { get; set; }
        int TribeId { get; set; }
        int Rank { get; set; }
        int Tutorial { get; set; }
        int TimeLastRestart { get; set; }
        bool GameOver { get; set; }
        int LoginAmount { get; set; }
        bool EmailConfirmed { get; set; }
        int NewMessages { get; set; }
        IList<int> NewMessageIds { get; set; }
        int NewReports { get; set; }
        IList<IReport> NewReportIds { get; }
        int NewPosts { get; set; }
        IList<int> NewThreadIds { get; set; }
        IList<IVillage> Villages { get; }
        int ChapelVillage { get; set; }
        bool HasChapel { get; set; }
        bool ChapelInQueue { get; set; }
        IList<string> TribeRights { get; set; }
        bool HasSecondVillage { get; set; }
    }
}