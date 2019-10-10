namespace CalculationTools.Core.DataModels.Meta
{
    public class Server
    {
        public string Name => ServerName;

        public string ServerName { get; set; }
        public string ServerCountryCode { get; set; }

        public string ImagePath => $"/Resources/Img/flags/flag_{this.ServerCountryCode}.png";



    }
}
