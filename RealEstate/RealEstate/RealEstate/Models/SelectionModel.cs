using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Models
{
    public class SelectionModel : BaseModel
    {
        private long id;
        public long Id
        {
            get
            {
                return id;
            }
            set
            {
                SetProperty(ref id, value);
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);
            }
        }

        private bool selected;
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                SetProperty(ref selected, value);
                OnPropertyChanged("Checkmark");
            }
        }

        public string Checkmark
        {
            get
            {
                return selected ? "checkmark" : "checkmark_none";
            }
        }
    }
}
