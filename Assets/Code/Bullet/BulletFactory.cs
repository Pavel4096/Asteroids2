using UnityEngine;

namespace Asteroids2
{
    internal sealed class BulletFactory : IBulletFactory
    {
        private IGame game;
        private GameObject bullet;
        private Pool<BulletViewModel> bulletPool;
        private IDamageManager damageManager;

        private const float bulletSpeed = 5.0f;
        private const float maxTime = 2.5f;

        public BulletFactory(IGame _game, IDamageManager _damageManager)
        {
            game = _game;
            bulletPool = new Pool<BulletViewModel>();
            damageManager = _damageManager;
            bullet = Resources.Load<GameObject>("Bullet");
        }

        public void GetBullet(Vector3 position, Vector3 direction, IScoreReceiver scoreReceiver)
        {
            BulletViewModel bullet;

            if(!bulletPool.TryGet(out bullet))
            {
                bullet = CreateBullet(position, direction);
                bulletPool.Add(bullet);
            }
            bullet.Reinitialize(position, direction, scoreReceiver);
        }

        public BulletViewModel CreateBullet(Vector3 position, Vector3 direction)
        {
            var newBullet = Object.Instantiate(bullet, position, Quaternion.identity);
            var bulletModel = new BulletModel(bulletSpeed, maxTime);
            var bulletViewModel = new BulletViewModel(bulletModel, damageManager);
            var bulletView = newBullet.GetComponent<BulletView>();
            bulletView.Init(bulletViewModel, game);

            return bulletViewModel;
        }
    }
}
