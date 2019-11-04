namespace CalculationTools.Common
{
    public interface IGroup
    {
        int Id { get; set; }
        string Name { get; set; }
        int Icon { get; set; }
        int CharacterId { get; set; }
    }
}