using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RealEstate.Models
{
    public class RealTypeModel : BaseModel
    {
        public RealTypeModel()
        {
            SelectCommand = new Command(() => OnSelectCommand());
        }

        private void OnSelectCommand()
        {
            Selected = !Selected;
            OnPropertyChanged("TextColor");
            OnPropertyChanged("BackgroundColor");
        }

        int id;
        public int Id { get { return id; } set { SetProperty(ref id, value); } }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set
            {
                SetProperty(ref selected, value);
                OnPropertyChanged("TextColor");
                OnPropertyChanged("BackgroundColor");
            }
        }

        public Color TextColor
        {
            get
            {
                if (Selected)
                {
                    return Color.White;
                }
                else
                {
                    return Color.FromHex("#A9A9BA");
                }
            }
        }

        public Color BackgroundColor
        {
            get
            {
                if (Selected)
                {
                    return Color.FromHex("#505050");
                }
                else
                {
                    return Color.FromHex("#F6F6F6");
                }
            }
        }

        public Command SelectCommand { get; }
    }
}
