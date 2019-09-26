using ClassLibrary.ViewModels;

namespace ClassLibrary.Class
{
    public class BattleResultValue : BaseViewModel
    {
        public int Value { get; set; }

        public BattleResultValue(int value)
        {
            Value = value;
        }
    }
}
