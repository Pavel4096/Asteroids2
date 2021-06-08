using System;

namespace Asteroids2
{
    internal interface IGame
    {
        bool IsPaused { get; set; }
        event Action<float> GameLoop;
    }
}
