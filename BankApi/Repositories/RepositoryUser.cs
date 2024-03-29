﻿using BankApi.Models;

namespace BankApi.Repositories
{
    public class RepositoryUser
    {
        MyContext myContext = new();

        public RepositoryUser(MyContext context)
        {
           this.myContext = context;
        }

        /// <summary>
        /// Добавление пользоввателя в бд
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public void AddUser(string login, string password) 
        { 
            User user = new User { Login=login, Password = password };

            myContext.User.Add(user);

            myContext.SaveChanges();
        }

        /// <summary>
        /// находит пользователя по логину и паролю
        /// </summary>
        /// <param name="login"></param>
        /// <param name="parol"></param>
        /// <returns></returns>
        public User GetUser(string login, string password) 
        {
            var user = myContext.User.FirstOrDefault(x => x.Login==login && x.Password == password);
            return user;
        }

        /// <summary>
        /// проверка есть ли в бд посльзователь с таким логином
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool PersonExist(string username)
        {
            var isPersonExist = myContext.User.Any(x => x.Login == username);
            return isPersonExist;
        }
    }
}
