﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Binder.Environment
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Game binderGame;
        Building building;

        public GameWindow(bool cheat, int difficulty)
        {
            //NameScope.SetNameScope(this, new NameScope());
            binderGame = new Game();
            binderGame.IsCheatOn = cheat;
            binderGame.Difficulty = difficulty;
            InitializeComponent();
            cnvsGame.DataContext = building;
        }

        //private void Window_Unloaded(object sender, RoutedEventArgs e)
        //{

        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TranslateTransform transform = new TranslateTransform(50, 20);
            //imgBl.RenderTransform = transform;

            BuildWalls();
            //Canvas.SetLeft(imgBl, Canvas.GetLeft(imgBl) - 50);
            //cnvsGame.Children.Remove(btnStart);
            
        }

        private void CnvsGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                binderGame.Marcus.Move('n', binderGame);
                Canvas.SetTop(imgBl, Canvas.GetTop(imgBl) - 50);
            }
            else if (e.Key == Key.Down)
            {
                binderGame.Marcus.Move('s', binderGame);
                Canvas.SetTop(imgBl, Canvas.GetTop(imgBl) + 50);
            }
            else if (e.Key == Key.Left)
            {
                binderGame.Marcus.Move('w', binderGame);
                Canvas.SetLeft(imgBl, Canvas.GetLeft(imgBl) - 50);
            }
            else if (e.Key == Key.Right)
            {
                binderGame.Marcus.Move('e', binderGame);
                Canvas.SetLeft(imgBl, Canvas.GetLeft(imgBl) + 50);
            }
            else if (e.Key == Key.C)
            {
                binderGame.Marcus.Attack();
            }
            else if (e.Key == Key.X)
            {
                binderGame.Marcus.Enteract();
            }
            else if (e.Key == Key.Escape)
            {
                Game.isPaused = true;
                Pause pauseWindow = new Pause(binderGame);
                pauseWindow.Show();
            }

            //Debug.WriteLine(Canvas.GetLeft(imgBl) + " " + Canvas.GetTop(imgBl));
            //Debug.WriteLine(imgBl.RenderTransform.Value);
        }

        //Builds Walls with Blocks on GUI 
        public void BuildWalls()
        {
            int[] c = new int[2] { 0, 0 };
            Block b = new Block(24, 24, c );
            //Image img = new Image()
            //{
            //    Source = new BitmapImage(new Uri("/Environment/blocks.png", UriKind.Relative))
            //};
            //Label block = new Label()
            //{
            //    Content = img
            //};
            //Build walls
            int[] pos = new int[2] { 100, 50 };
            Walls modelWallOne = new Walls(30, 500, pos);
            Building.WallsCol.Add(modelWallOne);
            pos[0] = 200;
            pos[1] = 100;
            Walls modelWallTwo = new Walls(30, 700, pos);
            Building.WallsCol.Add(modelWallOne);
            foreach (Walls wall in Building.WallsCol)
            {
                Rectangle wallOne = new Rectangle
                {
                    Width = wall.Length,
                    Height = wall.Width
                };
                wallOne.Fill = new SolidColorBrush(Colors.Brown);
                Canvas.SetLeft(wallOne, wall.Position[0]);
                Canvas.SetTop(wallOne, wall.Position[1]);
                cnvsGame.Children.Add(wallOne);
            }
            
        }
    }
}
