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
    /// <summary>Provides extension methods for <see cref="IMovieDatabase"/>.</summary>
    public static class MovieDatabaseExtensions
    {
        /// <summary>Seeds the database.</summary>
        /// <param name="source">The source.</param>
        public static void Seed ( this IMovieDatabase source )
        {
            var message = "";
            source.Add(new Movie() {
                Title = "The Mummy",
                IsOwned = true,
                Length = 120,

            }, out message);
            source.Add(new Movie() {
                Title = "The Incredibles",
                IsOwned = true,
                Length = 105,
            }, out message);
            source.Add(new Movie() {
                Title = "Test movie",
                IsOwned = false,
                Length = 60,
            }, out message);
                
        }
    }
}
