using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using RealEstate.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using RealEstate.Models;

namespace RealEstate.ViewModels
{
    public class SelectionViewModel : BaseViewModel
    {
        IEnumerable<SelectionModel> lastSelections;
        public SelectionViewModel(string title, string path, IEnumerable<SelectionModel> selections = null, SelectionMode selectionMode = SelectionMode.Multiple)
        {
            _selections = new ObservableRangeCollection<SelectionModel>();
            if (selections != null)
            {
                lastSelections = selections;
            }
            Title = title;
            SelectionMode = selectionMode;

            GetDataAsync(path);
        }

        public SelectionViewModel(string title, IEnumerable<SelectionModel> options, IEnumerable<SelectionModel> selections = null, SelectionMode selectionMode = SelectionMode.Multiple)
        {
            _selections = new ObservableRangeCollection<SelectionModel>(options);
            if (selections != null)
            {
                lastSelections = selections;
            }
            GenerateSelected();
            Title = title;
            SelectionMode = selectionMode;
        }

        private async Task GetDataAsync(string path)
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var apiService = DependencyService.Get<IAPIService>();
                var selections = await apiService.GetAsync<IEnumerable<SelectionModel>>(path);
                if (selections != null)
                {
                    _selections.AddRange(selections);

                    GenerateSelected();
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void GenerateSelected()
        {
            if (lastSelections != null)
            {
                foreach (var item in _selections)
                {
                    item.Selected = false;
                }
                var selecteds = from s1 in lastSelections
                                from s2 in _selections
                                where s1.Id == s2.Id
                                select s2;
                foreach (var item in selecteds)
                {
                    item.Selected = true;
                }
            }
        }

        private ObservableRangeCollection<SelectionModel> _selections;
        public ObservableRangeCollection<SelectionModel> Selections
        {
            get
            {
                if (string.IsNullOrEmpty(Search))
                {
                    return _selections;
                }
                else
                {
                    ObservableRangeCollection<SelectionModel> selections = new ObservableRangeCollection<SelectionModel>();
                    selections.AddRange(_selections.Where(s => s.Name.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0));
                    return selections;
                }
            }
        }

        public IEnumerable<SelectionModel> SelectedItems
        {
            get
            {
                return _selections.Where(s => s.Selected);
            }
        }

        string search;
        public string Search
        {
            get
            {
                return search;
            }
            set
            {
                SetProperty(ref search, value);
                OnPropertyChanged("Selections");
            }
        }

        SelectionMode _selectionMode;
        public SelectionMode SelectionMode
        {
            get
            {
                return _selectionMode;
            }
            set
            {
                SetProperty(ref _selectionMode, value);
            }
        }

        public bool IsMultiple
        {
            get
            {
                return SelectionMode == SelectionMode.Multiple;
            }
        }

        public void Selected(IEnumerable<SelectionModel> selection)
        {
            foreach (var item in _selections.Where(s => s.Selected))
            {
                item.Selected = false;
            }
            var selected = from s1 in _selections
                           from s2 in selection
                           where s1.Id == s2.Id
                           select s1;
            if (selected != null)
            {
                foreach (var item in selected)
                {
                    item.Selected = true;
                }
            }
        }
    }
}
