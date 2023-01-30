using MauiGreatCircle.Model;
using MauiGreatCircle.ViewModels;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

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
        LblThroughGround1.Text = "";
        LblThroughGround2.Text = "";
        Lat1 = Double.Parse(Latitude1.Text);
        Long1 = Double.Parse(Longitude1.Text);
        Lat2 = Double.Parse(Latitude2.Text);
        Long2 = Double.Parse(Longitude2.Text);

        Double Lat1Radians = GreateCircle.DegreesToRadians(Lat1);
        Double Lng1Radians = GreateCircle.DegreesToRadians(Long1);
        Double Lat2Radians = GreateCircle.DegreesToRadians(Lat2);
        Double Lng2Radians = GreateCircle.DegreesToRadians(Long2);

        Lat1Radians = Math.Round(Lat1Radians, 6);
        Lng1Radians = Math.Round(Lng1Radians, 6);
        Lat2Radians = Math.Round(Lat2Radians, 6);
        Lng2Radians = Math.Round(Lng2Radians, 6);

        Location1Radians.Text = Lat1Radians.ToString() + " " + Lng1Radians.ToString();

        Location2Radians.Text = Lat2Radians.ToString() + " " + Lng2Radians.ToString();


        Result = GreateCircle.GreatCircle_Calculation(Lat1, Long1, Lat2, Long2);
        Result = Math.Round(Result, 0);
        LblResult.Text = Result.ToString();

        string latStr1 = GreateCircle.Degrees_DMS(Lat1);
        string lngStr1 = GreateCircle.Degrees_DMS(Long1);
        string latStr2 = GreateCircle.Degrees_DMS(Lat2);
        string lngStr2 = GreateCircle.Degrees_DMS(Long2);

        Location1DMS.Text = latStr1 + " " + lngStr1;
        Location2DMS.Text = lngStr2 + " " + lngStr2;

        Double[] latDbArray1 = ParseStringToDoubleArray(latStr1);
        Double[] lngDbArray1 = ParseStringToDoubleArray(lngStr1);
        Double[] latDbArray2 = ParseStringToDoubleArray(latStr2);
        Double[] lngDbArray2 = ParseStringToDoubleArray(lngStr2);

        Double DistancePointAToPointB = GreateCircle.GetDistantThroughEarth(Lat1, Long1, Lat2, Long2);
        DistancePointAToPointB = Math.Round(DistancePointAToPointB, 0);
        LblResult2.Text = DistancePointAToPointB.ToString();

        Double[] Answer = GreateCircle.Get_AntiPodal(latDbArray1[0], latDbArray1[1], latDbArray1[2], lngDbArray1[0], lngDbArray1[1], lngDbArray1[2]);
        Double AnswerLat = Math.Round(Answer[0], 4);
        Double AnswerLng = Math.Round(Answer[1], 4);

        Double[] Answer2 = GreateCircle.Get_AntiPodal(latDbArray2[0], latDbArray2[1], latDbArray2[2], lngDbArray2[0], lngDbArray2[1], lngDbArray2[2]);
        Double AnswerLat2 = Math.Round(Answer2[0], 4);
        Double AnswerLng2 = Math.Round(Answer2[1], 4);

        LblAntipodal1.Text = AnswerLat.ToString() + " " + AnswerLng.ToString();

        LblAntipodal2.Text = AnswerLat2.ToString() + " " + AnswerLng2.ToString();

        LblResult3.Text = (Result - DistancePointAToPointB).ToString();

        //var ThroughGround1 = GreateCircle.GetDistantThroughEarth(Lat1, Long1, AnswerLat, AnswerLng);
        //var ThroughGround2 = GreateCircle.GetDistantThroughEarth(Lat2, Long2, AnswerLat2, AnswerLng2);

        //ThroughGround1 = Math.Round(ThroughGround1, 0);
        //ThroughGround2 = Math.Round(ThroughGround2, 0);

        //LblThroughGround1.Text = ThroughGround1.ToString();
        //LblThroughGround2.Text = ThroughGround2.ToString();

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
        LblResult2.Text = "";
        Location1DMS.Text = "";
        Location1Radians.Text = "";
        LblAntipodalTo1.Text = "";
        var location1 = LocationPicker1.SelectedItem as LocationInfo;
        if (location1 != null)
        {
            Location1.IsVisible = false;
            Latitude1.Text = location1.Latitude.ToString();
            Longitude1.Text = location1.Longitude.ToString();
            Lat1 = location1.Latitude;
            Long1 = location1.Longitude;
            LblAntipodalTo1.Text = location1.Name + " " + "Antipodal Location";
        }
        else
        {
            LblResult2.Text = "";
            Location1.Text = "";
            Location1.IsVisible = true;
            Latitude1.Text = "";
            Longitude1.Text = "";
            Lat1 = 0.0;
            Long1 = 0.0;
        }   
    }

    private void LocationPicker2_SelectedIndexChanged(object sender, EventArgs e)
    {
        LblResult2.Text = "";
        Location2DMS.Text = "";
        Location2Radians.Text = "";
        LblAntipodalTo2.Text = "";
        var location2 = LocationPicker2.SelectedItem as LocationInfo;
        if (location2 != null)
        {
            Location2.IsVisible = false;
            Latitude2.Text = location2.Latitude.ToString();
            Longitude2.Text = location2.Longitude.ToString();
            Lat2 = location2.Latitude;
            Long2 = location2.Longitude;
            LblAntipodalTo2.Text = location2.Name + " " + "Antipodal location";
        }
        else
        {
            LblResult2.Text = "";
            Location2.Text = "";
            Location1.IsVisible = true;
            Latitude2.Text = "";
            Longitude2.Text = "";
            Lat2 = 0.0;
            Long2 = 0.0;
        }  
    }

    private void BtnClear_Clicked(object sender, EventArgs e)
    {
        Location1DMS.Text = "";
        Location2DMS.Text = "";
        Location1Radians.Text = "";
        Location2Radians.Text = "";
        Location1.IsVisible = true;
        Location2.IsVisible = true;
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
       
        LblResult.Text = "";
        LblResult2.Text = "";
        LblAntipodal1.Text = "";
        LblAntipodal2.Text = "";
        LblAntipodalTo1.Text = "";
        LblAntipodalTo2.Text = "";
        LblAntipodalTo1.Text = "";
        LblAntipodalTo2.Text = "";
        LblThroughGround1.Text = "";
        LblThroughGround2.Text = "";
    }

    private void Location1Radians_TextChanged(object sender, TextChangedEventArgs e)
    {
        Clipboard.SetTextAsync(e.NewTextValue.ToString());
    }

    private void Location2Radians_TextChanged(object sender, TextChangedEventArgs e)
    {
        Clipboard.SetTextAsync(e.NewTextValue.ToString());
    }
}

