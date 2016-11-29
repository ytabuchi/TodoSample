using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using TodoSample.Models;


namespace TodoSample.ViewModels
{
    public class ItemPageViewModel : ViewModelBase
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _done;
        public bool Done
        {
            get { return _done; }
            set
            {
                if (_done != value)
                {
                    _done = value;
                    OnPropertyChanged();
                }
            }
        }

        ObservableCollection<TodoItem> Items = new ObservableCollection<TodoItem>();
        TodoItemManager manager = new TodoItemManager();

        public Command SaveCommand { get; private set; }
        public Command DeleteCommand { get; private set; }

        public ItemPageViewModel()
        {
            
        }
    }
}
