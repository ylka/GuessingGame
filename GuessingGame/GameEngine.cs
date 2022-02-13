using System;
using System.Collections.Generic;

namespace GuessingGame
{
    public enum Player
    {
        Player,
        Computer
    }

    public enum GameItem
    {
        Rock = 1,
        Paper,
        Scissors
    }

    public class GameEngine
    {
        public int RoundCount { get; set; }

        private int PlayerCount { get; set; }

        private int BotCount { get; set; }

        public void PrintTips()
        {
            Console.WriteLine($"Round {RoundCount}");
            Console.Write(@"---------------------------------------------------------
Please enter 1, 2 or 3 to choose rock, paper or scissors:
1. rock
2. paper
3. scissors
->");
        }

        public void PrintPlayerChose(Player player, GameItem gameItem)
        {
            Console.WriteLine($"{player} chose {gameItem}");
        }

        private (string, int) Adjudication(GameItem playerChose, GameItem botChose)
        {
            var playerResult = 0;
            string message;
            if (playerChose == botChose)
            {
                message = "It's a draw. Try again. Good luck.\r\n";
                return (message, playerResult);
            }

            playerResult = 1;
            message = "Player won!\r\n";
            switch (playerChose)
            {
                case GameItem.Paper:
                    if (botChose == GameItem.Scissors)
                        playerResult = 2;
                    break;
                case GameItem.Rock:
                    if (botChose == GameItem.Paper)
                        playerResult = 2;
                    break;
                case GameItem.Scissors:
                    if (botChose == GameItem.Rock)
                        playerResult = 2;
                    break;
            }

            if (playerResult == 2)
                message = "Computer won!\r\n";

            return (message, playerResult);
        }

        public void Run()
        {
            var bot = new Bot();
            RoundCount = 1;
            while (true)
            {
                PrintTips();

                var userInput = Console.ReadLine();
                var (validation, chose) = ValidationInput(userInput);
                if (!string.IsNullOrWhiteSpace(validation))
                {
                    Console.WriteLine(validation);
                    continue;
                }

                var botChose = bot.GetGameItem();
                var playerChose = chose;

                PrintPlayerChose(Player.Player, playerChose);
                PrintPlayerChose(Player.Computer, botChose);

                var (message, playerResult) = Adjudication(playerChose, botChose);
                Console.WriteLine(message);

                if (playerResult == 1)
                    PlayerCount++;
                else if (playerResult == 2)
                    BotCount++;

                if (PlayerCount == 2 || BotCount == 2)
                {
                    PrintScore();
                    break;
                }

                if (playerResult != 0)
                    RoundCount++;

            }
        }

        public (string, GameItem)
            ValidationInput(string input)
        {
            var validation = string.Empty;
            var values = new List<int> { 1, 2, 3 };

            if (string.IsNullOrWhiteSpace(input) ||
                !int.TryParse(input, out int result) ||
                !values.Contains(result))
            {
                result = 0;
                validation = "Invalid choice. Please choose again.\r\n";
            }

            return (validation, (GameItem)result);
        }

        public void PrintScore()
        {
            var winner = PlayerCount > BotCount ? Player.Player : Player.Computer;

            Console.WriteLine($@"**********   scoreboard     ********
Player:   {PlayerCount}/{RoundCount}
Computer: {BotCount}/{RoundCount}

The winner is {winner}");
        }

    }
}
