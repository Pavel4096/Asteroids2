using System;
using UnityEngine;

namespace Asteroids2
{
    internal sealed class BulletViewModel : IBulletViewModel, IPoolable
    {
        public IBulletModel Model
        {
            get => model;
        }

        public bool isFree { get; set; }

        public event Action<Vector3> BulletMoved;
        public event Action TimeElapsed;
        public event Action Destroyed;
        public event Action<Vector3> Reinitialized;

        private IBulletModel model;
        private Vector3 direction;
        private float elapsedTime;
        private IDamageManager damageManager;
        private IScoreReceiver scoreReceiver;

        public BulletViewModel(IBulletModel _model, IDamageManager _damageManager)
        {
            model = _model;
            damageManager = _damageManager;
            isFree = false;
            BulletMoved += delegate(Vector3 distance) { };
            TimeElapsed += delegate() { };
            Destroyed += delegate() { };
            Reinitialized += delegate(Vector3 newPosition) { };
        }

        public void Reinitialize(Vector3 newPosition, Vector3 newDirection, IScoreReceiver newScoreReceiver)
        {
            direction = newDirection;
            scoreReceiver = newScoreReceiver;
            elapsedTime = 0.0f;
            Reinitialized.Invoke(newPosition);
        }

        public void UpdateFrame(float frameTime)
        {
            BulletMoved.Invoke(model.Speed*frameTime*direction);
            elapsedTime += frameTime;
            if(elapsedTime >= model.MaxTime)
                TimeElapsed.Invoke();
        }

        public void Damage(int id)
        {
            damageManager.Damage(id, scoreReceiver, 1250);
            Destroyed.Invoke();
        }
    }
}

