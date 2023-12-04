using CommunityToolkit.Mvvm.ComponentModel;

using Microcharts;

using SkiaSharp;

namespace BrownianMotionMauiApp.Models;
public class ChartModel : ObservableObject
{
    private double _height;

    public double Height
    {
        get => _height;
        set
        {
            if (_height == value) return;
            
            _height = value;
            //OnPropertyChanged();
        }
    }

    private double _width;

    public double Width
    {
        get => _width;
        set
        {
            if (_width == value) return;

            _width = value;
            //OnPropertyChanged();
        }
    }

    private Chart _chart;

    public Chart Chart
    {
        get => _chart;
        set 
        {
            if (_chart == value) return;

            _chart = value;
        }
    }

    public static float GetPrice(double price)
    {
        return price switch
        {
            > float.MaxValue => float.MaxValue,
            < float.MinValue => float.MinValue,
            _ => (float)Math.Round(price, 2)
        };
    }

    public static SKColor GetRandomColor()
    {
        var rand = new Random();
        var red = rand.Next(0, 256);
        var green = rand.Next(0, 256);
        var blue = rand.Next(0, 256);

        return SKColor.Parse($"#{red:X2}{green:X2}{blue:X2}");
    }

    public static double[] GenerateBrownianMotion(double sigma, double mean, double initialPrice, int numDays)
    {
        Random rand = new();
        double[] prices = new double[numDays];
        prices[0] = initialPrice;

        for (var i = 1; i < numDays; i++)
        {
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

            double retornoDiario = mean + sigma * z;
            prices[i] = prices[i - 1] * Math.Exp(retornoDiario);
        }

        return prices;
    }
}
