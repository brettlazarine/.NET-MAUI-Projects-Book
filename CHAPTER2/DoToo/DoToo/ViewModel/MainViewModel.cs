using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoToo.Models;
using DoToo.Repositories;
using DoToo.Views;
using System.Collections.ObjectModel;

namespace DoToo.ViewModel
{
    public partial class MainViewModel : ViewModel
    {
        private readonly IToDoItemRepository _repository;
        private readonly IServiceProvider _services;

        [ObservableProperty]
        ObservableCollection<ToDoItemViewModel> items;

        [ObservableProperty]
        ToDoItemViewModel selectedItem;

        partial void OnSelectedItemChanging(ToDoItemViewModel value)
        {
            if (value is null)
            {
                return;
            }

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await NavigateToItemAsync(value);
            });
        }
        private async Task NavigateToItemAsync(ToDoItemViewModel item)
        {
            var itemView = _services.GetRequiredService<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;

            vm.Item = item.Item;
            itemView.Title = "Edit ToDo Item";

            await Navigation.PushAsync(itemView);
        }

        [RelayCommand]
        public async Task AddItemAsync() => await Navigation.PushAsync(_services.GetRequiredService<ItemView>());

        [ObservableProperty]
        bool showAll;

        public MainViewModel(IToDoItemRepository repository, IServiceProvider services)
        {
            this._repository = repository;
            this._services = services;

            _repository.OnItemUpdated += (sender, item) => Items.Add(CreateToDoItemViewModel(item));
            _repository.OnItemUpdated += (sender, items) => Task.Run(async () => await LoadDataAsync());
            Task.Run(async () => await LoadDataAsync());
        }

        private async Task LoadDataAsync()
        {
            var items = await _repository.GetItemsAsync();

            if (!ShowAll)
            {
                items = items.Where(x => x.Completed == false).ToList();
            }

            var itemViewModels = items.Select(i => CreateToDoItemViewModel(i));

            Items = new ObservableCollection<ToDoItemViewModel>(itemViewModels);
        }

        private ToDoItemViewModel CreateToDoItemViewModel(ToDoItem item)
        {
            var itemViewModel = new ToDoItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemStatusChanged(object sender, EventArgs e)
        {
            if (sender is ToDoItemViewModel item)
            {
                if (!ShowAll && item.Item.Completed)
                {
                    Items.Remove(item);
                }
                Task.Run(async () =>
                {
                    await _repository.UpdateItemAsync(item.Item);
                });
            }
        }

        [RelayCommand]
        private async Task ToggleFilterAsync()
        {
            ShowAll = !ShowAll;
            await LoadDataAsync();
        }
    }
}
