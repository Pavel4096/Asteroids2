using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids2
{
    internal sealed class Game : MonoBehaviour, IGame
    {
        public bool IsPaused
        {
            get => isPaused;
            set
            {
                if( (value == true) && (!isPaused) )
                {
                    previousTimeScale = Time.timeScale;
                    Time.timeScale = 0.0f;
                    isPaused = true;
                }
                else
                {
                    Time.timeScale = previousTimeScale;
                    isPaused = false;
                }
            }
        }

        public event Action<float> GameLoop;

        [SerializeField] private PlayerView playerView;
        [SerializeField] private ScoreView scoreView;

        private IInputController inputController;
        private bool isPaused;
        private AsteroidFactory asteroidFactory;
        private IDamageManager damageManager;
        private float previousTimeScale;

        void Start()
        {
            isPaused = false;
            inputController = new InputController();
            damageManager = new DamageManager();
            IPlayerModel playerModel = new PlayerModel(KeyCode.A, KeyCode.D, KeyCode.Space, 2.5f);
            IPlayerViewModel playerViewModel = new PlayerViewModel(playerModel, new BulletFactory(this, damageManager));
            playerView.Init(playerViewModel, inputController);
            scoreView.Init(playerViewModel);

            GameLoop += delegate(float frameTime) { };
            GameLoop += inputController.GameUpdate;
            GameLoop += playerView.GameUpdate;

            asteroidFactory = new AsteroidFactory(this, damageManager);
        }

        void Update()
        {
            if(!isPaused)
            {
                if((Time.frameCount % 250) == 0)
                    asteroidFactory.GetAsteroid();
                GameLoop.Invoke(Time.deltaTime);
            }
        }

        ~Game()
        {
            GameLoop -= inputController.GameUpdate;
            GameLoop -= playerView.GameUpdate;
        }
    }
}
