using ProjectManagementSystem.Models;

namespace ProjectManagementSystem
{
	public class Users
	{
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
				WorkingWithTasks workingWithTasks = new WorkingWithTasks();
				Authorization auth = new Authorization();
				Console.WriteLine("1. Посмотреть список задач");
				Console.WriteLine("2. Добавить задачу и назначить исполнителя");
				Console.WriteLine("3. Зарегестрировать нового пользователя");
				int answer = Convert.ToInt32(Console.ReadLine());
				switch (answer)
				{
					case 1: workingWithTasks.ShowTasks(); break;
					case 2: workingWithTasks.AddNewTasks(); break;
					case 3: auth.AddNewUser(); break;
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
				Console.WriteLine("1. Изменить статус задачи");
				Console.WriteLine("2. Отсортировать задачи");
				int answer = Convert.ToInt32(Console.ReadLine());
				switch (answer)
				{
					//case 1: ChangeStatusTasks(); break;
					//case 2: SortTask(); break;
					default: Console.WriteLine("Такого действия нет"); break;
				}
				Console.WriteLine("\n Нажмите любую клавишу для продолжения, Esc - вызов меню");
				end = Console.ReadKey();
			} while (end.Key != ConsoleKey.Escape);
		}
	}
}
