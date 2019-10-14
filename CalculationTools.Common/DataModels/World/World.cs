namespace CalculationTools.Common
{
    public class World : IWorld
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool Full { get; set; }

        public int Recommended { get; set; }

        public bool KeyRequired { get; set; }

    }
}
