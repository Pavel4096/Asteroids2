using System;
using UnityEngine;

namespace Asteroids2
{
    internal sealed class PlayerViewModel : IPlayerViewModel
    {
        public IPlayerModel playerModel;

        public Rigidbody2D Rigidbody { get; set; }

        public PlayerViewModel(IPlayerModel _playerModel)
        {
            playerModel = _playerModel;
            ScoreChanged += delegate(int newScore) { };
        }

        public IPlayerModel Model
        {
            get => playerModel;
        }

        public event Action<int> ScoreChanged;

        public void RotateShip(float direction, float frameTime)
        {
            Rigidbody.AddTorque(playerModel.Torque*frameTime*direction);
        }

        public void Fire()
        {

        }
    }
}
