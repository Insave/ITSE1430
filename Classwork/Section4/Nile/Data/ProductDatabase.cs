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
        public Product Add ( Product product ) 
        {
            //Check for null
            product = product ?? throw new ArgumentNullException(nameof(product));

            //Validate product
            product.Validate();

            // Verify unique product
            var existing = GetProductByNameCore(product.Name);
            if (existing != null)
                throw new Exception("Product already exists");
            
            return AddCore(product);
        }

        public Product Update ( Product product )
        {
            //Check for null
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            //Validate product
            product.Validate();

            // Verify unique product
            var existing = GetProductByNameCore(product.Name);
            if (existing != null && existing.Id != product.Id)
                throw new Exception("Product already exists");

            //Find existing
            existing = existing ?? GetCore(product.Id);
            if (existing == null)
                throw new ArgumentException("Product not found", nameof(product));
            
            return UpdateCore(product);
        }

        public IEnumerable<Product> GetAll()
        {
            return GetAllCore();
        }

        public void Remove ( int id )
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0");
            
            RemoveCore(id);
        }

        protected abstract Product AddCore( Product product );
        protected abstract IEnumerable<Product> GetAllCore();
        protected abstract Product GetCore( int id );
        protected abstract void RemoveCore( int id );
        protected abstract Product UpdateCore( Product product );
        protected abstract Product GetProductByNameCore( string name );
        
    }
}
