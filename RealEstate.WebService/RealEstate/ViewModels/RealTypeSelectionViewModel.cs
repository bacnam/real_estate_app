using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using RealEstate.Models;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class RealTypeSelectionViewModel : BaseViewModel
    {
        public RealTypeSelectionViewModel(SelectionMode selectionMode = SelectionMode.Multiple)
        {
            RealTypes = new ObservableCollection<RealTypeModel>();
            RealTypes.Add(new RealTypeModel() { Id = 1, Title = "Chung cư" });
            RealTypes.Add(new RealTypeModel() { Id = 2, Title = "Nhà riêng" });
            RealTypes.Add(new RealTypeModel() { Id = 3, Title = "Nhà biệt thự, liền kề" });
            RealTypes.Add(new RealTypeModel() { Id = 4, Title = "Nhà mặt phố" });
            RealTypes.Add(new RealTypeModel() { Id = 5, Title = "Đất thường" });
            RealTypes.Add(new RealTypeModel() { Id = 6, Title = "Trang trại, khu nghỉ dưỡng" });
            RealTypes.Add(new RealTypeModel() { Id = 7, Title = "Nhà xưởng" });
            RealTypes.Add(new RealTypeModel() { Id = 8, Title = "Bất động sản khác" });
            RealTypes.Add(new RealTypeModel() { Id = 9, Title = "Tất cả", Selected = true });

            SelectionMode = selectionMode;
        }

        public SelectionMode SelectionMode;

        public void OnSelect(string title)
        {
            if (title == RealTypes.LastOrDefault().Title)
            {
                return;
            }
            if (!IsMultiple)
            {
                foreach (var realType in RealTypes)
                {
                    realType.Selected = false;
                }
            }
            var selected = RealTypes.FirstOrDefault(r => r.Title == title);
            if (selected != null)
            {
                selected.SelectCommand.Execute(null);
                RealTypes.LastOrDefault().Selected = false;
            }
        }

        public void ClearSelected()
        {
            foreach (var item in RealTypes)
            {
                item.Selected = false;
            }
            RealTypes.LastOrDefault().Selected = true;
        }

        public void Select(params RealTypeModel[] reals)
        {
            RealTypes.LastOrDefault().Selected = false;
            if (reals != null)
            {
                var data = from r1 in RealTypes
                           join r2 in reals
                           on r1.Title equals r2.Title
                           select r1;
                foreach (var item in data)
                {
                    item.Selected = true;
                }
            }
        }

        public bool IsMultiple
        {
            get
            {
                return SelectionMode == SelectionMode.Multiple;
            }
        }

        public ObservableCollection<RealTypeModel> RealTypes { get; }
    }
}
