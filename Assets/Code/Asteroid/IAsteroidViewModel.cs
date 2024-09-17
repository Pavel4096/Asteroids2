using System;
using UnityEngine;

namespace Asteroids2
{
    internal interface IAsteroidViewModel : IPoolable, IDamageReceiver
    {
        IAsteroidModel Model { get; }

        event Action<Vector3, Vector2> Reinitialized;
        event Action TimeElapsed;
        event Action Destroyed;

        void Reinitialize(Vector3 position, Vector2 force);
        void GameUpdate(float frameTime);
    }
}
