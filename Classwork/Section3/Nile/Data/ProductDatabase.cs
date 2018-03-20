using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data
{
    /// <summary>Provides an abstract product database.</summary>
    public abstract class ProductDatabase : IProductDatabase
    {
        public Product Add ( Product product, out string message )
        {
            //Check for null
            if (product == null)
            {
                message = "Product cannot be null.";
                return null;
            };

            //Validate product
            var errors = ObjectValidator.Validate(product);
            //var error = product.Validate();
            if (errors.Count() > 0)
            {
                message = errors.ElementAt(0).ErrorMessage;
                return null;
            };

            // Verify unique product
            var existing = GetProductByNameCore(product.Name);
            if(existing != null)
            {
                message = "Product already exists.";
                return null;
            }

            message = null;
            return AddCore(product);
        }

        public Product Update ( Product product, out string message )
        {
            message = "";

            //Check for null
            if (product == null)
            {
                message = "Product cannot be null.";
                return null;
            };

            //Validate product
            var errors = ObjectValidator.Validate(product);
            if (errors.Count() > 0)
            {
                message = errors.ElementAt(0).ErrorMessage;
                return null;
            };

            // Verify unique product
            var existing = GetProductByNameCore(product.Name);
            if (existing != null && existing.Id != product.Id)
            {
                message = "Product already exists.";
                return null;
            }

            //Find existing
            existing = existing ?? GetCore(product.Id);
            if (existing == null)
            {
                message = "Product not found.";
                return null;
            };
            
            return UpdateCore(product);
        }

        public IEnumerable<Product> GetAll()
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

        protected abstract Product AddCore( Product product );
        protected abstract IEnumerable<Product> GetAllCore();
        protected abstract Product GetCore( int id );
        protected abstract void RemoveCore( int id );
        protected abstract Product UpdateCore( Product product );
        protected abstract Product GetProductByNameCore( string name );
        
    }
}
