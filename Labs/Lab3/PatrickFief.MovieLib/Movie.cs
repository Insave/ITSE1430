/*
 * ITSE 1430
 * Patrick Fief
 * Lab 3
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatrickFief.MovieLib
{
    /// <summary>Provides infromation about a Movie.</summary>
    public class Movie : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate( ValidationContext validationContext )
        {
            throw new NotImplementedException();
        }

        private string _title;
        private string _description;
    }
}
