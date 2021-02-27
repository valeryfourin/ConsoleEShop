﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleEShop
{
    interface ICheck
    {
        bool CheckField(ref string field);
        bool CheckEmail(string email);
        bool CheckLogin(string login);
        bool CheckEnter(string login, string password);
    }
    interface IUser
    {
        void SearchProduct();
        void ViewProductsList();
    }
    interface IGuest : IUser
    {
        void Registration();
        bool Enter();
    }

    class Register
    {
        Checker checker = new Checker();
        public string InputName()
        {
            Console.WriteLine("Введіть своє ім'я:");
            return Console.ReadLine();
        }

        public string InputSurname()
        {
            Console.WriteLine("Введіть своє прізвище:");
            return Console.ReadLine();
        }

        public string InputEmail()
        {
            string email;
            while (true)
            {
                Console.WriteLine("Введіть свій email:");
                email = Console.ReadLine();
                if (checker.CheckEmail(email))
                {
                    Console.WriteLine("Введений email вже використовується");
                }
                break;
            }
            return email;
        }

        public string InputLogin()
        {
            string login;
            while (true)
            {
                Console.WriteLine("Введіть свій login:");
                login = Console.ReadLine();
                if (checker.CheckLogin(login))
                {
                    Console.WriteLine("Введений login вже використовується");
                }
                break;
            }
            return login;
        }

        public string InputPassword()
        {
            Console.WriteLine("Вигадайте свій пароль:");
            return Console.ReadLine();
        }
    }
    class Checker : ICheck
    {
        public bool CheckField(ref string field)  // Можливий нескінченний цикл нада перевірити буде
        {
            while (true)
            {
                if(field != "")
                {
                    return true;
                }
                Console.WriteLine("Поле не може бути пустим");
                field = Console.ReadLine();
            }
        }
        public bool CheckEmail(string email)
        {
            
            for (int i = 0; i < UsersLocalDB.GetRegistredGuests.Count; i++)
            {
                if (UsersLocalDB.GetRegistredGuests[i].Email == email)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckEnter(string login, string password)
        {
            for (int i = 0; i < UsersLocalDB.GetRegistredGuests.Count; i++)
            {
                if (UsersLocalDB.GetRegistredGuests[i].Login == login && UsersLocalDB.GetRegistredGuests[i].Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckLogin(string login)
        {
            for (int i = 0; i < UsersLocalDB.GetRegistredGuests.Count; i++)
            {
                if (UsersLocalDB.GetRegistredGuests[i].Login == login)
                {
                    return true;
                }
            }
            return false;
        }
    }
    static class UsersLocalDB
    {
        static private List<RegistredGuest> registredGuests = new List<RegistredGuest>();
        static public List<RegistredGuest> GetRegistredGuests
        {
            get
            {
                return registredGuests;
            }
        }
        static public void Add(RegistredGuest guest)
        {
            registredGuests.Add(guest);
        }
    }

    static class ProductsLocalDB
    {
        static private List<Product> productsDB = new List<Product>();
        static public List<Product> GetProducts
        {
            get
            {
                return productsDB;
            }
        }
        static public void Add(Product product)
        {
            productsDB.Add(product);
        }

    }

    class Product
    {
        public string Name { set; get; }
        public string Category { set; get; }
        public string Description { set; get; }
        public decimal Cost { set; get; }

        public Product(string name, string category, string description, decimal cost)
        {
            Name = name;
            Category = category;
            Description = description;
            Cost = cost;
        }
        public override string ToString()
        {
            return $"{Name},{Category},{Cost}\n{Description}";
        }
    }

    class Guest : IGuest
    {
        Checker checker = new Checker();
        Register register = new Register();
        public void Registration()
        {
            RegistredGuest registredGuest = new RegistredGuest(
                register.InputName(),
                register.InputSurname(),
                register.InputEmail(),
                register.InputLogin(),
                register.InputPassword());
            UsersLocalDB.Add(registredGuest);
        }
        public bool Enter()
        {
            Console.WriteLine("Вхід у систему, введіть логін:");
            string login = Console.ReadLine();  // додати перевірку на пустоту поля
            checker.CheckField(ref login);
            Console.WriteLine("Вхід у систему, введіть пароль:");
            string password = Console.ReadLine(); // додати перевірку на пустоту поля
            checker.CheckField(ref password);
            if (checker.CheckEnter(login, password))
            {
                Console.WriteLine($"Ви увійшли на сайт як {login}");
                return true;
            }
            return false;
        }

        public void ViewProductsList()
        {
            
            for (int i = 0; i < ProductsLocalDB.GetProducts.Count; i++)
            {
                Console.WriteLine($"{i}){ProductsLocalDB.GetProducts[i]}");
            }
        }
        public void SearchProduct()
        {
            string searched = Console.ReadLine();
            for (int i = 0; i < ProductsLocalDB.GetProducts.Count; i++)
            {
                if(searched == ProductsLocalDB.GetProducts[i].Name)
                {
                    Console.WriteLine($"{i}){ProductsLocalDB.GetProducts[i]}");
                }
            }
        }
    }
    class RegistredGuest : Guest
    {
        public string Name { set; get; }
        public string Lastname { set; get; }
        public string Email { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
        public RegistredGuest(string name, string lastname, string email, string login, string password)
        {
            Name = name;
            Lastname = lastname;
            Email = email;
            Login = login;
            Password = password;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
