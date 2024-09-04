using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem
{
	public class Authorization : Users
	{
		public void СheckAuthorization()
		{
			Console.Write("Введите логин: ");
			string login = Console.ReadLine();
			Console.Write("Введите пароль: ");
			string password = Console.ReadLine();
			Employee? user = GetUserOrNull(login, GetHash(password));
			if (user == null)
			{
				Console.Write("Пароль или логин не верный. Проверьте правильность и попробуйте еще раз.");
				СheckAuthorization();
			}
			else
			{
				User = user;
			}
		}

		Employee? GetUserOrNull(string login, string hashPassword) => ContextDB.Employees.FirstOrDefault(it => it.Login == login && it.Password == hashPassword);

		string GetHash(string input)
		{
			var md5 = MD5.Create();
			var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
			return Convert.ToBase64String(hash);
		}

		public void AddNewUser()
		{
			Employee user = new Employee();
			Console.Write("Введите имя: ");
			user.Name = Console.ReadLine();
			Console.Write("Введите фамилию: ");
			user.Surname = Console.ReadLine();
			Console.Write("Введите логин: ");
			user.Login = Console.ReadLine();
			Console.Write("Введите пароль: ");
			user.Password = GetHash(Console.ReadLine());
			Console.WriteLine("Выберите роль:");
			List<Role> roles = ContextDB.Roles.ToList<Role>();
			roles.ForEach(it => Console.WriteLine($"{it.Id}. {it.Role1}"));
			Console.Write("Номер роли: ");
			user.IdRole = Convert.ToInt32(Console.ReadLine());
			if (GetUserOrNull(user.Login, user.Password) != null)
			{
				Console.WriteLine("Пользователь с данным логином и паролем уже существует");
			}
			else
			{
				ContextDB.Employees.Add(user);
				ContextDB.SaveChanges();
			}
		}
	}
}
