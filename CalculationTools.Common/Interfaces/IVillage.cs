namespace CalculationTools.Common
{
    public interface IVillage
    {
        int Id { get; set; }
        string Name { get; set; }
        int X { get; set; }
        int Y { get; set; }
        int? CharacterId { get; set; }
        string WorldId { get; set; }
    }
}