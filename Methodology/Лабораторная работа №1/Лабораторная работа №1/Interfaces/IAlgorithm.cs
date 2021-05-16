using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лабораторная_работа__1.Interfaces
{
    interface IAlgorithm
    {
        void findroute(string from, string to);

        bool routefound();

        string[] route();
    }
}
