using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CalculationTools.Common
{
    public class LoginDataDTO : ILoginData
    {

        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("token")]
        public string AccessToken { get; set; }

        [JsonIgnore]
        public IList<ICharacter> Characters => CharactersDTO.ToList<ICharacter>();

        [JsonProperty("characters")]
        public List<CharacterDTO> CharactersDTO { get; set; }

        [JsonIgnore]
        public IList<IWorld> Worlds => WorldsDTO.ToList<IWorld>();

        [JsonProperty("worlds")]
        public List<WorldDTO> WorldsDTO { get; set; }

        [JsonProperty("invitations")]
        public IList<object> Invitations { get; set; }

        [JsonProperty("premium")]
        public int Premium { get; set; }

        [JsonProperty("server_timestamp")]
        public int ServerTimestamp { get; set; }

        [JsonProperty("first_login")]
        public bool FirstLogin { get; set; }

        [JsonProperty("is_guest")]
        public bool IsGuest { get; set; }

        [JsonProperty("vip")]
        public bool Vip { get; set; }

        [JsonProperty("accepted_adjust")]
        public bool AcceptedAdjust { get; set; }

        [JsonProperty("accepted_pixels")]
        public bool AcceptedPixels { get; set; }

        [JsonProperty("newsletter_window")]
        public bool NewsletterWindow { get; set; }

    }
}
