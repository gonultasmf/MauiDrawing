
namespace MauiDrawing.Views;

public partial class MainPage(MainPageViewModel viewModel) : BasePage<MainPageViewModel>(viewModel, "Main Page")
{
    private Image drawingImage;
    public override void Build()
    {
        this
        .ContentFmg(
            new Grid()
            .ColumnDefinitionsFmg(e => e.Star().Star())
            .RowDefinitionsFmg(e => e.Star().Auto().Absolute(200))
            .RowSpacingFmg(10)
            .ColumnSpacingFmg(10)
            .PaddingFmg(10)
            .ChildrenFmg(
                new DrawingView()
                .LineColorFmg(Blue)
                .LineWidthFmg(2)
                .ShouldClearOnFinishFmg(false)
                .IsMultiLineModeEnabledFmg(true)
                .ColumnSpanFmg(2)
                .BindFmg(DrawingView.LinesProperty, nameof(BindingContext.Lines))
                .InvokeOnElementFmg(d => d.DrawingLineCompleted += D_DrawingLineCompleted),

                new Button()
                .TextFmg("Temizle")
                .RowFmg(1)
                .CommandFmg(BindingContext.ClearCommand),

                new Button()
                .TextFmg("Kaydet")
                .RowFmg(1)
                .ColumnFmg(1)
                .InvokeOnElementFmg(b => b.Clicked += SaveDrawing),

                new Image()
                .AssignFmg(out drawingImage)
                .HeightRequestFmg(200)
                .WidthRequestFmg(400)
                .RowFmg(2)
                .ColumnSpanFmg(2)
            )
        );
    }

    private async void D_DrawingLineCompleted(object? sender, CommunityToolkit.Maui.Core.DrawingLineCompletedEventArgs e)
    {
        var stream = await e.LastDrawingLine.GetImageStream(400, 200, Gray.AsPaint());
        drawingImage.Source = ImageSource.FromStream(() => stream);
    }

    private async void SaveDrawing(object? sender, EventArgs e)
    {
        var stream = await DrawingView.GetImageStream(BindingContext.Lines, new Size(400, 200), Gray);
        drawingImage.Source = ImageSource.FromStream(() => stream);
    }
}
