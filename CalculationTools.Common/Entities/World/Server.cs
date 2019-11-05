using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculationTools.Common
{
    public class Server
    {
        public int Id { get; set; }
        public string ServerName { get; set; }
        public string ServerCountryCode { get; set; }
        public ICollection<World> Worlds { get; set; }

        [NotMapped]
        public string ImagePath => $"/Resources/Img/flags/flag_{this.ServerCountryCode}.png";



    }
}
