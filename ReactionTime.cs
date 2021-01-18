using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Timers;

namespace Avivs_Reaction_Time_Game
{
    internal class Program
    {
        private static Random RandomNum = new Random();
        private static Random Color = new Random();
        private static Stopwatch stopwatch = new Stopwatch();
        private static Stopwatch ExecutionTime = new Stopwatch();
        private static List<long> Times = new List<long>();
        private static System.ConsoleKeyInfo KeyEntered;
        private static int Difficulty;

        private static ConsoleKey Button;
        private static char Difficulty_Key;
        private const int makeup_time = 0;

        private static void Main()
        {
            Console.CursorVisible = false;

            Console.WriteLine("You will have your reaction time tested 5 times... or... quice(?)");

            Spacer();

            Console.WriteLine("Please press the key on your keyboard that you'd like to use as your action key.");
            KeyEntered = Console.ReadKey();

            Button = KeyEntered.Key;

            Spacer();

            while (true)
            {
                if (Times.Count == 0)
                {
                    Console.WriteLine("Please select a difficulty from 1-5, the higher the number the harder the difficulty.");

                    KeyEntered = Console.ReadKey();

                    Difficulty_Key = KeyEntered.KeyChar;

                    while ((int)Difficulty_Key < 49 || (int)Difficulty_Key > 53)
                    {
                        Spacer();
                        Console.WriteLine("{0}, please select a difficulty from 1-5", Environment.UserName);

                        KeyEntered = Console.ReadKey();

                        Difficulty_Key = KeyEntered.KeyChar;
                    }

                    switch (Difficulty_Key)  //easier and more readable to use a switch-case than make a useable mathematical formula that scales well across all difficulties
                    {
                        case '1':
                            Difficulty = 350;
                            break;

                        case '2':
                            Difficulty = 300;
                            break;

                        case '3':
                            Difficulty = 250;
                            break;

                        case '4':
                            Difficulty = 200;
                            break;

                        case '5':
                            Difficulty = 150;
                            break;
                    }

                    Spacer();

                    Console.WriteLine($"Have an average reaction time of {Difficulty}ms or lower to win.");
                }
                Spacer();
                Standard_Text(Button);

                KeyEntered = Console.ReadKey();

                if (KeyEntered.Key != Button)
                {
                    Response(KeyEntered.Key);
                }

                if (KeyEntered.Key == Button)
                {
                    System.Timers.Timer Kuqi = new System.Timers.Timer(RandomNum.Next(10, 70) * 100);
                    Kuqi.AutoReset = false;
                    Kuqi.Elapsed += Kuqi_Elapsed;
                    Kuqi.Start();

                    KeyEntered = Console.ReadKey();
                    if (stopwatch.IsRunning == true)
                    {
                        ExecutionTime.Start();
                        if (KeyEntered.Key == Button)
                        {

                            if (Times.Count < 5)
                            {
                                Console.WriteLine(ExecutionTime.Elapsed);
                                ExecutionTime.Reset();
                                Times.Add(stopwatch.ElapsedMilliseconds - makeup_time);
                            }
                            Kuqi.Dispose();
                            Spacer();

                            Console.WriteLine($"{stopwatch.ElapsedMilliseconds - makeup_time} Milliseconds");
                            

                            GameStatus(Times);
                            Kuqi.Stop();
                            stopwatch.Reset();
                        }
                    }
                    else
                    {
                        Kuqi.Dispose();
                        Spacer();
                        Console.WriteLine($"hehe, no no no {Environment.UserName}, I'll be adding a time of 500 ms as punishment :) ");
                        Times.Add(500);

                        GameStatus(Times);

                        stopwatch.Reset();
                    }
                }

                if (KeyEntered.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        private static void Kuqi_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Spacer();
            Console.WriteLine("GO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            stopwatch.Start();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void Response(ConsoleKey KeyPressed)
        {
            Spacer();
            Console.WriteLine($"You are a good friend, {Environment.UserName}, and a known trickster. I see that you've bamboozled me by pressing {KeyPressed}. Well done! But please, press {Button} :)");
        }

        private static void Spacer()
        {
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void Standard_Text(ConsoleKey DesiredKey)
        {
            Console.WriteLine($"Press {DesiredKey} to to start the timer, when the green text appears, press {DesiredKey} again to stop the timer.");
        }

        private static void GameStatus(List<long> Status)
        {
            if (Status.Count < 5)
            {
                return;
            }

            Console.WriteLine();

            switch (Difficulty_Key)
            {
                case '1':
                    if (Status.Average() < 350)
                    {
                        Console.WriteLine("You Win!!!");
                        Spacer();
                    }
                    else
                    {
                        YouLose();
                    }
                    break;

                case '2':
                    if (Status.Average() < 300)
                    {
                        Console.WriteLine("You Win!!!");
                        Spacer();
                    }
                    else
                    {
                        YouLose();
                    }
                    break;

                case '3':
                    if (Status.Average() < 250)
                    {
                        Console.WriteLine("You Win!!!");
                        Spacer();
                    }
                    else
                    {
                        YouLose();
                    }
                    break;

                case '4':
                    if (Status.Average() < 200)
                    {
                        Console.WriteLine("You Win!!!");
                        Spacer();
                    }
                    else
                    {
                        YouLose();
                    }
                    break;

                case '5':
                    if (Status.Average() < 150)
                    {
                        Console.WriteLine("You Win!!!");
                        Spacer();
                    }
                    else
                    {
                        YouLose();
                    }
                    break;
            }

            Spacer();
            Console.WriteLine("Here are your stats:");
            Spacer();
            foreach (long record in Status)
            {
                Console.WriteLine(record);
                Thread.Sleep(400);
            }
            Spacer();
            Console.WriteLine($"Your average time was {Status.Average()}. Your best time was {Status.Min()}. Your worst time was {Status.Max()}.");
            Spacer();
            Status.Clear(); //This clears the both Status and Times, as Status is a reference to the original List.
        }

        private static void YouLose()
        {
            int LastColor = 0;
            int CurrentColor;

            stopwatch.Reset();
            for (int i = 0; i < 200; i++)
            {
                CurrentColor = Color.Next(1, 7);
                while (CurrentColor.Equals(LastColor)) //Ensures the same color is not picked twice in a row
                {
                    CurrentColor = Color.Next(1, 7);
                }
                switch (CurrentColor)

                {
                    case 1:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        }
                    case 2:
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        }
                    case 3:
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        }
                    case 4:
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        }
                    case 5:
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        }
                    case 6:
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                        }
                    case 7:
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            break;
                        }
                }
                LastColor = CurrentColor;
                Thread.Sleep(20);
                Console.WriteLine("Pathetic Pathetic Pathetic Pathetic Pathetic Pathetic Pathetic Pathetic Pathetic Pathetic Pathetic Pathetic");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}