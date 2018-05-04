/*
 * ITSE 1430
 * Lab 5
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatrickFief.MovieLib
{
    /// <summary>Provides information about a movie.</summary>
    public class Movie : IValidatableObject
    {   
        /// <summary>Gets or sets the movie ID.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description
        {            
            get => _description ?? "";          // { return _description ?? ""; }
            set => _description = value ?? "";  // { _description = value ?? ""; }
        }

        /// <summary>Gets or sets the title.</summary>
        //[RequiredAttribute]
        [Required(AllowEmptyStrings = false)]     
        //[StringLength(1)]
        public string Title
        {
            get => _title ?? "";
            set => _title = value;
        }

        /// <summary>Gets or sets the length.</summary>
        [Range(0, Int32.MaxValue, ErrorMessage = "Length must be >= 0")]
        public int Length { get; set; }

        /// <summary>Determines if the movie is Owned.</summary>
        public bool IsOwned { get; set; }
        
        /// <summary>Validate the movie.</summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The validation results.</returns>
        public IEnumerable<ValidationResult> Validate( ValidationContext validationContext )
        {
            var errors = new List<ValidationResult>();

            return errors;
        }

        #region Private Members

        private string _title;
        private string _description;

        #endregion
    }
}
