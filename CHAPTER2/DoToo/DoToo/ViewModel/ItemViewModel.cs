using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoToo.Models;
using DoToo.Repositories;

namespace DoToo.ViewModel
{
    public partial class ItemViewModel : ViewModel
    {
        private readonly IToDoItemRepository _repository;

        [ObservableProperty]
        ToDoItem item;

        public ItemViewModel(IToDoItemRepository repository)
        {
            this._repository = repository;
            Item = new ToDoItem() { Due = DateTime.Now.AddDays(1) };
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            await _repository.AddOrUpdateAync(Item);
            await Navigation.PopAsync();
        }
    }
}
