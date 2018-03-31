/*
 * ITSE 1430
 * Patrick Fief
 * Lab 3
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickFief.MovieLib
{
    public static class ObjectValidator
    {
        public static IEnumerable<ValidationResult> Validate ( this IValidatableObject source)
        {
            var context = new ValidationContext(source);
            var errors = new Collection<ValidationResult>();
            Validator.TryValidateObject(source, context, errors, true);

            return errors;
        }
    }
}
