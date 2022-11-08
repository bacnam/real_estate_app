using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RealEstate.Models
{
    public class DirectionModel : BaseModel
    {
        public DirectionModel()
        {
            SelectCommand = new Command(() => OnSelectCommand());
        }

        long id;
        public long Id { get { return id; } set { SetProperty(ref id, value); } }

        int order;
        public int Sort { get { return order; } set { SetProperty(ref order, value); } }

        string name;
        public string Name { get { return name; } set { SetProperty(ref name, value); } }

        private void OnSelectCommand()
        {
            Selected = !Selected;
            OnPropertyChanged("TextColor");
            OnPropertyChanged("BackgroundColor");
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
