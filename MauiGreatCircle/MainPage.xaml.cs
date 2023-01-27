using System.Runtime.CompilerServices;

namespace MauiGreatCircle;

public partial class MainPage : ContentPage
{
	GreatCircle GreateCircle = new GreatCircle();
	Double Lat1 { get; set; }
	Double Lat2 { get; set; }
	Double Long1 { get; set; }
	Double Long2 { get; set; }
	Double Result { get; set; }

	Double OneDegLat { get; set; } = 29.8815356;
    Double NormalDegLat { get; set; } = 28.8815356;
    Double OneDegLon { get; set; } = -80.7036378;
    Double NormalDegLon { get; set; } = -81.7036378;

    public MainPage()
	{
		InitializeComponent();
        BindingContext = new Model.LocationInfo();
	}

    private void BtnCalculate_Clicked(object sender, EventArgs e)
    {
        Lat1 = Double.Parse(Latitude1.Text);
        Long1 = Double.Parse(Longitude1.Text);
        Lat2 = Double.Parse(Latitude2.Text);
        Long2 = Double.Parse(Longitude2.Text);

        Result = GreateCircle.GreatCircle_Calculation(Lat1, Long1, Lat2, Long2);
        Result = Math.Round(Result, 2);
        LblResult.Text = Result.ToString();

        string latStr1 = GreateCircle.Degrees_DMS(Lat1);
        string lngStr1 = GreateCircle.Degrees_DMS(Long1);
        string latStr2 = GreateCircle.Degrees_DMS(Lat2);
        string lngStr2 = GreateCircle.Degrees_DMS(Long2);

        Double[] latDbArray1 = ParseStringToDoubleArray(latStr1);
        Double[] lngDbArray1 = ParseStringToDoubleArray(lngStr1);
        Double[] latDbArray2 = ParseStringToDoubleArray(latStr2);
        Double[] lngDbArray2 = ParseStringToDoubleArray(lngStr2);


        Double[] Answer = GreateCircle.Get_AntiPodal(latDbArray1[0], latDbArray1[1], latDbArray1[2], lngDbArray1[0], lngDbArray1[1], lngDbArray1[2]);
        Double AnswerLat = Math.Round(Answer[0],5);
        Double AnswerLng = Math.Round(Answer[1], 5);
        LblAntipodal.Text = AnswerLat.ToString() + " " + AnswerLng.ToString();
    }

    private void BtnSwitch_Clicked(object sender, EventArgs e)
    {
		if (Double.Parse(Latitude2.Text) == 28.8815356)
		{
            Latitude2.Text = OneDegLat.ToString();
            Longitude2.Text = NormalDegLon.ToString();
        }
		else
		{
            Latitude2.Text = NormalDegLat.ToString();
            Longitude2.Text = OneDegLon.ToString();
		}
    }

    public static double[] ParseStringToDoubleArray(string input)
    {
		string[] coorRaw = input.Split(' ');
        double[] result = new double[coorRaw.Length];
        for (int i = 0; i < coorRaw.Length; i++)
        {
            result[i] = double.Parse(coorRaw[i]);
        }
        return result;
    }

    private void LocationPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var location = (Model.LocationInfo)picker.SelectedItem;
        if (Latitude1.Text == "")
        {
            Latitude1.Text = location.Latitude.ToString();
            Longitude1.Text = location.Longitude.ToString();
            Location1.Text = location.Name;
        }
        else
        {
            Latitude2.Text = location.Latitude.ToString();
            Longitude2.Text = location.Longitude.ToString();
            Location2.Text = location.Name;
        }
    }
}

