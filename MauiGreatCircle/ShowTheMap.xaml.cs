namespace MauiGreatCircle;

public partial class ShowTheMap : ContentPage
{
	public ShowTheMap()
	{
		InitializeComponent();
	}

    private async void BtnMap_Clicked(object sender, EventArgs e)
    {
		await Launcher.OpenAsync(@"http://maps.google.com/?daddr=San+Francisco,+CA&saddr=Mountain+View");
    }
}