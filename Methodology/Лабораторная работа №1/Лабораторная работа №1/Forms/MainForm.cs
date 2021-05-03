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

namespace Лабораторная_работа__1
{
    public partial class MainForm : Form
    {
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
            switch (comboBox1.Text)
            {
                case "Метод в глубину":
                    DepthFirstSearch depthFirstSearch = new DepthFirstSearch();


                    break;

                case "Метод в ширину":
                    BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch();


                    break;

                case "Восхождение на гору":
                    MountainClimbing mountainClimbing = new MountainClimbing();


                    break;

                case "Наименьшая стоимость":
                    LeastCost leastCost = new LeastCost();


                    break;

                case "Оптимальный поиск":
                    OptimalSearch optimalSearch = new OptimalSearch();


                    break;
            }
        }
    }
}
