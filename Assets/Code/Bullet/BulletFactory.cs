using UnityEngine;

namespace Asteroids2
{
    internal sealed class BulletFactory : IBulletFactory
    {
        private IGame game;
        private GameObject bullet;
        private Pool<BulletViewModel> bulletPool;

        private const float bulletSpeed = 5.0f;
        private const float maxTime = 2.5f;

        public BulletFactory(IGame _game)
        {
            game = _game;
            bulletPool = new Pool<BulletViewModel>();
            bullet = Resources.Load<GameObject>("Bullet");
        }

        public void GetBullet(Vector3 position, Vector3 direction)
        {
            BulletViewModel bullet;

            if(!bulletPool.TryGet(out bullet))
            {
                bullet = CreateBullet(position, direction);
                bulletPool.Add(bullet);
            }
            bullet.Reinitialize(position, direction);
        }

        public BulletViewModel CreateBullet(Vector3 position, Vector3 direction)
        {
            var newBullet = Object.Instantiate(bullet, position, Quaternion.identity);
            var bulletModel = new BulletModel(bulletSpeed, maxTime);
            var bulletViewModel = new BulletViewModel(bulletModel);
            var bulletView = newBullet.GetComponent<BulletView>();
            bulletView.Init(bulletViewModel, game);

            return bulletViewModel;
        }
    }
}
