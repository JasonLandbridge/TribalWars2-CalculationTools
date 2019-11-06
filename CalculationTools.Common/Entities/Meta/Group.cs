namespace CalculationTools.Common
{
    public class Group : IGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Icon { get; set; }
        public Character Character { get; set; }
        public int CharacterId { get; set; }
    }
}
