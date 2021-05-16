using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Лабораторная_работа__1.Interfaces;


namespace Лабораторная_работа__1.Classes
{
	class LeastCost : IAlgorithm
	{
		const int MAXDIST = 100000;

		// Вектор содержит информацию о рейсах
		List<FlightInfo> flights = new List<FlightInfo>();

		// Стек используется для возврата
		Stack<FlightInfo> btStack = new Stack<FlightInfo>();

		public LeastCost(List<FlightInfo> f)
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
			int pos = -1;
			int dist = MAXDIST; //длиннее, чем самый длинный рейс
			for (int i = 0; i < flights.Count; i++)
			{
				if (flights[i].from == from && !flights[i].skip)
				{
					//Использует самый короткий рейс
					if (flights[i].distance < dist)
					{
						pos = i;
						dist = flights[i].distance;
					}
				}
			}
			if (pos != -1)
			{
				f = flights[pos];
				flights[pos].skip = true; //Предотвращает повторное использование
				return true;
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
				routeStr += f.from + " - ";
				dist += f.distance;
			}

			routeStr += f.to;

			return new string[2] { routeStr, dist.ToString() };
		}

		//Определяет есть ли маршрут между from и to
		public void findroute(string from, string to)
		{
			int dist = 0;
			FlightInfo f = new FlightInfo();

			//Проверяет не достигнута ли цель
			if (match(from, to, ref dist))
			{
				btStack.Push(new FlightInfo(from, to, dist));
				return;
			}

			//Пробует другой маршрут
			if (find(from, ref f))
			{
				btStack.Push(new FlightInfo(from, to, f.distance));
				findroute(f.to, to);
			}
			else if (!(btStack.Count == 0))
			{
				//Поднимается на уровень вверх и проверяет другой маршрут
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
	}
}
