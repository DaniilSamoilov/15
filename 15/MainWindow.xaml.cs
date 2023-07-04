using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _15
{
    public partial class MainWindow : Window
    {
        List<element> elements= new List<element>();
        bool[,] positions =new bool[4, 4];

        public MainWindow()
        {
            InitializeComponent();
            positions = generatePositions();
            CreateElements();
        }

        void CreateElements()
        {
            int i = 1;

            var result = createSolvableSequence();
            foreach (var item in result)
            {
                elements.Add(new element(ref MainGrid, item, i / 4, i % 4,ref positions));
                i++;
            }
            foreach (var item in elements)
            {
                item.btn.Click += checkWin;
            }
        }
        bool[,] generatePositions()
        {
            int EmptyTile = new Random().Next(1,17);
            for (int i = 0; i < 16; i++)
            {
                if (0 ==i)
                {
                    this.positions[i / 4, i % 4] = false;
                }
                else
                {
                    this.positions[i / 4, i % 4] = true;
                }
            }
            return positions;
        }

        void checkWin(object sender,EventArgs e)
        {
            int[] Order =  new int[16];
            foreach (var item in elements)
            {
                if (item.tag!=(item.x_pos_temp*4+item.y_pos_temp).ToString())
                {
                    return;
                }
            }
            MessageBox.Show("Победа");
        }

        int[] createSolvableSequence()
        {
            int[] numbers = new int[15];
            bool check = true;
            var result = Enumerable.Range(1, 15).OrderBy(g => Guid.NewGuid()).ToArray();
            while (check)
            {
                result = Enumerable.Range(1, 15).OrderBy(g => Guid.NewGuid()).ToArray();
                int temp = 0;
                foreach (var item in result)
                {
                    numbers[temp] = item;
                    temp++;
                }
                int inv = 0;
                for (int i = 0; i < 15; ++i)
                {
                    for (int j = 0; j < i; ++j)
                    {
                        if (numbers[j] > numbers[i]) { ++inv; }
                    }
                }
                if (inv % 2 == 0){check = false;}
            }
            return result;
        }
    }
}
