using CalculationTools.Common.Data;

namespace CalculationTools.Common
{
    public class VillageGroup : IGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Icon { get; set; }
        public int CharacterId { get; set; }
    }
}
