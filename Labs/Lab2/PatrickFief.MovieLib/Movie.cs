/*
 * ITSE 1430
 * Patrick Fief
 * Lab 2
 */
using System;

namespace PatrickFief.MovieLib
{
    /// <summary>Provides infromation about a Movie.</summary>
    public class Movie
    {
        /// <summary>The title of the movie</summary>
        public string Title
        {
            get { return _title ?? ""; }
            set { _title = value ?? ""; }
        }

        /// <summary>A description of the movie</summary>
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value ?? ""; }
        }

        /// <summary>Length of the movie in minutes</summary>
        public int Length {get; set;} = 0;

        /// <summary>Whether or not the movie is owned</summary>
        public bool IsOwned { get; set; }

        /// <summary>Validates the product.</summary>
        /// <returns>Error message, if any.</returns>
        public string Validate()
        {
            //Name is required
            if (String.IsNullOrEmpty(_title))
                return "Title cannot be empty";

            //Length >= 0
            if (Length < 0)
                return "Length must be >= 0 minutes";

            return "";
        }

        private string _title;
        private string _description;
    }
}
