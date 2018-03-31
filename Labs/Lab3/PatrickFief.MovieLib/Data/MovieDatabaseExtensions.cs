/*
 * ITSE 1430
 * Patrick Fief
 * Lab 3
 */
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
                Title = "iPhone X",
                IsOwned = true,
                Length = 1500,

            }, out message);
            source.Add(new Movie() {
                Title = "Windows Phone",
                IsOwned = true,
                Length = 15,
            }, out message);
            source.Add(new Movie() {
                Title = "Samsung S8",
                IsOwned = false,
                Length = 800,
            }, out message);
                
        }
    }
}
