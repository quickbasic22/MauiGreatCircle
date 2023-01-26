namespace MauiGreatCircle;

public partial class MainPage : ContentPage
{
	GreatCircle GreateCircle = new GreatCircle();
	Double Latitude1 { get; set; }
	Double Latitude2 { get; set; }
	Double Longitude1 { get; set; }
	Double Longitude2 { get; set; }
	Double Result { get; set; }

	Double OneDegLat { get; set; } = 29.8815356;
    Double NormalDegLat { get; set; } = 28.8815356;
    Double OneDegLon { get; set; } = -80.7036378;
    Double NormalDegLon { get; set; } = -81.7036378;

    public MainPage()
	{
		InitializeComponent();
	}

    private void BtnCalculate_Clicked(object sender, EventArgs e)
    {
		Latitude1 = Double.Parse(EntryLat1.Text);
		Longitude1 = Double.Parse(EntryLng1.Text);
		Latitude2 = Double.Parse(EntryLat2.Text);
		Longitude2 = Double.Parse(EntryLng2.Text);

		Result = GreateCircle.GreatCircle_Calculation(Latitude1, Longitude1, Latitude2, Longitude2);
		Result = Math.Round(Result, 2);
		LblResult.Text = Result.ToString();
    }

    private void BtnSwitch_Clicked(object sender, EventArgs e)
    {
		if (Double.Parse(EntryLat2.Text) == 28.8815356)
		{
			EntryLat2.Text = OneDegLat.ToString();
            EntryLng2.Text = NormalDegLon.ToString();
        }
		else
		{
			EntryLat2.Text = NormalDegLat.ToString();
			EntryLng2.Text = OneDegLon.ToString();
		}
    }
}

