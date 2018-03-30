using System.Collections.Generic;

namespace PatrickFief.MovieLib.Data
{
    public interface IMovieDatabase
    {
        Movie Add( Movie movie, out string message );
        Movie Update( Movie movie, out string message );
        IEnumerable<Movie> GetAll();
        void Remove( int id );
    }
}