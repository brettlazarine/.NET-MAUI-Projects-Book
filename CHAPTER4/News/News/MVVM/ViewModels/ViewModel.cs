using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.MVVM.ViewModels
{
    [ObservableObject]
    public abstract partial class ViewModel
    {
        public INavigate Navigation { get; init; }

        internal ViewModel(INavigate navigation) => Navigation = navigation;
    }
}
