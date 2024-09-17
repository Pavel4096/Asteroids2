using System;
using UnityEngine;

namespace Asteroids2
{
    internal sealed class PlayerViewModel : IPlayerViewModel
    {
        public IPlayerModel playerModel;

        public IPlayerModel Model
        {
            get => playerModel;
        }

        public event Action<int> ScoreChanged;
        public event Action<float> PlayerRotated;

        private IBulletFactory bulletFactory;

        public PlayerViewModel(IPlayerModel _playerModel, IBulletFactory _bulletFactory)
        {
            playerModel = _playerModel;
            bulletFactory = _bulletFactory;
            ScoreChanged += delegate(int newScore) { };
            PlayerRotated += delegate(float value) { };
        }

        public void RotateShip(float direction, float frameTime)
        {
            PlayerRotated.Invoke(playerModel.Torque*frameTime*direction);
        }

        public void Fire(Vector3 position, Vector3 direction)
        {
            bulletFactory.GetBullet(position, direction, this);
        }

        public void AddScore(int additionalScore)
        {
            playerModel.AddScore(additionalScore);
            ScoreChanged.Invoke(playerModel.Score);
        }
    }
}
