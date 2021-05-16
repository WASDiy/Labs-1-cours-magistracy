using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Лабораторная_работа__1.Interfaces;

namespace Лабораторная_работа__1.Classes
{
	class BreadthFirstSearch : IAlgorithm
	{
		//Вектор содержит информацию о рейсах
		List<FlightInfo> flights = new List<FlightInfo>();

		//Стек используется для возврата
		Stack<FlightInfo> btStack = new Stack<FlightInfo>();

		public BreadthFirstSearch(List<FlightInfo> f)
		{
			flights = f;
		}

		//Если есть рейс отfrom и до to,
		//то в dist запоминается расстояние.
		//Возвращает true, если рейс существует, b
		//false - в противном случае
		bool match(string from, string to, ref int dist)
        {
			for (int i = 0; i < flights.Count; i++)
			{
				if (flights[i].from == from && flights[i].to == to && !flights[i].skip)
				{
					flights[i].skip = true; //Препятствует повторному использованию
					dist = flights[i].distance;
					return true;
				}
			}
			return false; //Не найден
		}

		//При заданном from ищет любой прямой рейс (соединение).
		//Возвращает true, если рейс найден,
		//и false - в противном случае.
		bool find(string from, ref FlightInfo f)
        {
			for (int i = 0; i < flights.Count; i++)
			{
				if (flights[i].from == from && !flights[i].skip)
				{
					f = flights[i];
					flights[i].skip = true; //Препятствует повторному использованию
					return true;
				}
			}
			return false;
		}

		//Показывает маршрут и общее расстояние
		public string[] route()
		{
			Stack<FlightInfo> rev = new Stack<FlightInfo>();
			int dist = 0;
			FlightInfo f = new FlightInfo();

			//Для отображения маршрута меняет порядок стека на противоположный
			while (!(btStack.Count == 0))
			{
				f = btStack.Peek();
				rev.Push(f);
				btStack.Pop();
			}

			string routeStr = "";

			//Отображает маршрут
			while (!(rev.Count == 0))
			{
				f = rev.Peek();
				rev.Pop();
				//cout << f.from << " to ";
				routeStr += f.from + " - ";
				dist += f.distance;
			}
			//cout << f.to << endl;
			//cout << "Distance is " << dist << endl;

			routeStr += f.to;

			return new string[2] { routeStr, dist.ToString() };
		}

		//Определяет есть ли маршрут между from и to
		public void findroute(string from, string to)
        {
			int dist = 0;
			FlightInfo f = new FlightInfo();

			//Стек нужен при поиске в ширину
			Stack<FlightInfo> resetStck = new Stack<FlightInfo>();

			//Проверяет, есть ли рейс до пункта назначения
			if (match(from, to, ref dist))
			{
				btStack.Push(new FlightInfo(from, to, dist));
				return;
			}

			//Проверяются рейсы из заданного узла
			while (find(from, ref f))
			{
				resetStck.Push(f);
				if (match(f.to, to, ref dist))
				{
					resetStck.Push(new FlightInfo());
					btStack.Push(new FlightInfo(from, f.to, f.distance));
					btStack.Push(new FlightInfo(f.to, to, dist));
					return;
				}
			}

			//Переустанавливаются поля skip, установленные
			//в предшествующем цикле while. Это изменение
			//необходимо для поиска в ширину
			while (resetStck.Count == 0)
			{
				resetSkip(resetStck.Peek());
				resetStck.Pop();
			}

			//Проверяет следующий рейс
			if (find(from, ref f))
			{
				btStack.Push(new FlightInfo(from, to, f.distance));
				findroute(f.to, to);
			}
			else if (!(btStack.Count == 0))
			{
				//Откат и проверка другого рейса
				f = btStack.Peek();
				btStack.Pop();
				findroute(f.from, f.to);
			}
		}

		//Возвращает true, если маршрут был найден
		public bool routefound()
		{
			return !(btStack.Count == 0);
		}

		//Переустановка полей skip в векторе flights
		void resetSkip(FlightInfo f)
        {
			for (int i = 0; i < flights.Count; i++)
				if (flights[i].from == f.from && flights[i].to == f.to)
					flights[i].skip = false;
		}
	};
}
