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

        private const float turnLeft = 1.0f;
        private const float turnRight = -1.0f;

        private float lastFireTime = 0;
        private float bulletOffsetScaleFactor;

        private const float minTimeBetweenFire = 0.5f;
        private const float placeBulletALittleFurther = 1.5f;

        public void Init(IPlayerViewModel _playerViewModel, IInputController inputController)
        {
            playerViewModel = _playerViewModel;
            playerModel = playerViewModel.Model;

            playerViewModel.PlayerRotated += RotatePlayer;

            playerRigidbody2D = GetComponent<Rigidbody2D>();

            bulletOffsetScaleFactor = GetComponent<PolygonCollider2D>().bounds.extents.y*placeBulletALittleFurther;

            inputController.RegisterKey("TurnLeft", InputType.Holded, RotateLeft);
            inputController.RegisterKey("TurnRight", InputType.Holded, RotateRight);
            inputController.RegisterKey("Fire", InputType.Pressed, Fire);
        }

        public void RotatePlayer(float torque)
        {
            playerRigidbody2D.AddTorque(torque);
        }

        public void GameUpdate(float frameTime)
        {
        }

        public void RotateLeft()
        {
            playerViewModel.RotateShip(turnLeft, Time.deltaTime);
        }

        public void RotateRight()
        {
            playerViewModel.RotateShip(turnRight, Time.deltaTime);
        }

        public void Fire()
        {
            if( (Time.time - lastFireTime) >= minTimeBetweenFire )
            {
                playerViewModel.Fire(gameObject.transform.position + gameObject.transform.up*bulletOffsetScaleFactor, gameObject.transform.up);
                lastFireTime = Time.time;
            }
        }

        ~PlayerView()
        {
            playerViewModel.PlayerRotated -= RotatePlayer;
        }
    }
}
