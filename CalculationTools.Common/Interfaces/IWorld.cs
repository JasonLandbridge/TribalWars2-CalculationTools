namespace CalculationTools.Common
{
    public interface IWorld
    {
        string WorldId { get; set; }
        string Name { get; set; }
        bool Full { get; set; }
    }
}