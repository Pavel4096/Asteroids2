using System;
using UnityEngine;

namespace Asteroids2
{
    internal interface IPlayerViewModel
    {
        IPlayerModel Model { get; }

        event Action<int> ScoreChanged;
        event Action<float> PlayerRotated;

        void RotateShip(float direction, float frameTime);
        void Fire(Vector3 position, Vector3 direction);
    }
}
