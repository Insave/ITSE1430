/*
 * ITSE 1430
 * Lab 4
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
            source.Add(new Movie() {
                Title = "The Mummy",
                IsOwned = true,
                Description = "There's a scarry mummy guy who wants to rule the world",
                Length = 90, });
            source.Add(new Movie() {
                Title = "The Incredibles",
                IsOwned = true,
                Length = 120, });
            source.Add(new Movie() {
                Title = "A long movie",
                IsOwned = false,
                Length = 800
            });
        }
    }
}
