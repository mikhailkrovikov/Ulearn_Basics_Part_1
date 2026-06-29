using System.Collections.Generic;

namespace StructBenchmarking
{
    public enum Task
    {
        ArrayCreationTask,
        MethodCallTask,
    }

    public class BuildChartData
    {
        public ChartData Create(IBenchmark benchmark, Task task, string title, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();
            List<int> newFieldCounts = new List<int>();
            foreach (var element in Constants.FieldCounts)
                newFieldCounts.Add(element);
            if (task == Task.ArrayCreationTask)
                for (int i = 0; i < Constants.FieldCounts.Count; i++)
                {
                    classesTimes.Add(new ExperimentResult(newFieldCounts[i],
                                                          benchmark.MeasureDurationInMs(
new ClassArrayCreationTask(newFieldCounts[i]), repetitionsCount)));
                    structuresTimes.Add(new ExperimentResult(newFieldCounts[i],
                                                             benchmark.MeasureDurationInMs(
new StructArrayCreationTask(newFieldCounts[i]), repetitionsCount)));
                }
            else
                for (int i = 0; i < Constants.FieldCounts.Count; i++)
                {
                    classesTimes.Add(new ExperimentResult(newFieldCounts[i],
                        benchmark.MeasureDurationInMs(new MethodCallWithClassArgumentTask(newFieldCounts[i]), repetitionsCount)));
                    structuresTimes.Add(new ExperimentResult(newFieldCounts[i],
                        benchmark.MeasureDurationInMs(new MethodCallWithStructArgumentTask(newFieldCounts[i]), repetitionsCount)));
                }
            return new ChartData { Title = title, ClassPoints = classesTimes, StructPoints = structuresTimes, };
        }
    }

    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var temp = new BuildChartData();
            return temp.Create(benchmark, Task.ArrayCreationTask, "Create array", repetitionsCount);
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var temp = new BuildChartData();
            return temp.Create(benchmark, Task.MethodCallTask, "Call method with argument", repetitionsCount);
        }
    }
}