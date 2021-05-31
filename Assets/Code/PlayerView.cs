using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids2
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class PlayerView : MonoBehaviour, IPlayerView
    {
        private IPlayerViewModel playerViewModel;
        private IPlayerModel playerModel;

        private const float turnRight = 1.0f;
        private const float turnLeft = -1.0f;

        public void Init(IPlayerViewModel _playerViewModel)
        {
            playerViewModel = _playerViewModel;
            playerViewModel.Rigidbody = GetComponent<Rigidbody2D>();
            playerModel = playerViewModel.Model;
        }

        public void GameUpdate(float frameTime)
        {
            if(Input.GetKey(playerModel.RotateRightKey))
                playerViewModel.RotateShip(turnRight, frameTime);
            else if(Input.GetKey(playerModel.RotateLeftKey))
                playerViewModel.RotateShip(turnLeft, frameTime);
            if(Input.GetKeyDown(playerModel.FireKey))
                playerViewModel.Fire();
        }
    }
}
