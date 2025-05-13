using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ballectics.App.Models;
using Ballectics.App.Pages;
using Ballectics.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;

namespace Ballectics.App.ViewModels;

public partial class BasePageViewModel : ObservableObject
{
    [ObservableProperty]
    public bool _isBusy;

    [ObservableProperty]
    public string _title;

    public bool IsNotBusy => !IsBusy;

    partial void OnIsBusyChanged(bool oldValue, bool newValue)
    {
        OnPropertyChanged(nameof(IsNotBusy));
    }

    protected Task RunOnUiAsync(Action action)
    {
        return MainThread.InvokeOnMainThreadAsync(async () => action);
    }

    protected Task<T> RunOnUiAsync<T>(Func<T> func)
    {
        return MainThread.InvokeOnMainThreadAsync(func);
    }

    protected Task RunOnUiAsync(Func<Task> asyncAction)
    {
        return MainThread.InvokeOnMainThreadAsync(async () => await asyncAction());
    }
}
