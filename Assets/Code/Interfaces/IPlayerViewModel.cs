using System;
using UnityEngine;

namespace Asteroids2
{
    internal interface IPlayerViewModel
    {
        IPlayerModel Model { get; }
        Rigidbody2D Rigidbody { get; set; }

        event Action<int> ScoreChanged;

        void RotateShip(float direction, float frameTime);
        void Fire();
    }
}
