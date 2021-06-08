using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFants.Entities;
using static MyFants.Entities.PGEntities;

namespace MyFants.Repos
{
    public class PGRepo
    {
        public PGEntities _ctx;
        public PGRepo(PGEntities ctx)
        {
            _ctx = ctx;
        }

        //-----------------------------ЗАПРОСЫ-------------------------------
        //ПРОДУКТЫ
        //Получить продукт по имени
        public Product GetProductByName(string name)
        {
            return _ctx.products.Include(pr=>pr.Seller).FirstOrDefault(pr => pr.name == name);
        }
        //Получить продукт по id
        public Product GetProductByid(int id)
        {
            return _ctx.products.Include(pr => pr.Seller).FirstOrDefault(pr => pr.id == id);
        }
        //Получить всю информаци по всем продуктам
        public List<Product> GetProducts()
        {
            return _ctx.products.Include(p => p.Seller).ToList();
        }
        //Изменить продукт по имени
        public bool ChangeProductById(int id, Product product)
            {
            try
            {
                var _product = _ctx.products.FirstOrDefault(pr => pr.id == id);

                _product.name = product.name == null ? _product.name : product.name;
                _product.price = product.price;
                _product.date_time = product.date_time;
                _product.description = product.description == null ? _product.description : product.description;
                _product.sellerid = product.sellerid;

                _ctx.products.Update(_product);
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        //Добавить продукт
        public bool AddProduct(Product product)
        {
            try
            {
                var _product = new Product();

                _product.name = product.name;
                _product.price = product.price;
                _product.date_time = product.date_time;
                _product.description = product.description == null ? _product.description : product.description;
                _product.sellerid = product.sellerid;

                _ctx.products.Add(_product);
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        //Удалить продукт
        public bool RemoveProduct(int id)
        {
            try
            {
                var _product = _ctx.products.FirstOrDefault(pr => pr.id == id);

                _ctx.products.Remove(_product);
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        //КАТЕГОРИИ
        //Получить всю информаци по всем категориям
        public List<Category> GetCategories()
        {
            return _ctx.categories.ToList();
        }
        //Получить категорию по id
        public Category GetCategoryByName(string name)
        {
            return _ctx.categories.FirstOrDefault(c => c.name == name);
        }
        //Добавить категорию
        public bool AddCategory (Category category)
        {
            try
            {
                var _category = new Category();

                _category.name = category.name;

                _ctx.categories.Add(_category);
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        //Удалить категорию по имени
        public bool RemoveCategory(string name)
        {
            try
            {
                var _category = _ctx.categories.FirstOrDefault(c => c.name == name);

                _ctx.categories.Remove(_category);
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        //Изменить категорию по имени
        public bool ChangeCategoryByName(string name, Category category)
        {
            try
            {
                var _category = _ctx.categories.FirstOrDefault(c => c.name == name);

                _category.name = category.name == null ? _category.name : category.name;

                _ctx.categories.Update(_category);
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        //Получить продукт, его категори, его продавца(id)
        public List<ProductWithCategorySeller> GetProductWithCategorySeller()
        {
            var PrWithCaWithSel = from pr in _ctx.products
                           join cap in _ctx.category_product on pr.id equals cap.productid
                           join ca in _ctx.categories on cap.categoryid equals ca.id
                           join s in _ctx.sellers on pr.sellerid equals s.id
                           select new ProductWithCategorySeller
                           {
                               productid = pr.id,
                               name = pr.name,
                               price = pr.price,
                               date_time = pr.date_time,
                               description = pr.description,
                               category = ca.name,
                               sellerid = pr.sellerid,
                               passport = s.passport
                           };
            return PrWithCaWithSel.ToList();
        }
        //Изменить категорию товара по айди
        public bool ChangeProductByIdCategoryByName(int productid, string categoryname)
        {
            try
            {
                var _category = _ctx.categories.FirstOrDefault(c => c.name == categoryname);
                var _product = _ctx.products.FirstOrDefault(pr => pr.id == productid);
                var _catprod = new Category_Product();

                _catprod.productid = _product.id;
                _catprod.categoryid = _category.id;

                _ctx.category_product.Update(_catprod);
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        //Проверка присвоения категории товара
        public bool CheckCategoryProduct(int productid, string categoryname)
        {
            try
            {
                var cheking = _ctx.category_product.Where(ch => ch.productid == productid);
                var _categoryname = _ctx.categories.FirstOrDefault(cat=>cat.name == categoryname);
                foreach (Category_Product item in cheking)
                {
                    if (item.categoryid == _categoryname.id) return true;
                }
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        //-----------------------------Конец-----------------------------
    }
}
