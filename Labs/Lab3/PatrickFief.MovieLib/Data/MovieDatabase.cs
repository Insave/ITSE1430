using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickFief.MovieLib.Data
{
    /// <summary>Provides an abstract product database.</summary>
    public abstract class MovieDatabase : IMovieDatabase
    {
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

        public Movie Update ( Movie movie, out string message )
        {
            message = "";

            //Check for null
            if (movie == null)
            {
                message = "Product cannot be null.";
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
            var existing = GetMovieByNameCore(movie.Name);
            if (existing != null && existing.Id != movie.Id)
            {
                message = "Product already exists.";
                return null;
            }

            //Find existing
            existing = existing ?? GetCore(movie.Id);
            if (existing == null)
            {
                message = "Product not found.";
                return null;
            };
            
            return UpdateCore(movie);
        }

        public IEnumerable<Movie> GetAll()
        {
            return GetAllCore();
        }

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
