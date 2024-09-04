using ProjectManagementSystem.Models;

namespace ProjectManagementSystem
{
	public class Users
	{
		static WorkingWithTasks WorkingWithTasks = new WorkingWithTasks();
		static Authorization Auth = new Authorization();
		public static PmsContext ContextDB = new PmsContext();
		public static Employee User = new Employee();
		public void StartSession()
		{
			Console.WriteLine("Система управления проектом");
			Authorization auth = new Authorization();
			auth.СheckAuthorization();
			ShowMenu(User.IdRole);
		}
		public void ShowMenu(int role)
		{
			Console.Clear();
			Console.WriteLine("Выберите нужное действие: ");
			switch (role)
			{
				case 1: ShowAdminMenu(); break;
				case 2: ShowUserMenu(); break;
			}
		}
		public void ShowAdminMenu()
		{
			ConsoleKeyInfo end;
			do
			{
				Console.Clear();
				Console.WriteLine("1. Посмотреть список задач");
				Console.WriteLine("2. Добавить задачу и назначить исполнителя");
				Console.WriteLine("3. Зарегестрировать нового пользователя");
				int answer = Convert.ToInt32(Console.ReadLine());
				switch (answer)
				{
					case 1: WorkingWithTasks.ShowTasks(); break;
					case 2: WorkingWithTasks.AddNewTasks(); break;
					case 3: Auth.AddNewUser(); break;
					default: Console.WriteLine("Такого действия нет"); break;
				}
				Console.WriteLine("\n Нажмите любую клавишу для продолжения, Esc - вызов меню");
				end = Console.ReadKey();
			} while (end.Key != ConsoleKey.Escape);
		}
		public void ShowUserMenu()
		{
			ConsoleKeyInfo end;
			do
			{
				Console.Clear();
				Console.WriteLine("Ваши задачи:");
				WorkingWithTasks.ShowTasks();
				Console.WriteLine("\nВыберите действие:");
				Console.WriteLine("1. Изменить статус задачи");
				Console.WriteLine("2. Отсортировать задачи");
				int answer = Convert.ToInt32(Console.ReadLine());
				switch (answer)
				{
					case 1: WorkingWithTasks.ChangeStatusTasks(); break;
					case 2: WorkingWithTasks.SortTasks(); break;
					default: Console.WriteLine("Такого действия нет"); break;
				}
				Console.WriteLine("\n Нажмите любую клавишу для продолжения, Esc - вызов меню");
				end = Console.ReadKey();
			} while (end.Key != ConsoleKey.Escape);
		}
	}
}
