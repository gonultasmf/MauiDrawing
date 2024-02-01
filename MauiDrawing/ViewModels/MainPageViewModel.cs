using CommunityToolkit.Maui.Core;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiDrawing.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<IDrawingLine> _lines = new();

    public ICommand ClearCommand => new Command(() =>
    {
        Lines.Clear();
    });
}
