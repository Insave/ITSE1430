/*
 * ITSE 1430
 * Lab 5
 */
using System.Collections.Generic;

namespace PatrickFief.MovieLib.Data
{
    /// <summary>Provides access to movies.</summary>
    public interface IMovieDatabase
    {
        /// <summary>Adds a movie to the database.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <returns>The added movie.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is null.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        /// <exception cref="Exception">A movie with the same name already exists.</exception>
        /// <remarks>
        /// Generates an error if:
        /// <paramref name="movie"/> is null or invalid.
        /// A movie with the same name already exists.
        /// </remarks>
        Movie Add( Movie movie );

        /// <summary>Gets all the movies.</summary>
        /// <returns>The list of movies.</returns>
        IEnumerable<Movie> GetAll();

        /// <summary>Removes a movie.</summary>
        /// <param name="id">The ID of the project.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is less than or equal to zero.</exception>
        /// <remarks>
        /// Returns an error if <paramref name="id"/> is less than or
        /// equal to zero.
        /// </remarks>
        void Remove( int id );

        /// <summary>Updates an existing movie in the database.</summary>
        /// <param name="movie">The movie to update.</param>
        /// <returns>The updated movie.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="movie"/> is null.</exception>
        /// <exception cref="ValidationException"><paramref name="movie"/> is invalid.</exception>
        /// <exception cref="Exception">A movie with the same name already exists.</exception>
        /// <exception cref="ArgumentException">The movie does not exist.</exception>
        /// <remarks>
        /// Generates an error if:
        /// <paramref name="movie"/> is null or invalid.
        /// A movie with the same name already exists.
        /// The movie does not exist.
        /// </remarks>
        Movie Update( Movie movie );                
    }
}