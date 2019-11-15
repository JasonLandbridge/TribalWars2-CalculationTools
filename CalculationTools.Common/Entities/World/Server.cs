using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculationTools.Common
{
    public class Server
    {
        public string Id { get; set; }
        public string ServerName { get; set; }

        #region Relationships
        public ICollection<Account> Accounts { get; set; }
        public ICollection<World> Worlds { get; set; }

        #endregion

        [NotMapped]
        public string ImagePath => $"/Resources/Img/flags/flag_{this.Id}.png";



    }
}
