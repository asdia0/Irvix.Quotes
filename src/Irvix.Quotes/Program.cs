namespace Irvix.Quotes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Program
    {
        /// <summary>
        /// Print a list of quotes from a specified period.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Start date: ");
            DateTime start = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("End date: ");
            DateTime end = DateTime.Parse(Console.ReadLine());
            Console.Clear();
            GetQuotes(start, end);
            Console.Read();
        }

        /// <summary>
        /// Gets a list of dates from a starting date to an ending date.
        /// </summary>
        /// <param name="start">The starting date.</param>
        /// <param name="end">The ending date.</param>
        /// <returns></returns>
        public static List<DateTime> GetDateRange(DateTime start, DateTime end)
        {
            return Enumerable.Range(0, 1 + end.Subtract(start).Days)
                .Select(offset => start.AddDays(offset))
                .ToList();
        }

        /// <summary>
        /// Returns a list of quotes from `quotes.txt`.
        /// </summary>
        /// <param name="start">The start date to search from.</param>
        /// <param name="end">The date to stop searching at.</param>
        public static void GetQuotes(DateTime start, DateTime end)
        {
            List<string> lines = File.ReadAllLines("quotes.txt").ToList();
            List<DateTime> dates = GetDateRange(start, end);
            List<string> quotes = new();
            List<string> actualQuotes = new();

            foreach (string line in lines)
            {
                if (line.Count(i => i == '~') == 1 && line.Count(i => i == '\"') > 1)
                {
                    quotes.Add(line);
                }
            }

            foreach (DateTime date in dates)
            {
                string dateString = date.ToString("dd/MM/yyyy");
                List<string> dayQuotes = new();

                foreach (string quote in quotes)
                {
                    if (quote.Contains(dateString))
                    {
                        string actualQuote = quote.Substring(17).ToLower().Replace(" ", "").Split(":")[1];
                        if (!actualQuotes.Contains(actualQuote))
                        {
                            dayQuotes.Add(quote);
                            actualQuotes.Add(actualQuote);
                        }
                    }
                }

                if (dayQuotes.Count > 0)
                {
                    Console.WriteLine($"\n{dateString}");

                    foreach (string dayQuote in dayQuotes)
                    {
                        Console.WriteLine(dayQuote.Split(": ")[1]);
                    }
                }
            }
        }
    }
}
