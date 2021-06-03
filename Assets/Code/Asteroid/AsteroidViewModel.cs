using System;
using UnityEngine;

namespace Asteroids2
{
    internal sealed class AsteroidViewModel : IAsteroidViewModel, IPoolable
    {
        public IAsteroidModel Model
        {
            get => model;
        }

        public bool isFree { get; set; }

        public event Action<Vector3, Vector2> Reinitialized;
        public event Action TimeElapsed;
        public event Action Destroyed;

        private IAsteroidModel model;
        private float elapsedTime;

        private const int scoreValue = 250;

        public AsteroidViewModel(IAsteroidModel _model)
        {
            model = _model;
            isFree = false;

            Reinitialized += delegate(Vector3 position, Vector2 force) { };
            TimeElapsed += delegate() { };
            Destroyed += delegate() { };
        }

        public void Reinitialize(Vector3 position, Vector2 force)
        {
            elapsedTime = 0.0f;
            Reinitialized.Invoke(position, force);
        }

        public void Damage(IScoreReceiver scoreReceiver, float damage)
        {
            model.Damage(damage);
            if(model.Health <= 0.0f)
            {
                Destroyed.Invoke();
                scoreReceiver.AddScore(scoreValue);
            }
        }

        public void GameUpdate(float frameTime)
        {
            elapsedTime += frameTime;
            if(elapsedTime >= model.MaxTime)
                TimeElapsed.Invoke();
        }
    }
}
