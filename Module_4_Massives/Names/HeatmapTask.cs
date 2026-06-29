using System.Xml.Linq;

namespace Names;

internal static class HeatmapTask
{
    public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
    {
        string[] days = new string[30];
        string[] months = new string[12];
        double[,] mapOfDay = new double[30, 12];

        for (int i = 0; i < days.Length; i++)
        {
            days[i] = (i + 2).ToString();
        }

        for (int i = 0; i < months.Length; i++)
        {
            months[i] = (i + 1).ToString();
        }

        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].BirthDate.Day == 1) continue;
            int indexOfDay = names[i].BirthDate.Day - 2;
            int indexOfMonth = names[i].BirthDate.Month - 1;
            mapOfDay[indexOfDay, indexOfMonth]++;
        }

        return new HeatmapData("Пример карты интенсивностей", mapOfDay, days, months);
    }
}