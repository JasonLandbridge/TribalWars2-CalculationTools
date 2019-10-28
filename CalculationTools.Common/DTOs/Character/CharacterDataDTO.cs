using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CalculationTools.Common
{
    public class CharacterDataDTO : ICharacterData
    {
        [JsonProperty("gold_coins")]
        public int GoldCoins { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("victory_points")]
        public int VictoryPoints { get; set; }

        [JsonProperty("tribe_id")]
        public int TribeId { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("tutorial")]
        public int Tutorial { get; set; }

        [JsonProperty("time_last_restart")]
        public int TimeLastRestart { get; set; }

        [JsonProperty("game_over")]
        public bool GameOver { get; set; }

        [JsonProperty("login_amount")]
        public int LoginAmount { get; set; }

        [JsonProperty("email_confirmed")]
        public bool EmailConfirmed { get; set; }

        [JsonProperty("new_messages")]
        public int NewMessages { get; set; }

        [JsonProperty("new_message_ids")]
        public IList<int> NewMessageIds { get; set; }

        [JsonProperty("new_reports")]
        public int NewReports { get; set; }


        [JsonIgnore]
        public IList<IReport> NewReportIds => NewReportIdsDTO.ToList<IReport>();

        [JsonProperty("new_report_ids")]
        public List<ReportDTO> NewReportIdsDTO { get; set; }

        [JsonProperty("new_posts")]
        public int NewPosts { get; set; }

        [JsonProperty("new_thread_ids")]
        public IList<int> NewThreadIds { get; set; }



        [JsonProperty("villages")]
        public List<VillageDTO> VillagesDTO { get; set; }



        [JsonProperty("chapel_village")]
        public int ChapelVillage { get; set; }

        [JsonProperty("hasChapel")]
        public bool HasChapel { get; set; }

        [JsonProperty("chapel_in_queue")]
        public bool ChapelInQueue { get; set; }

        [JsonProperty("tribe_rights")]
        public IList<string> TribeRights { get; set; }

        [JsonProperty("has_second_village")]
        public bool HasSecondVillage { get; set; }

        [JsonIgnore]
        public IList<IVillage> Villages
        {
            get => VillagesDTO.ToList<IVillage>();
            set => VillagesDTO = new List<VillageDTO>((IEnumerable<VillageDTO>)value);
        }
    }
}
