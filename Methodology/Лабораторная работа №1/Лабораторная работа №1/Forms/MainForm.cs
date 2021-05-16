using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Лабораторная_работа__1.Classes;
using Лабораторная_работа__1.Interfaces;

namespace Лабораторная_работа__1
{
    public partial class MainForm : Form
    {
        List<FlightInfo> flights;

        //Помещает рейсы в базу данных
        void addflight(string from, string to, int dist)
        {
            flights.Add(new FlightInfo(from, to, dist));
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flights = new List<FlightInfo>();

            // Добавляет рейсы в базу данных
            addflight("A", "B", 15);
            addflight("A", "C", 13);
            addflight("A", "D", 19);
            addflight("A", "E", 11);

            addflight("B", "F", 18);
            addflight("B", "G", 12);

            addflight("C", "H", 16);

            addflight("D", "J", 23);
            addflight("D", "K", 14);

            addflight("E", "L", 21);

            addflight("G", "M", 23);
            addflight("G", "N", 15);
            addflight("G", "O", 20);

            addflight("J", "P", 12);
            addflight("J", "R", 18);
            addflight("J", "Q", 15);

            addflight("L", "S", 14);

            addflight("N", "T", 25);
            addflight("N", "U", 15);

            addflight("R", "V", 10);
            addflight("R", "W", 16);

            addflight("S", "X", 12);
            addflight("S", "Y", 13);

            addflight("T", "Z", 24);

            switch (comboBox1.Text)
            {
                case "Поиск в глубину":
                    DepthFirstSearch depthFirstSearch = new DepthFirstSearch(flights);

                    findRoute(depthFirstSearch);

                    break;

                case "Поиск в ширину":
                    BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch(flights);

                    findRoute(breadthFirstSearch);

                    break;

                case "Восхождение на гору":
                    MountainClimbing mountainClimbing = new MountainClimbing(flights);

                    findRoute(mountainClimbing);

                    break;

                case "Наименьшая стоимость":
                    LeastCost leastCost = new LeastCost(flights);

                    findRoute(leastCost);

                    break;
            }
        }

        void findRoute(IAlgorithm alg)
        {
            string from = textBox1.Text;
            string to = textBox2.Text;

            // Проверяет есть ли маршрут между from и to 
            alg.findroute(from, to);

            // Если маршрут существует, отображает его
            if (alg.routefound())
            {
                string[] str = alg.route();
                textBox3.Text = str[0];
                textBox4.Text = str[1];
            }
            else
            {
                textBox3.Text = "Нет маршрута!";
                textBox4.Text = "Нет маршрута!";
            }
        }
    }
}
