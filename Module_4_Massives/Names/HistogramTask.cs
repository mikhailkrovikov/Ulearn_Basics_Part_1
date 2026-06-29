using System;
using System.Globalization;

namespace Names;
internal static class HistogramTask
{
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        double[] frequency = new double[31];
        foreach (var element in names)
        {
            if (element.Name == name && element.BirthDate.Day != 1)
            {
                int data = element.BirthDate.Day;
                frequency[data - 1]++;
            }
        }
        string[] days = new string[31];
        for (int i = 0; i < days.Length; i++)
        {
            days[i] = (i + 1).ToString();
        }

        return new HistogramData
            (string.Format("Рождаемость людей с именем '{0}'", name),
            days,
            frequency);
    }
}