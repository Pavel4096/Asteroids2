using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids2
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class PlayerView : MonoBehaviour, IGameUpdate
    {
        private IPlayerViewModel playerViewModel;
        private IPlayerModel playerModel;

        private Rigidbody2D playerRigidbody2D;

        private const float turnRight = 1.0f;
        private const float turnLeft = -1.0f;

        private float timeSinceLastFire = 0;
        private float bulletOffsetScaleFactor;

        private const float minTimeBetweenFire = 0.5f;
        private const float placeBulletALittleFurther = 1.5f;

        public void Init(IPlayerViewModel _playerViewModel)
        {
            playerViewModel = _playerViewModel;
            playerModel = playerViewModel.Model;

            playerViewModel.PlayerRotated += RotatePlayer;

            playerRigidbody2D = GetComponent<Rigidbody2D>();

            bulletOffsetScaleFactor = GetComponent<PolygonCollider2D>().bounds.extents.y*placeBulletALittleFurther;
        }

        public void RotatePlayer(float torque)
        {
            playerRigidbody2D.AddTorque(torque);
        }

        public void GameUpdate(float frameTime)
        {
            if(Input.GetKey(playerModel.RotateRightKey))
                playerViewModel.RotateShip(turnRight, frameTime);
            else if(Input.GetKey(playerModel.RotateLeftKey))
                playerViewModel.RotateShip(turnLeft, frameTime);
            if(Input.GetKeyDown(playerModel.FireKey) && (timeSinceLastFire >= minTimeBetweenFire))
            {
                playerViewModel.Fire(gameObject.transform.position + gameObject.transform.up*bulletOffsetScaleFactor, gameObject.transform.up);
                timeSinceLastFire = 0;
            }
            else
                timeSinceLastFire += frameTime;
        }

        ~PlayerView()
        {
            playerViewModel.PlayerRotated -= RotatePlayer;
        }
    }
}
