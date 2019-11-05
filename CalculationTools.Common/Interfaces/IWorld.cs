namespace CalculationTools.Common
{
    public interface IWorld
    {
        string WorldCode { get; set; }
        string Name { get; set; }
        bool Full { get; set; }
    }
}