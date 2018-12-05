using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;

namespace day01
{
    class Program
    {
        static void Main(string[] args)
        {
            //string inputUri = @"https://adventofcode.com/2018/day/1/input";
            //string input = new WebClient().DownloadString(inputUri);

            var exeFilename = Assembly.GetExecutingAssembly().Location;
            var inputFilename = Path.Combine(Path.GetDirectoryName(exeFilename), "input");
            string input = File.ReadAllText(inputFilename);
            var result = new Calibrator().Calibrate(input);
            Console.WriteLine($"First calibration: {result}");

            var cacheValues = new HashSet<long>();
            var twice = new Calibrator().CalibrateTwice(input, cacheValues);
            Console.WriteLine($"Second calibration: {twice}");

            Console.ReadKey();
        }
    }

    internal class Calibrator
    {
        internal long Calibrate(string input)
        {
            long result = 0;
            var splittedInput = input.Replace("\r\n", "\n").Replace("\r", "\n").Split("\n");
            foreach (var item in splittedInput)
            {
                if (String.IsNullOrEmpty(item)) continue;
                var value = long.Parse(item);
                result += value;
            }
            return result;
        }

        internal long CalibrateTwice(string input, HashSet<long> cacheValues)
        {
            long? twiceResult = null;
            var splittedInput = input.Replace("\r\n", "\n").Replace("\r", "\n").Split("\n");

            long result = 0;
            while (twiceResult == null)
            {
                foreach (var item in splittedInput)
                {
                    if (String.IsNullOrEmpty(item)) continue;
                    var value = long.Parse(item);
                    result += value;

                    if (cacheValues.Contains(result))
                    {
                        twiceResult = result;
                        break;
                    }
                    cacheValues.Add(result);
                }
            }
            return (long)twiceResult;
        }
    }
}
