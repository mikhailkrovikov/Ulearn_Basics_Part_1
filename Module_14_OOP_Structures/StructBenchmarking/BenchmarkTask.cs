using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class BuilderTest : ITask
    {
        public const int Count = 10000;
        public const char Letter = 'a';
        public StringBuilder StringBuilder = new StringBuilder();
        public void Run()
        {
            for (int i = 0; i < Count; i++)
            {
                StringBuilder.Append(Letter);
            }
            StringBuilder.ToString();
        }
    }

    public class StringTest : ITask
    {
        public const int Count = 10000;
        public const char Letter = 'a';
        public void Run()
        {
            new string(Letter, Count);
        }
    }

    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            task.Run();
            GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
            GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
                                            // и как-то повлияет на них.
            Stopwatch stopWatch = Stopwatch.StartNew();
            for (int i = 0; i < repetitionCount; i++)
                task.Run();
            stopWatch.Stop();
            return (double)stopWatch.ElapsedMilliseconds / repetitionCount;
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            BuilderTest builderTest = new BuilderTest();
            StringTest stringTest = new StringTest();
            Benchmark benchmark = new Benchmark();

            double builderTime = benchmark.MeasureDurationInMs(builderTest, 100);
            double stringTime = benchmark.MeasureDurationInMs(stringTest, 100);
            //Assert.Less(stringTime, builderTime);
        }
    }
}