using BrownianMotionMauiApp.Extensions;
using BrownianMotionMauiApp.Models;

using CommunityToolkit.Mvvm.ComponentModel;

using Microcharts;

using SkiaSharp;

using System.Windows.Input;

namespace BrownianMotionMauiApp.ViewModels;

public class BrownianMotionViewModel : ObservableObject
{
    #region Consts

    private const string BackgroundColor = "#000000";
    private const string SerieName = "Movimento Browniano";

    #endregion

    #region Constructors

    public BrownianMotionViewModel()
    {
        _series = new List<ChartSerie>();
        ChartBackgroundColor = Color.FromArgb(BackgroundColor);
        EntryColor = Color.FromArgb(BackgroundColor);
        var entries = new double[] { 1, 1000, -30, 150, 12 };
        SimulationCommand = new Command(OnSimulationCommandExecuted);
        ClearCommand = new Command(() =>
        {
            _series = new List<ChartSerie>();
            ChartInit(entries);
        });

        ChartInit(entries);
    }

    private void ChartInit(double[] entries)
    {
        Model = GetLineChart($"{SerieName} (Exemplo)", entries.GetChartEntries(), true);
    }

    #endregion

    #region Properties

    private double? _initialPrice;

    public double? InitialPrice
    {
        get => _initialPrice;
        set
        {
            if (value is null or < 1) return;

            if (Math.Abs((double)(_initialPrice ?? 0 - value)) < 0) return;

            _initialPrice = value;
            OnPropertyChanged();
        }
    }

    private double? _averageVolatility;

    public double? AverageVolatility
    {
        get => _averageVolatility;
        set
        {
            if (value is null or < 1) return;

            if (Math.Abs((double)(_averageVolatility ?? 0 - value)) < 0) return;

            _averageVolatility = value;
            OnPropertyChanged();
        }
    }

    private double? _averagePayback;
    public double? AveragePayback
    {
        get => _averagePayback;
        set
        {
            if (value is null or < 1) return;

            if (Math.Abs((double)(_averagePayback ?? 0 - value)) < 0) return;

            _averagePayback = value;
            OnPropertyChanged();
        }
    }

    private int? _days;
    public int? Days
    {
        get => _days;
        set
        {
            if (_days == value) return;

            _days = value;
            OnPropertyChanged();
        }
    }

    private static List<ChartSerie> _series;
    private ChartModel _model;

    public ChartModel Model
    {
        get => _model;
        set
        {
            _model = value;
            OnPropertyChanged();
        }
    }

    private Color _chartBackgroundColor;

    public Color ChartBackgroundColor
    {
        get => _chartBackgroundColor;
        set
        {
            if (Equals(_chartBackgroundColor, value)) return;

            _chartBackgroundColor = value;
            OnPropertyChanged();
        }
    }

    private Color _entryColor;

    public Color EntryColor
    {
        get => _entryColor;
        set
        {
            if (Equals(_entryColor, value)) return;

            _entryColor = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Commands

    public ICommand SimulationCommand { get; }
    public ICommand ClearCommand { get; }

    #endregion

    #region Methods

    private void OnSimulationCommandExecuted(object obj)
    {
        if (AveragePayback is null or < 1 || AverageVolatility is null or < 1 || InitialPrice is null or < 1 || Days is null or < 1)
        {
            Application.Current?.MainPage?.DisplayAlert("Erro", "Preencha todos o campos", "Ok");
            return;
        }

        var entries = ChartModel.GenerateBrownianMotion((double)AverageVolatility, (double)AveragePayback, (double)InitialPrice, (int)Days);

        Model = GetLineChart(SerieName, entries.GetChartEntries());
    }

    private static ChartModel GetLineChart(string serieName, IEnumerable<ChartEntry> entries, bool isExample = false)
    {
        var counter = _series.Where(s => !s.Name.Contains("Exemplo")).ToList().Count + 1;
        var serie = GetChartSerie(serieName, entries, counter, isExample);

        _series.Add(serie);

        var series = !isExample
            ? _series.Where(s => !s.Name.Contains("Exemplo")).ToList() 
            : _series;
        return new ChartModel()
        {
            Chart = new LineChart()
            {
                BackgroundColor = SKColor.Parse(Colors.Gray.ToHex()),
                LabelColor = SKColor.Parse(Colors.White.ToHex()),
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelTextSize = 10,
                LineMode = LineMode.Straight,
                ValueLabelTextSize = 1,
                SerieLabelTextSize = 16,
                PointMode = PointMode.None,
                PointSize = 0,
                LineSize = 2,
                LegendOption = SeriesLegendOption.Top,
                Series = series
            },
            Height = (DeviceDisplay.MainDisplayInfo.Height / 2),
            Width = (DeviceDisplay.MainDisplayInfo.Width / 2)
        };
    }

    private static ChartSerie GetChartSerie(string serieName, IEnumerable<ChartEntry> entries, int counter, bool isExample = false)
    {
        if (isExample)
            return new ChartSerie()
            {
                Name = serieName,
                Color = SKColor.Parse(Colors.MediumPurple.ToHex()),
                Entries = entries
            };

        return new ChartSerie
        {
            Name = $"{serieName} {counter}",
            Color = ChartModel.GetRandomColor(),
            Entries = entries
        };
    }

    #endregion
}