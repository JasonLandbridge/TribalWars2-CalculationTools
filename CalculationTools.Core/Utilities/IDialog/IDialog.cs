using System.Windows;

namespace CalculationTools.Core
{
    /// <summary>
    /// This exposes several properties from the WindowClass to allow direct interaction through an interface
    /// <para>Based on: https://www.youtube.com/watch?v=OqKaV4d4PXg </para>
    /// </summary>
    public interface IDialog
    {
        object DataContext { get; set; }
        bool? DialogResult { get; set; }
        Window Owner { get; set; }
        void Close();
        bool? ShowDialog();

    }
}