using System;
using System.IO;
using NLog;
using NLog.Web;

namespace MovieLibrary
{
    class Program
    {
        private Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        static void Main(string[] args)
        {
            string csvPath = "movies.csv";

            File csv = new File(csvPath);

            string userInput = "";
            do
            {
                Console.WriteLine("(A)dd Movie  |  (P)rint Movies  |  NA to quit");
                userInput = Console.ReadLine();
                if (userInput.ToUpper() == "A")
                {
                    Movie movie = new Movie();
                    Console.Write("Title: ");
                    movie.Title = Console.ReadLine();
                    if (csv.IsCreated(movie.Title))
                    {
                        string input;
                        do
                        {
                            Console.WriteLine("Genre (NA when done)");
                            input = Console.ReadLine();
                            if (input.ToUpper() != "NA" && input.Length > 0)
                            {
                                movie.Genres.Add(input);
                            }
                        } while (input.ToUpper() != "NA");
                        if (movie.Genres.Count == 0)
                        {
                            movie.Genres.Add("n/a");
                        }
                        csv.AddMovie(movie);
                    }
                }
                else if (userInput.ToUpper() == "P")
                {
                    foreach (Movie mov in csv.Movies)
                    {
                        Console.WriteLine(mov.Print());
                    }
                }
            } while (userInput.ToUpper() == "A" || userInput.ToUpper() == "P");
        }
    }
}