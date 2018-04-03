/*
 * ITSE 1430
 * Patrick Fief
 * Lab 3
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatrickFief.MovieLib.Data
{
    /// <summary>Provides a base implementation of <see cref="IMovieDatabase"/>.</summary>
    public abstract class MovieDatabase : IMovieDatabase
    {
        /// <summary>Add a new movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The added movie.</returns>
        /// <remarks>
        /// Returns an error if movie is null, invalid or if a movie
        /// with the same name already exists.
        /// </remarks>
        public Movie Add ( Movie movie, out string message )
        {
            //Check for null
            if (movie == null)
            {
                message = "Movie cannot be null.";
                return null;
            };

            //Validate product
            var errors = movie.Validate();

            var error = errors.FirstOrDefault();
            if(error != null)
            {
                message = error.ErrorMessage;
                return null;
            }

            // Verify unique product
            var existing = GetMovieByNameCore(movie.Title);
            if(existing != null)
            {
                message = "Movie already exists.";
                return null;
            }

            message = null;
            return AddCore(movie);
        }

        /// <summary>Edits an existing movie.</summary>
        /// <param name="movie">The movie to update.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The updated movie.</returns>
        /// <remarks>
        /// Returns an error if movie is null, invalid, movie name
        /// already exists or if the movie cannot be found.
        /// </remarks>
        public Movie Update ( Movie movie, out string message )
        {
            message = "";

            //Check for null
            if (movie == null)
            {
                message = "Movie cannot be null.";
                return null;
            };

            //Validate product
            var errors = movie.Validate();
            if (errors.Count() > 0)
            {
                message = errors.ElementAt(0).ErrorMessage;
                return null;
            };

            // Verify unique product
            var existing = GetMovieByNameCore(movie.Title);
            if (existing != null && existing.Id != movie.Id)
            {
                message = "Movie already exists.";
                return null;
            }

            //Find existing
            existing = existing ?? GetCore(movie.Id);
            if (existing == null)
            {
                message = "Movie not found.";
                return null;
            };
            
            return UpdateCore(movie);
        }

        /// <summary>Gets all movies.</summary>
        /// <returns>The list of movies.</returns>
        public IEnumerable<Movie> GetAll()
        {
            return GetAllCore();
        }

        /// <summary>Removes a movie.</summary>
        /// <param name="id">The movie ID.</param>
        public void Remove ( int id )
        {
            if (id > 0)
            {
                RemoveCore(id);
            };
        }

        protected abstract Movie AddCore( Movie movie );
        protected abstract IEnumerable<Movie> GetAllCore();
        protected abstract Movie GetCore( int id );
        protected abstract void RemoveCore( int id );
        protected abstract Movie UpdateCore( Movie movie );
        protected abstract Movie GetMovieByNameCore( string name );
        
    }
}
