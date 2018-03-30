using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickFief.MovieLib.Data
{
    public static class MovieDatabaseExtensions
    {
        public static void Seed ( this IMovieDatabase source )
        {
            var message = "";
            source.Add(new Movie() {
                Name = "iPhone X",
                IsDiscontinued = true,
                Price = 1500,

            }, out message);
            source.Add(new Movie() {
                Name = "Windows Phone",
                IsDiscontinued = true,
                Price = 15,
            }, out message);
            source.Add(new Movie() {
                Name = "Samsung S8",
                IsDiscontinued = false,
                Price = 800,
            }, out message);
                
        }
    }
}
