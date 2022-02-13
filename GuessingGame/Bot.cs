using System;

namespace GuessingGame
{
    internal class Bot
    {
        public GameItem GetGameItem()
        {
            var r = new Random().Next(1, 3);
            return (GameItem)r;
        }
    }
}
