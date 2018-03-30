using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickFief.MovieLib.Data.Memory
{
    /// <summary>Provides an in-memory product database.</summary>
    public class MemoryProductDatabase : MovieDatabase
    {
        protected override Movie AddCore ( Movie movie )
        {
            //Clone the object
            movie.Id = _nextID++;
            _movies.Add(Clone(movie));

            //Return a copy
            return movie;
        }

        protected override Movie UpdateCore ( Movie movie )
        {
            var existing = GetCore(movie.Id);

            // Clone the object
            //_products[existingIndex] = Clone(product);
            Copy(existing, movie);

            //Return a copy
            return movie;
        }

        protected override Movie GetCore( int id )
        {
            foreach (var movie in _movies)
            {
                if (movie.Id == id)
                    return movie;
            };

            return null;
        }

        protected override IEnumerable<Movie> GetAllCore()
        {
            foreach (var movie in _movies)
            {
                if (movie != null)
                    yield return Clone(movie);
            }
        }

        protected override void RemoveCore ( int id )
        {
            var existing = GetCore(id);
            if (existing != null)
                _movies.Remove(existing);
        }

        private Movie Clone( Movie item )
        {
            var newMovie = new Movie();
            Copy(newMovie, item);

            return newProduct;
        }

        private void Copy( Movie target, Movie source )
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.IsDiscontinued = source.IsDiscontinued;
        }

        protected override Movie GetMovieByNameCore( string name)
        {
            foreach (var movie in _movies)
            {
                if (String.Compare(movie.Name, name, true) == 0)
                    return movie;
            };

            return null;
        }

        private readonly List<Movie> _products = new List<Movie>();
        private int _nextID = 1;
    }
}
