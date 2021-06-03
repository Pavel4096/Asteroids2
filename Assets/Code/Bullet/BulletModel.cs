using UnityEngine;

namespace Asteroids2
{
    internal sealed class BulletModel : IBulletModel
    {
        public float Speed { get; }
        public float MaxTime { get; set; }

        public BulletModel(float _speed, float _maxTime)
        {
            Speed = _speed;
            MaxTime = _maxTime;
        }
    }
}
