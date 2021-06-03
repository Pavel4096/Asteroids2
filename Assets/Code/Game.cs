using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids2
{
    internal sealed class Game : MonoBehaviour, IGame
    {
        public event Action<float> GameLoop;

        [SerializeField] private PlayerView playerView;
        [SerializeField] private ScoreView scoreView;
        private AsteroidFactory asteroidFactory;
        private IDamageManager damageManager;

        void Start()
        {
            damageManager = new DamageManager();
            IPlayerModel playerModel = new PlayerModel(KeyCode.A, KeyCode.D, KeyCode.Space, 2.5f);
            IPlayerViewModel playerViewModel = new PlayerViewModel(playerModel, new BulletFactory(this, damageManager));
            playerView.Init(playerViewModel);
            scoreView.Init(playerViewModel);

            GameLoop += delegate(float frameTime) { };
            GameLoop += playerView.GameUpdate;

            asteroidFactory = new AsteroidFactory(this, damageManager);
        }

        void Update()
        {
            if((Time.frameCount % 250) == 0)
                asteroidFactory.GetAsteroid();
            GameLoop.Invoke(Time.deltaTime);
        }
    }
}
