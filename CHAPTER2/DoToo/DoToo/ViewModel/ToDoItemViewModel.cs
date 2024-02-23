using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoToo.ViewModel
{
    public partial class ToDoItemViewModel : ViewModel
    {
        public ToDoItemViewModel(ToDoItem item) => Item = item;

        public event EventHandler ItemStatusChanged;

        [ObservableProperty]
        ToDoItem item;

        public string StatusText => Item.Completed ? "Reactivate" : "Completed";

        [RelayCommand]
        void ToggleCompleted()
        {
            Item.Completed = !Item.Completed;
            ItemStatusChanged?.Invoke(this, new EventArgs());
        }
    }
}
