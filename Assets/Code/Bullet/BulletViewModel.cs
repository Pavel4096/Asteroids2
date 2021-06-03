using System;
using UnityEngine;

namespace Asteroids2
{
    internal sealed class BulletViewModel : IBulletViewModel, IPoolable
    {
        IBulletModel model;

        public IBulletModel Model
        {
            get => model;
        }

        public bool isFree { get; set; }

        public event Action<Vector3> BulletMoved;
        public event Action TimeElapsed;
        public event Action<Vector3> Reinitialized;

        private Vector3 direction;
        private float elapsedTime;

        public BulletViewModel(IBulletModel _model)
        {
            model = _model;
            isFree = true;
            BulletMoved += delegate(Vector3 distance) { };
            TimeElapsed += delegate() { };
            Reinitialized += delegate(Vector3 newPosition) { };
        }

        public void Reinitialize(Vector3 newPosition, Vector3 newDirection)
        {
            direction = newDirection;
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
    }
}

