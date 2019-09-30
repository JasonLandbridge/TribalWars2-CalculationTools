using ClassLibrary.ViewModels;

namespace ClassLibrary.Class
{
    public class UnitRow : BaseViewModel
    {
        public string ImagePath { get; set; }

        public string Name { get; set; }

        public string Title => Name;
    }
}
