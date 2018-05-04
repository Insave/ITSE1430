/*
 * ITSE 1430
 * Lab 4
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatrickFief.MovieLib.Data.Memory
{
    /// <summary>Provides an in-memory movie database.</summary>
    public class MemoryMovieDatabase : MovieDatabase
    {                
        protected override Movie AddCore ( Movie movie )
        {
            // Clone the object
            movie.Id = _nextId++;
            _movies.Add(Clone(movie));

            // Return a copy
            return movie;
        }

        protected override Movie GetCore( int id )
        {
            return (from p in _movies
                    where p.Id == id
                    select p).FirstOrDefault();
        }

        protected override IEnumerable<Movie> GetAllCore ()
                            => from p in _movies
                               select Clone(p);
                
        protected override void RemoveCore ( int id )
        {
            var existing = GetCore(id);
            if (existing != null)
                _movies.Remove(existing);
        }

        protected override Movie UpdateCore ( Movie movie )
        {
            var existing = GetCore(movie.Id);

            // Clone the object
            Copy(existing, movie);

            return movie;
        }

        protected override Movie GetMovieByNameCore( string title )
        {
            return (from p in _movies
                    where String.Compare(p.Title, title, true) == 0
                    select p).FirstOrDefault();
        }

        #region Private Members

        //Clone a movie
        private Movie Clone ( Movie item )
        {
            var newMovie = new Movie();
            Copy(newMovie, item);

            return newMovie;
        }

        //Copy a movie from one object to another
        private void Copy ( Movie target, Movie source )
        {
            target.Id = source.Id;
            target.Title = source.Title;
            target.Description = source.Description;
            target.Length = source.Length;
            target.IsOwned = source.IsOwned;
        }
                        
        private readonly List<Movie> _movies = new List<Movie>();
        private int _nextId = 1;

        #endregion
    }
}
