using System;

namespace Asteroids2
{
    internal interface IGame
    {
        event Action<float> GameLoop;
    }
}
