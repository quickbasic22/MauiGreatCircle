using MauiGreatCircle.Model;
using MauiGreatCircle.ViewModels;
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
    public MainPage()
	{
		InitializeComponent();
        BindingContext = new MainPageViewModel();
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

        Double[] Answer2 = GreateCircle.Get_AntiPodal(latDbArray2[0], latDbArray2[1], latDbArray2[2], lngDbArray2[0], lngDbArray2[1], lngDbArray2[2]);
        Double AnswerLat2 = Math.Round(Answer2[0], 5);
        Double AnswerLng2 = Math.Round(Answer2[1], 5);

        LblAntipodal1.Text = AnswerLat.ToString() + " " + AnswerLng.ToString();
        LblAntipodal2.Text = AnswerLat2.ToString() + " " + AnswerLng2.ToString();
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

    private void LocationPicker1_SelectedIndexChanged(object sender, EventArgs e)
    { 
        var location1 = LocationPicker1.SelectedItem as LocationInfo;
        if (location1 != null)
        {
            Latitude1.Text = location1.Latitude.ToString();
            Longitude1.Text = location1.Longitude.ToString();
            Lat1 = location1.Latitude;
            Long1 = location1.Longitude;
        }
        else
        {
            Latitude1.Text = "";
            Longitude1.Text = "";
            Lat1 = 0.0;
            Long1 = 0.0;
        }   
    }

    private void LocationPicker2_SelectedIndexChanged(object sender, EventArgs e)
    {
        var location2 = LocationPicker2.SelectedItem as LocationInfo;
        if (location2 != null)
        {
            Latitude2.Text = location2.Latitude.ToString();
            Longitude2.Text = location2.Longitude.ToString();
            Lat2 = location2.Latitude;
            Long2 = location2.Longitude;
        }
        else
        {
            Latitude2.Text = "";
            Longitude2.Text = "";
            Lat2 = 0.0;
            Long2 = 0.0;
        }  
    }

    private void BtnClear_Clicked(object sender, EventArgs e)
    {
        var location1 = LocationPicker1.SelectedItem as LocationInfo;
        var location2 = LocationPicker2.SelectedItem as LocationInfo;
        Location1.Text = "";
        Latitude1.Text = "";
        Longitude1.Text = "";
        Location2.Text = "";
        Latitude1.Text = "";
        Longitude1.Text = "";
        LocationPicker1.SelectedIndex = -1;
        LocationPicker2.SelectedIndex = -1;
        LblName1.Text = "";
        LblResult.Text = "";
        LblAntipodal1.Text = "";
        LblAntipodal2.Text = "";
    }
}

