using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data.Memory
{
    /// <summary>Provides an in-memory product database.</summary>
    public class MemoryProductDatabase : ProductDatabase
    {
        protected override Product AddCore ( Product product )
        {
            //Clone the object
            product.Id = _nextID++;
            _products.Add(Clone(product));

            //Return a copy
            return product;
        }

        protected override Product UpdateCore ( Product product )
        {
            var existing = GetCore(product.Id);

            // Clone the object
            //_products[existingIndex] = Clone(product);
            Copy(existing, product);

            //Return a copy
            return product;
        }

        protected override Product GetCore( int id )
        {
            //Option 4 - Combo
            return (from p in _products
                   where p.Id == id
                   select p).FirstOrDefault();

            //Option 3 - LINQ
            //var items = from p in _products
            //            where p.Id == id
            //            select p;
            //return items.FirstOrDefault();

            //Option 2 - extension
            //return _products.FirstOrDefault(p => p.Id == id);

            //Option 1
            //foreach (var product in _products)
            //{
            //    if (product.Id == id)
            //        return product;
            //};

            //return null;
        }

        protected override IEnumerable<Product> GetAllCore()
        {
            //Option 3 LINQ
            return from p in _products
                   select Clone(p);

            //Option 2 extension
            //return _products.Select(p => Clone(p));
            
            //Option 1
            //foreach (var product in _products)
            //{
            //    if (product != null)
            //        yield return Clone(product);
            //}
        }

        protected override void RemoveCore ( int id )
        {
            var existing = GetCore(id);
            if (existing != null)
                _products.Remove(existing);
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

        protected override Product GetProductByNameCore (string name)
        {
            //Option 3 LINQ
            return (from p in _products
                    where String.Compare(p.Name, name, true) == 0
                    select p).FirstOrDefault();

            //Option 2 extension
            //return _products.FirstOrDefault(p => String.Compare(p.Name, name, true) == 0);

            //Option 1
            //foreach (var product in _products)
            //{
            //    if (String.Compare(product.Name, name, true) == 0)
            //        return product;
            //};

            //return null;
        }

        private readonly List<Product> _products = new List<Product>();
        private int _nextID = 1;
    }
}
