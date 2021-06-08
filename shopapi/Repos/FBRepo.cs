using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebirdSql.EntityFrameworkCore.Firebird;
using Microsoft.EntityFrameworkCore;
using MyFants.Entities;
using static MyFants.Entities.FBEntities;

namespace MyFants.Repos
{
	public class FBRepo
	{
		public FBEntities _ctx;
        //Получить всех пользователей
        public List<User> GetUsers()
        {
            return _ctx.USERS.ToList();
        }
        //Добавить пользователя
        public bool AddUser(User user)
		{
			try
			{
				var _user = new User();

				_user.LOGIN = user.LOGIN;
				_user.PASSWORD = user.PASSWORD;
				_user.IS_ADMIN = user.IS_ADMIN;

				_ctx.USERS.Add(_user);
				_ctx.SaveChanges();
			}
			catch (Exception e)
			{
				return false;
			}
			return true;
		}
        //Изменить пользователя по имени
        public bool ChangeUserByName(User user, string loginforchange)
        {
            try
            {
                var _user = _ctx.USERS.FirstOrDefault(u => u.LOGIN == loginforchange);

                _user.LOGIN = user.LOGIN;
                _user.PASSWORD = user.PASSWORD;
                _user.IS_ADMIN = user.IS_ADMIN;

                _ctx.USERS.Update(_user);
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        //Изменить пользователя по айди
        public bool ChangeUserById(User user, int idforchange)
        {
            try
            {
                var _user = _ctx.USERS.FirstOrDefault(u => u.ID == idforchange);

                _user.LOGIN = user.LOGIN;
                _user.PASSWORD = user.PASSWORD;
                _user.IS_ADMIN = user.IS_ADMIN;

                _ctx.USERS.Update(_user);
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        //Удалить пользователя по имени
        public bool RemoveUserByName(string loginforremove)
        {
            try
            {
                var _user = _ctx.USERS.FirstOrDefault(u => u.LOGIN == loginforremove);
                _ctx.USERS.Remove(_user);
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        //Удалить пользователя по айди
        public bool RemoveUserById(int idforremove)
        {
            try
            {
                var _user = _ctx.USERS.FirstOrDefault(u => u.ID == idforremove);
                _ctx.USERS.Remove(_user);
                _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        //Получить пользователя по имени
        public User GetUserByLogin(string login)
        {
                return _ctx.USERS.FirstOrDefault(u => u.LOGIN == login);
        }
        //Получить пользователя по айди
        public User GetUserById(int ID)
        {
            return _ctx.USERS.FirstOrDefault(u => u.ID == ID);
        }

        //Получить все заказы и логин пользователя который их заказал
        public List<OrderWithUser> GetOrdersWithUsers()
        {
            var OrWithUs = from ord in _ctx.ORDERS
                           join us in _ctx.USERS on ord.ID_USER equals us.ID
                           select new OrderWithUser
                           {
                            ID = ord.ID,
                            PRICE = ord.PRICE,
                            DATETIME = ord.DATETIME,
                            ID_USER = ord.ID_USER,
                            LOGIN_USER = us.LOGIN
                           };
            return OrWithUs.ToList();
        }

    public FBRepo(FBEntities ctx)
        {
            _ctx = ctx;
        }
    }
}
