using ProjectManagementSystem.Models;
using System.Threading.Tasks;

namespace ProjectManagementSystem
{
	public class WorkingWithTasks : Users
	{
		public void ShowTasks()
		{
			List<Status> statuses = ContextDB.Statuses.ToList();
			List<Employee> users = ContextDB.Employees.ToList();
			List<WorkTask> workTask = ContextDB.WorkTasks.ToList();
			if (User.IdRole != 1)
			{
				workTask = workTask.Where(it => it.IdEmployee == User.Id).ToList();
			}
            foreach (var task in workTask)
            {
				Console.WriteLine($"Задача #{task.Id}");
				Console.WriteLine($"Наименование: {task.Name}");
				Console.WriteLine($"Описание: \n{task.Description}");
				Console.WriteLine($"Статус: {statuses.First(it => it.Id == task.IdStatus).Status1}");
				if (User.IdRole == 1)
				{
					Employee employee = users.First(it => it.Id == task.IdEmployee);
					Console.WriteLine($"Исполнитель: {employee.Name} {employee.Surname}");
				}
			}
            //фильтр/сортировку задач
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
				Console.WriteLine($"{i}. {employees[i].Name}. {employees[i].Surname}");
			}
			Console.Write("Номер исполнителя: ");
			int index = Convert.ToInt32(Console.ReadLine());
			task.IdEmployee = employees[index].Id;
			task.IdStatus = 1;
			ContextDB.WorkTasks.Add(task);
			ContextDB.SaveChanges();
		}
	}
}
