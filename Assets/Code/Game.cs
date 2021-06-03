using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids2
{
    internal sealed class Game : MonoBehaviour, IGame
    {
        [SerializeField] PlayerView playerView;

        public event Action<float> GameLoop;

        void Start()
        {
            IPlayerModel playerModel = new PlayerModel(KeyCode.A, KeyCode.D, KeyCode.Space, 2.5f);
            IPlayerViewModel playerViewModel = new PlayerViewModel(playerModel, new BulletFactory(this));
            playerView.Init(playerViewModel);

            GameLoop += delegate(float frameTime) { };
            GameLoop += playerView.GameUpdate;
        }

        void Update()
        {
            GameLoop.Invoke(Time.deltaTime);
        }
    }
}
