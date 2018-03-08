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
            var existing = GetProductByName(product.Name);
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
            var existing = GetProductByName(product.Name);
            if (existing != null && existing.Id != product.Id)
            {
                message = "Product already exists.";
                return null;
            }

            //Find existing
            existing = existing ?? GetById(product.Id);
            if (existing == null)
            {
                message = "Product not found.";
                return null;
            };

            //Clone the object
            //_products[existingIndex] = Clone(product);
            Copy(existing, product);
            message = null;

            //Return a copy
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return GetAllCore();
        }

        public void Remove ( int id )
        {
            if (id > 0)
            {
                var existing = GetById(id);
                if (existing != null)
                    _products.Remove(existing);
            };
        }

        private Product Clone(Product item)
        {
            var newProduct = new Product();
            Copy(newProduct, item);

            return newProduct;
        }

        private void Copy(Product target, Product source)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.IsDiscontinued = source.IsDiscontinued;
        }

        //private int FindEmptyProductIndex()
        //{
        //    for (var index = 0; index < _products.Length; ++index)
        //    {
        //        if (_products[index] == null)
        //            return index;
        //    };

        //    return -1;
        //}

        private Product GetById ( int id )
        {
            //for (var index = 0; index < _products.Length; ++index)
            foreach (var product in _products)
            {
                if (product.Id == id)
                    return product;
            };

            return null;
        }

        private Product GetProductByName (string name)
        {
            foreach (var product in _products)
            {
                if (String.Compare(product.Name, name, true) == 0)
                    return product;
            };

            return null;
        }

        protected abstract Product AddCore( Product product );
        protected abstract IEnumerable<Product> GetAllCore();
        protected abstract Product GetCore( int id );

        private readonly List<Product> _products = new List<Product>();
        private int _nextID = 1;
    }
}
