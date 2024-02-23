using System;
using System.Collections.Generic;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DoToo.ViewModel
{
    [ObservableObject]
    public abstract partial class ViewModel
    {
        public INavigation Navigation { get; set; }
    }
}
