using BrownianMotionMauiApp.Models;

using Microcharts;

namespace BrownianMotionMauiApp.Extensions;
public static class DoubleExtensions
{
    public static List<ChartEntry> GetChartEntries(this double[] entries)
    {
        return entries.ToList().Select(x => new ChartEntry(ChartModel.GetPrice(x))
        {
            Color = ChartModel.GetRandomColor()
        }).ToList();
    }
}
