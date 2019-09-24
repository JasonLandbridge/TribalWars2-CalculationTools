namespace ClassLibrary.Class
{
    public class UnitRow
    {
        public string ImagePath { get; set; }

        public string Name { get; set; }

        public string Title => Name;
    }
}
