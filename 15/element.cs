using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Net.Http;

namespace _15
{
    internal class element
    {
        int x_pos;
        public int x_pos_temp;
        int y_pos;
        public int y_pos_temp;
        public string tag;
        public Button btn;
        bool[,] positions;

        public element(ref Grid GridToAdd, int tag, int x_pos, int y_pos, ref bool[,] positions)
        {
            this.x_pos = x_pos;
            this.y_pos = y_pos;
            this.tag = tag.ToString();
            this.btn = new Button();
            this.positions = positions;
            btn.SetValue(Grid.RowProperty, x_pos);
            btn.SetValue(Grid.ColumnProperty, y_pos);
            btn.Content = tag;
            btn.Click += move;
            GridToAdd.Children.Add(btn);
        }
        private void update(object sender,EventArgs e)
        {
            btn.RenderTransform = null;
            btn.SetValue(Grid.RowProperty, x_pos);
            btn.SetValue(Grid.ColumnProperty, y_pos);
        }
        private void move(object sender, RoutedEventArgs e)
        {
            checkAllNeighbours();
            animateMove();
        }
        private void checkAllNeighbours()
        {
            copyCoordinats();
            if (y_pos + 1 <= positions.GetLength(0)-1 && !positions[x_pos, y_pos + 1])
            {
                positions[x_pos, y_pos + 1] = true;
                positions[x_pos, y_pos] = false;
                y_pos++;
                return;
            }

            if (y_pos - 1 >= 0 && !positions[x_pos, y_pos - 1])
            {
                positions[x_pos, y_pos - 1] = true;
                positions[x_pos, y_pos] = false;
                y_pos--;
                return;
            }

            if (x_pos + 1 <= positions.GetLength(0) - 1 && !positions[x_pos + 1, y_pos])
            {
                positions[x_pos + 1, y_pos] = true;
                positions[x_pos, y_pos] = false;
                x_pos++;
                return;
            }

            if (x_pos - 1 >= 0 && !positions[x_pos - 1, y_pos])
            {
                positions[x_pos - 1, y_pos] = true;
                positions[x_pos, y_pos] = false;
                x_pos--;
                return;
            }
        }
        void copyCoordinats()
        {
            x_pos_temp = x_pos;
            y_pos_temp = y_pos;
        }
        void animateMove()
        {
            TranslateTransform movement = new TranslateTransform();
            btn.RenderTransform= movement;
            
            DoubleAnimation animy = new DoubleAnimation(0, (x_pos - x_pos_temp) * btn.ActualWidth, TimeSpan.FromSeconds(0.5));
            animy.Completed += update;
            DoubleAnimation animx = new DoubleAnimation(0, (y_pos - y_pos_temp) * btn.ActualHeight, TimeSpan.FromSeconds(0.5));
            animx.Completed += update;
            movement.BeginAnimation(TranslateTransform.XProperty, animx);
            movement.BeginAnimation(TranslateTransform.YProperty, animy);
        }
    }
}
