using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyFants.Entities;
using MyFants.Models;
using MyFants.Repos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static MyFants.Entities.FBEntities;

using static MyFants.Entities.PGEntities;

namespace MyFants.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IConfiguration _config;

        private readonly CommentsService _commentService;
        private PGRepo _PGRepo;
        private FBRepo _FBRepo;

        private int rnd;
        private DateTime now;

        public ApiController(ILogger<ApiController> logger,
                             IConfiguration config,
                             PGRepo PGRepo,
                             FBRepo FBRepo,
                             CommentsService commentService)
        {
            _logger = logger;
            _config = config;
            _commentService = commentService;
            _PGRepo = PGRepo;
            _FBRepo = FBRepo;
            now = DateTime.Now.ToLocalTime().AddHours(3);
            rnd = (new Random(now.Millisecond)).Next();
        }

        [HttpGet]
        public IActionResult lbh()
        {

            return Content("Это api магазаина игрушек");
        }


        //-----------------------------ЗАПРОСЫ-----------------------------
        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        [HttpGet("getUsers")]
        public IActionResult GetUsers()
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic users = new JArray();
            dynamic response = new JObject();

            var user = _FBRepo.GetUsers();
            foreach (var u in user)
            {
                users.Add(new JObject(
                    new JProperty("ID", u.ID),
                    new JProperty("LOGIN", u.LOGIN),
                    new JProperty("PASSWORD", u.PASSWORD),
                    new JProperty("IS_ADMIN", u.IS_ADMIN)
                    ));
            }

            response.response = users;

            response.status = "OK";

            string respStr = response.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Получить все заказы и логины пользователей, которые их заказали
        /// </summary>
        [HttpGet("getOrdersWithUsers")]
        public IActionResult GetOrdersWithUsers()
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic orders = new JArray();
            dynamic response = new JObject();

            var order = _FBRepo.GetOrdersWithUsers();
            foreach (var or in order)
            {
                orders.Add(new JObject(
                    new JProperty("ID", or.ID),
                    new JProperty("PRICE", or.PRICE),
                    new JProperty("DATATIME", or.DATETIME),
                    new JProperty("ID_USER", or.ID_USER),
                    new JProperty("LOGIN_USER", or.LOGIN_USER)
                    ));
            }

            response.response = orders;

            response.status = "OK";

            string respStr = response.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Получить все товары
        /// </summary>
        [HttpGet("getProducts")]
        public IActionResult GetProducts()
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic products = new JArray();
            dynamic response = new JObject();

            var product = _PGRepo.GetProducts();
            foreach (var pr in product)
            {
                products.Add(new JObject(
                    new JProperty("id", pr.id),
                    new JProperty("name", pr.name),
                    new JProperty("price", pr.price),
                    new JProperty("date_time", pr.date_time),
                    new JProperty("description", pr.description),
                    new JProperty("sellerid", pr.sellerid)
                    ));
            }

            response.response = products;

            response.status = "OK";

            string respStr = response.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Получить все товары, его категорию и его продавца
        /// </summary>
        [HttpGet("getProductsWithCategoryWithSeller")]
        public IActionResult GetProductsWithSeller()
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic products = new JArray();
            dynamic response = new JObject();

            var product = _PGRepo.GetProductWithCategorySeller();
            foreach (var pr in product)
            {
                var user =_FBRepo.GetUserById(pr.sellerid);
                products.Add(new JObject(
                    new JProperty("id", pr.productid),
                    new JProperty("name", pr.name),
                    new JProperty("price", pr.price),
                    new JProperty("date_time", pr.date_time),
                    new JProperty("description", pr.description),
                    new JProperty("category", pr.category),
                    new JProperty("sellerid", pr.sellerid),
                    new JProperty("sellerLOGIN", user.LOGIN),
                    new JProperty("passport", pr.passport)
                    ));
            }

            response.response = products;

            response.status = "OK";

            string respStr = response.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Получить все категории
        /// </summary>
        [HttpGet("getCategories")]
        public IActionResult GetCategories()
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic categories = new JArray();
            dynamic response = new JObject();

            var category = _PGRepo.GetCategories();
            foreach (var pr in category)
            {
                categories.Add(new JObject(
                    new JProperty("id", pr.id),
                    new JProperty("name", pr.name)
                    ));
            }

            response.response = categories;

            response.status = "OK";

            string respStr = response.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Получить все имена товаров
        /// </summary>
        [HttpGet("getProductsList")]
        public IActionResult GetProductsList()
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic resp = new JObject();

            resp.response = new JArray();

            var products = _PGRepo.GetProducts();
            foreach (var pr in products)
            {
                resp.response.Add(new JValue(pr.name));
            }
            resp.status = "OK";

            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Получить товар по имени
        /// </summary>
        [HttpGet("getProductByName")]
        public IActionResult GetProductList([FromQuery(Name = "name")] string name)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic resp = new JObject();
            resp.response = new JObject();

            var product = _PGRepo.GetProductByName(name);
            if (product != null)
            {
                resp.status = "OK";
                resp.response = new JObject(
                    new JProperty("id", product.id),
                    new JProperty("name", product.name),
                    new JProperty("price", product.price),
                    new JProperty("date_time", product.date_time),
                    new JProperty("description", product.description),
                    new JProperty("sellerid", product.sellerid)
                    );
            }
            else
            {
                resp.status = "FAIL";
                resp.response = new JValue("No such product");
            }

            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Авторизация с мобильного приложения
        /// </summary>
        [HttpGet("authorizationMobile")]
        public IActionResult AuthorizationMobile([FromQuery(Name = "LOGIN")] string LOGIN,
                                                 [FromQuery(Name = "PASSWORD")] string PASSWORD)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic resp = new JObject();
            resp.status = "OK";
            resp.response = new JObject();

            var user = _FBRepo.GetUserByLogin(LOGIN);
            if (user == null)
            {
                resp.status = "FAIL";
                resp.response = "WRONG_LOGIN";
            }
            else
            {
                // check password
                if (user.PASSWORD == PASSWORD)
                {
                    resp.status = "OK";
                    resp.response = "OK";
                }
                else
                {
                    resp.status = "FAIL";
                    resp.response = "WRONG_PASSWORD";
                }
            }

            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Получить список комментариев
        /// </summary>
        [HttpGet("getCommentList")]
        public ActionResult GetCommentList()
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            List<Comments> lst = new List<Comments>();
            dynamic comments = new JArray();
            dynamic response = new JObject();

            lst = _commentService.GetAllComments();

            foreach (var comment in lst)
            {
                comments.Add(new JObject(
                        new JProperty("commentid", comment.Id),
                        new JProperty("productid", comment.productid),
                        new JProperty("commetntext", comment.text),
                        new JProperty("commenttime", comment.time)
                    ));
            }


            response.status = "OK";
            response.response = comments;

            string respStr = response.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Получить список комментариев по айди продукта
        /// </summary>
        [HttpGet("getCommentListByProductId")]
        public ActionResult GetCommentListByProductId([FromQuery(Name = "productid")] string productid)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            List<Comments> lst = new List<Comments>();
            dynamic comments = new JArray();
            dynamic response = new JObject();

            lst = _commentService.GetAllCommentsByProductid(productid);

            foreach (var comment in lst)
            {
                comments.Add(new JObject(
                        new JProperty("commentid", comment.Id),
                        new JProperty("productid", comment.productid),
                        new JProperty("commetntext", comment.text),
                        new JProperty("commenttime", comment.time)
                    ));
            }


            response.status = "OK";
            response.response = comments;

            string respStr = response.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Изменить товар по id
        /// </summary>
        [HttpPost("changeProductById")]
        public IActionResult ChangeProductById([FromQuery(Name = "id")] int id, [FromBody] object data)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic req = JObject.Parse(data.ToString());
            dynamic resp = new JObject();
            resp.status = "OK";
            resp.response = new JObject();

            string name = req.name;
            int price = req.price;
            DateTime date_time = req.date_time;
            string description = req.description;
            int sellerid = req.sellerid;

            Product product = new Product();

            product.name = name;
            product.price = price;
            product.date_time = date_time;
            product.description = description;
            product.sellerid = sellerid;

            bool res = _PGRepo.ChangeProductById(id, product);
            if (res == true)
            {
                resp.status = "OK";
                resp.response = "OK";
            }
            else
            {
                resp.status = "FAIL";
                resp.response = "";
            }

            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Изменить товар по имени
        /// </summary>
        [HttpPost("changeProductByIdCategoryByName")]
        public IActionResult ChangeProductByIdCategoryByName([FromQuery(Name = "productid")] int productid, [FromQuery(Name = "categoryname")] string categoryname)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic resp = new JObject();
            resp.status = "OK";
            resp.response = new JObject();
            bool exist = _PGRepo.CheckCategoryProduct(productid, categoryname);
            if (exist)
            {
                resp.status = "FAIL";
                resp.response = "input Category_Product already exists";
            }
            else
            { 
                bool res = _PGRepo.ChangeProductByIdCategoryByName(productid, categoryname);
                if (res == true)
                {
                    resp.status = "OK";
                    resp.response = "OK";
                }
                else
                {
                    resp.status = "FAIL";
                    resp.response = "Not such product or category";
                }
            }
            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Изменить товар по имени
        /// </summary>
        [HttpPost("changeCategoryByName")]
        public IActionResult ChangeCategoryByName([FromQuery(Name = "name")] string nameforchange, [FromBody] object data)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic req = JObject.Parse(data.ToString());
            dynamic resp = new JObject();
            resp.status = "OK";
            resp.response = new JObject();

            string name = req.name;

            Category category = new Category();

            category.name = name;

            bool res = _PGRepo.ChangeCategoryByName(nameforchange, category);
            if (res == true)
            {
                resp.status = "OK";
                resp.response = "OK";
            }
            else
            {
                resp.status = "FAIL";
                resp.response = "No such category";
            }

            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Изменить пользователя по логину
        /// </summary>
        [HttpPost("changeUserByName")]
        public IActionResult ChangeUserByName([FromQuery(Name = "loginforchange")] string loginforchange, [FromBody] object data)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic req = JObject.Parse(data.ToString());
            dynamic resp = new JObject();
            resp.status = "OK";
            resp.response = new JObject();

            string LOGIN = req.LOGIN;
            string PASSWORD = req.PASSWORD;
            bool  IS_ADMIN = req.IS_ADMIN;

            User user = new User();

            user.LOGIN = LOGIN;
            user.PASSWORD = PASSWORD;
            user.IS_ADMIN = IS_ADMIN;
            bool res = _FBRepo.ChangeUserByName(user, loginforchange);
            if (res == true)
            {
                resp.status = "OK";
                resp.response = "OK";
            }
            else
            {
                resp.status = "FAIL";
                resp.response = "No suck user";
            }
            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }
        /// <summary>
        /// Изменить пользователя по айди
        /// </summary>
        [HttpPost("changeUserById")]
        public IActionResult ChangeUserById([FromQuery(Name = "idforchange")] int idforchange, [FromBody] object data)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic req = JObject.Parse(data.ToString());
            dynamic resp = new JObject();
            resp.status = "OK";
            resp.response = new JObject();

            string LOGIN = req.LOGIN;
            string PASSWORD = req.PASSWORD;
            bool IS_ADMIN = req.IS_ADMIN;

            User user = new User();

            user.LOGIN = LOGIN;
            user.PASSWORD = PASSWORD;
            user.IS_ADMIN = IS_ADMIN;
            bool res = _FBRepo.ChangeUserById(user, idforchange);
            if (res == true)
            {
                resp.status = "OK";
                resp.response = "OK";
            }
            else
            {
                resp.status = "FAIL";
                resp.response = "No suck user";
            }
            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Добавить товар
        /// </summary>
        [HttpPost("addProduct")]
        public IActionResult AddProduct([FromBody] object data)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic req = JObject.Parse(data.ToString());
            dynamic resp = new JObject();
            resp.status = "OK";
            resp.response = new JObject();

            string name = req.name;
            int price = req.price;
            DateTime date_time = req.date_time;
            string description = req.description;
            int sellerid = req.sellerid;

            Product product = new Product();

            product.name = name;
            product.price = price;
            product.date_time = date_time;
            product.description = description;
            product.sellerid = sellerid;

            bool res = _PGRepo.AddProduct(product);
            if (res == true)
            {
                resp.status = "OK";
                resp.response = "OK";
            }
            else
            {
                resp.status = "FAIL";
                resp.response = "Seller with id =" + product.sellerid +" not exist";
            }

            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        [HttpPost("addUser")]
        public IActionResult AddUser([FromBody] object data)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic req = JObject.Parse(data.ToString());
            dynamic resp = new JObject();
            resp.status = "OK";
            resp.response = new JObject();

            string LOGIN = req.LOGIN;
            string PASSWORD = req.PASSWORD;
            bool IS_ADMIN = req.IS_ADMIN;

            User user = new User();

            user.LOGIN = LOGIN;
            user.PASSWORD = PASSWORD;
            user.IS_ADMIN = IS_ADMIN;

            bool res = _FBRepo.AddUser(user);
            if (res == true)
            {
                resp.status = "OK";
                resp.response = "OK";
            }
            else
            {
                resp.status = "FAIL";
                resp.response = "";
            }

            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Добавить категорию
        /// </summary>
        [HttpPost("addCategory")]
        public IActionResult AddCategory([FromBody] object data)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic req = JObject.Parse(data.ToString());
            dynamic resp = new JObject();
            resp.status = "OK";
            resp.response = new JObject();

            string name = req.name;

            Category category = new Category();

            category.name = name;

            bool res = _PGRepo.AddCategory(category);
            if (res == true)
            {
                resp.status = "OK";
                resp.response = "OK";
            }
            else
            {
                resp.status = "FAIL";
                resp.response = "";
            }

            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Изменить комментарий к товару по айди
        /// </summary>
        [HttpPost("changeCommentById")]
        public ActionResult ChangeComment([FromQuery(Name = "commentid")] string commentid, [FromBody] object data)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            JsonResponse resp = new JsonResponse();
            dynamic req = JObject.Parse(data.ToString());

            Comments newcomment = new();
            newcomment = _commentService.GetById(commentid);

            newcomment.text = req.text;
            newcomment.time = DateTime.Now.ToString("G");

            bool res =_commentService.Update(commentid, newcomment);
            if (res)
            {
                resp.status = "OK";
                resp.response = "Comment change successfully";
            }
            else
            {
                resp.status = "FAIL";
                resp.response = "Error";
            }

            string respStr = JsonConvert.SerializeObject(resp);

            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr, "application/json");
        }

        /// <summary>
        /// Отправить комментарий к товару
        /// </summary>
        [HttpPost("addComment")]
        public ActionResult AddComment([FromBody] object data)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic req = JObject.Parse(data.ToString());

            Comments comment = new();
            comment.productid = req.productid;
            comment.text = req.text;
            comment.time = DateTime.Now.ToString("G");

            JsonResponse resp = new JsonResponse();

            bool res = _commentService.Create(comment);

            if (res)
            {
                resp.status = "FAIL";
                resp.response = "Comment added successfully";
            }
            else
            {
                resp.status = "FAIL";
                resp.response = "Error";
            }

            string respStr = resp.ToString();


            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr, "application/json");
        }

        /// <summary>
        /// Удалить товар по имени
        /// </summary>
        [HttpDelete("removeCategory")]
        public IActionResult RemoveCategory([FromQuery(Name = "name")] string name, string pass)
        {
            if (pass == "196574839")
            {
                _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

                dynamic resp = new JObject();
                resp.response = new JObject();

                var category = _PGRepo.GetCategoryByName(name);
                if (category != null)
                {
                    resp.status = "OK";
                    resp.response = new JValue("Category id: " + category.id + " Removed");
                    _PGRepo.RemoveCategory(name);
                }
                else
                {
                    resp.status = "FAIL";
                    resp.response = new JValue("No such category");
                }

                string respStr = resp.ToString();
                _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

                return Content(respStr);
            }
            return Content("фиг вам");
        }

        /// <summary>
        /// Удалить товар по id
        /// </summary>
        [HttpDelete("removeProductById")]
        public IActionResult RemoveProduct([FromQuery(Name = "id")] int id, string pass)
        {
            if (pass == "196574839")
            {
                _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

                dynamic resp = new JObject();
                resp.response = new JObject();

                var product = _PGRepo.GetProductByid(id);
                if (product != null)
                {
                    resp.status = "OK";
                    resp.response = new JValue("Product id: " + product.id + " Removed");
                    _PGRepo.RemoveProduct(id);
                }
                else
                {
                    resp.status = "FAIL";
                    resp.response = new JValue("No such product");
                }

                string respStr = resp.ToString();
                _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

                return Content(respStr);
            }
            return Content("фиг вам");
        }

        /// <summary>
        /// Удалить пользователя по логину
        /// </summary>
        [HttpDelete("removeUserByName")]
        public IActionResult RemoveUserByName([FromQuery(Name = "loginforremove")] string loginforremove)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic resp = new JObject();
            resp.status = "OK";
            resp.response = new JObject();

            bool res = _FBRepo.RemoveUserByName(loginforremove);
            if (res == true)
            {
                resp.status = "OK";
                resp.response = "OK";
            }
            else
            {
                resp.status = "FAIL";
                resp.response = "No suck user";
            }
            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Удалить пользователя по айди
        /// </summary>
        [HttpDelete("removeUserById")]
        public IActionResult RemoveUserById([FromQuery(Name = "idforremove")] int idforremove)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            dynamic resp = new JObject();
            resp.status = "OK";
            resp.response = new JObject();

            bool res = _FBRepo.RemoveUserById(idforremove);
            if (res == true)
            {
                resp.status = "OK";
                resp.response = "OK";
            }
            else
            {
                resp.status = "FAIL";
                resp.response = "No such user";
            }
            string respStr = resp.ToString();
            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr);
        }

        /// <summary>
        /// Удалить комментарий к товару по айди
        /// </summary>
        [HttpDelete("removeCommentById")]
        public ActionResult RemoveComment([FromQuery(Name = "commentid")] string commentid)
        {
            _logger.LogInformation($"Req id:{rnd}\n Time: {now} Method: {System.Reflection.MethodInfo.GetCurrentMethod()}\n");

            JsonResponse resp = new JsonResponse();

            bool res = _commentService.Remove(commentid);

            if (res)
            {
                resp.status = "OK";
                resp.response = "Comment Removed";
            }
            else
            {
                resp.status = "FAIL";
                resp.response = "Comment Remove ERROR";
            }

            string respStr = resp.response.ToString();

            _logger.LogInformation($"Req id:{rnd}\n Time: {now}\n Resp: {respStr}\n");

            return Content(respStr, "application/json");
        }
        //-----------------------------Конец-----------------------------
    }
}
