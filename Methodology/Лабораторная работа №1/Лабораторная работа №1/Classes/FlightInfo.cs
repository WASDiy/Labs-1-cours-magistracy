using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лабораторная_работа__1.Classes
{
    class FlightInfo
    {
		public string from; // Пункт отправления
		public string to; // Пункт назначения
		public int distance; // Расстояние от и до
		public bool skip;  // Используется при возврате или откате (бектрекинге)

		public FlightInfo()
		{
			from = "";
			to = "";
			distance = 0;
			skip = false;
		}

		public FlightInfo(string f, string t, int d)
		{
			from = f;
			to = t;
			distance = d;
			skip = false;
		}

	}
}
