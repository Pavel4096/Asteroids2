using System;
using UnityEngine;

namespace Asteroids2
{
    internal interface IBulletViewModel : IPoolable
    {
        IBulletModel Model { get; }

        event Action<Vector3> BulletMoved;
        event Action TimeElapsed;
        event Action<Vector3> Reinitialized;

        void Reinitialize(Vector3 position, Vector3 direction);
        void UpdateFrame(float frameTime);
    }
}
