using System;
using System.Xml.XPath;
class Program
{
    static void Main(string[] args)
    {
        gameStartScreen();
    }

    static void gameStartScreen(string startError = "")
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Math Game!");

        // Function to draw horizontal lines
        static void drawHorizontalLines()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write('-');
            }
        }
        drawHorizontalLines();

        Console.WriteLine("\n1. Addition");
        Console.WriteLine("2. Subtraction");
        Console.WriteLine("3. Multiplication");
        Console.WriteLine("4. Division");
        Console.WriteLine("0. Exit");

        drawHorizontalLines();

        if (startError != "")
        {
            Console.Write("\n" + startError);
        }
        Console.Write("\nPlease choose a game (1-4, 0 to exit): ");
        ConsoleKeyInfo choice = Console.ReadKey();

        if (!char.IsDigit(choice.KeyChar))
        {
            gameStartScreen("You must enter a digit 0-4 !!!");
        }
        else
        {
            int choiceInt = choice.KeyChar - '0';

            if (choiceInt == 0)
            {
                Environment.Exit(0);
            }
            else if (choiceInt >= 1 && choiceInt <= 4)
            {
                gameLogic(choiceInt);
            }
            else
            {
                gameStartScreen("You must enter a digit 0-4 !!");
            }
        }
    }

    static void gameLogic(int choice)
    {
        Console.Clear();

        if (choice < 1 || choice > 4)
        {
            Console.WriteLine("There was a problem with the game. Do you wish to restart it?\n1. Yes\nAny other key: No");
            ConsoleKeyInfo restart = Console.ReadKey();
            if (restart.KeyChar == '1')
            {
                gameStartScreen();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        Console.WriteLine("\nDifficulty:\n1. Easy\n2. Medium\n3. Hard");

        ConsoleKeyInfo difficulty = Console.ReadKey();

        if (!char.IsDigit(difficulty.KeyChar))
        {
            Console.WriteLine("\nYou must enter a digit 1-3 !!!\nPress any key to continue.");
            Console.ReadKey();
            gameLogic(choice);
        }
        else
        {
            int difficultyLvl = difficulty.KeyChar - '0';

            if (difficultyLvl < 1 || difficultyLvl > 3)
            {
                Console.WriteLine("\nYou must enter a digit 1-3 !!!\n\nPress any key to continue");
                Console.ReadKey();
                gameLogic(choice);
            }
            else
            {
                Random random = new Random();

                int num1 = (int)Math.Pow(random.Next(1, 10), difficultyLvl);
                int num2 = (int)Math.Pow(random.Next(1, 10), difficultyLvl);

                if (num1 < num2)
                {
                    int temp = num2;
                    num2 = num1;
                    num1 = temp;
                }

                Console.Clear();

                switch (choice)
                {
                    case 1: // addition game
                        game("addition", choice, num1, num2);

                        break;
                    case 2: // subratction game
                        game("subtraction", choice, num1, num2);
                        break;
                    case 3:
                        game("multiplication", choice, num1, num2);
                        break;
                    case 4:
                        game("division", choice, num1, num2);
                        break;
                }
            }
        }
    }

    static void game(string gameMode, int choice, int num1, int num2)
    {
        int correctAnswer = 0;

        switch (gameMode.ToLower())
        {
            case "addition":
                Console.Write($"{num1} + {num2} = ");
                correctAnswer = num1 + num2;
                break;

            case "subtraction":
                Console.Write($"{num1} - {num2} = ");
                correctAnswer = num1 - num2;
                break;

            case "multiplication":
                Console.Write($"{num1} * {num2} = ");
                correctAnswer = num1 * num2;
                break;

            case "division":
                if (num2 == 0)
                {
                    Console.WriteLine("Cannot divide by zero. Press any key to continue.");
                    Console.ReadKey();
                    game(gameMode, choice, num1, num2);
                }
                Console.Write($"{num1} / {num2} = ");
                correctAnswer = num1 / num2;
                break;

            default:
                Console.WriteLine("Invalid game mode.");
                return;
        }

        string? input = Console.ReadLine();
        if (input == null)
        {
            Console.WriteLine("No input provided.\nPress any key to continue.");
            Console.ReadKey();
            gameLogic(choice); 
        }

        if (int.TryParse(input, out int result))
        {
            if (result == correctAnswer)
            {
                Console.WriteLine("\nYou Win!\nPress 1. to restart, 2. for new game, 0. to exit\n");
                ConsoleKeyInfo decision = Console.ReadKey();
                if (!char.IsDigit(decision.KeyChar))
                {
                    Environment.Exit(0);
                }
                int decisionNum = decision.KeyChar - '0';
                if (decisionNum == 1)
                {
                    gameLogic(choice); 
                }
                else if (decisionNum == 2)
                {
                    gameStartScreen();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid input! Please enter a valid integer.");
        }
    }

}
