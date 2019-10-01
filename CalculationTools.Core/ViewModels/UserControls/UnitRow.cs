using CalculationTools.Core.Base;

namespace CalculationTools.Core
{
    public class UnitRow : BaseViewModel
    {
        public string ImagePath { get; set; }

        public string Name { get; set; }

        public string Title => Name;
    }
}
