using ProjectManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementSystem
{
	public class WorkingWithTasks : Users
	{
		List<WorkTask> WorkTask = new List<WorkTask>();
		List<Status> Statuses => ContextDB.Statuses.ToList();
		List<Employee> Users => ContextDB.Employees.ToList();

		/// <summary>
		/// Получение доступных задач текущего пользователя
		/// </summary>
		public void GetTasks() => WorkTask = ContextDB.WorkTasks.ToList();
		public void ShowTasksAdmin()
		{
			GetTasks();
			PrintTasks(WorkTask);
			Console.WriteLine("\nЖелаете отсортировать задачи?");
			Console.WriteLine("1. Да");
			Console.WriteLine("2. Нет");
			int answer = Convert.ToInt32(Console.ReadLine());
			if (answer == 1)
			{
				SortTasks();
			}
		}
		public void ShowTasks()
		{
			GetTasks();
			WorkTask = WorkTask.Where(it => it.IdEmployee == User.Id).ToList();
			PrintTasks(WorkTask);
		}
		public void PrintTasks(List<WorkTask> tasks)
		{
			for (int i = 0; i < tasks.Count; i++)
			{
				Console.WriteLine($"\nЗадача #{i + 1}");
				Console.WriteLine($"Наименование: {tasks[i].Name}");
				Console.WriteLine($"Описание: \n{tasks[i].Description}");
				Console.WriteLine($"Статус: {Statuses.First(it => it.Id == WorkTask[i].IdStatus).Status1}");
				if (User.IdRole == 1)
				{
					Employee employee = Users.First(it => it.Id == WorkTask[i].IdEmployee);
					Console.WriteLine($"Исполнитель: {employee.Name} {employee.Surname}");
				}
			}
		}
		public void AddNewTasks()
		{
			WorkTask task = new WorkTask();
			Console.Write("Введите название задачи: ");
			task.Name = Console.ReadLine();
			Console.Write("Введите описание задачи: ");
			task.Description = Console.ReadLine();
			Console.Write("Выберите исполнителя: ");
			List<Employee> employees = ContextDB.Employees.Where(it => it.IdRole != 1).ToList();
			for (int i = 0; i < employees.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {employees[i].Name} {employees[i].Surname}");
			}
			Console.Write("Номер исполнителя: ");
			int index = Convert.ToInt32(Console.ReadLine()) - 1;
			task.IdEmployee = employees[index].Id;
			task.IdStatus = 1;
			ContextDB.WorkTasks.Add(task);
			ContextDB.SaveChanges();
			Console.WriteLine("Новая задача создана!");
			ShowTasks();
		}
		public void ChangeStatusTasks()
		{
			List<Status> statuses = ContextDB.Statuses.ToList();
			Console.Write("Введите номер задачи: ");
			int index = Convert.ToInt32(Console.ReadLine()) - 1;
			Console.WriteLine("Выберите новый статус:");
			statuses.ForEach(it => Console.WriteLine($"{it.Id}. {it.Status1}"));
			Console.Write("Номер статуса: ");
			WorkTask[index].IdStatus = Convert.ToInt32(Console.ReadLine());
			ContextDB.SaveChanges();
			Console.WriteLine("Статус задачи изменен!");
			ShowTasks();
		}
		public void SortTasks()
		{
			List<WorkTask> sortingTasks = new List<WorkTask>();
			GetTasks();
			Console.WriteLine("\nПо какому параметру отсортировать результаты?");
			Console.WriteLine("1. Название");
			Console.WriteLine("2. Статус");
			int param = Convert.ToInt32(Console.ReadLine());
			switch (param)
			{
				case 1: sortingTasks = WorkTask.OrderBy(it => it.Name).ToList();  break;
				case 2: sortingTasks = WorkTask.OrderBy(it => it.IdStatus).ToList();  break;
			}
			PrintTasks(sortingTasks);
		}
	}
}
