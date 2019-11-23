namespace CalculationTools.Common
{
    public class Tribe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Points { get; set; }

        public string Tag { get; set; }


        #region Relationships
        public World World { get; set; }
        public string WorldId { get; set; }
        #endregion



    }


}
