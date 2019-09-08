using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace TribalWars2_CalculationTools.ViewModels
{
    public sealed class InputRowHeaderViewModel : PropertyChangedBase
    {
        private string _title = "Default ViewModel first caption";
        private string _imagePath = "";

        public InputRowHeaderViewModel()
        {

        }

        public InputRowHeaderViewModel(string title, string imagePath)
        {
            this._title = title;
            this._imagePath = imagePath;
        }

        public string Title
        {
            get => this._title;

            set
            {
                if (value != this._title)
                {
                    this._title = value;
                    this.NotifyOfPropertyChange(() => this.Title);
                }
            }
        }

        public string ImagePath
        {
            get => this._imagePath;

            set
            {
                if (value != this._imagePath)
                {
                    this._imagePath = value;
                    this.NotifyOfPropertyChange(() => this.ImagePath);
                }
            }
        }
    }
}
