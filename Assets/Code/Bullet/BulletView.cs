using System.Collections.Generic;
using UnityEngine;

namespace Asteroids2
{
    [RequireComponent(typeof(PolygonCollider2D))]
    internal sealed class BulletView : MonoBehaviour, IGameUpdate
    {
        private IBulletViewModel bulletViewModel;
        private IGame game;
        private PolygonCollider2D bulletCollider;
        private List<Collider2D> colliders;

        public void Init(IBulletViewModel _bulletViewModel, IGame _game)
        {
            bulletViewModel = _bulletViewModel;
            game = _game;
            bulletCollider = GetComponent<PolygonCollider2D>();
            colliders = new List<Collider2D>(2);
            bulletViewModel.BulletMoved += MoveBullet;
            bulletViewModel.TimeElapsed += Destruct;
            bulletViewModel.Reinitialized += Show;
        }

        public void Show(Vector3 newPosition)
        {
            gameObject.transform.position = newPosition;
            gameObject.SetActive(true);
            game.GameLoop += GameUpdate;
        }

        public void Hide()
        {
            game.GameLoop -= GameUpdate;
            gameObject.SetActive(false);
            bulletViewModel.isFree = true;
        }

        public void MoveBullet(Vector3 distance)
        {
            gameObject.transform.position += distance;
        }

        public void Destruct()
        {
            Hide();
        }

        public void GameUpdate(float frameTime)
        {
            bulletViewModel.UpdateFrame(frameTime);
            if((Time.frameCount & 0x1) == 0)
            {
                var filter = new ContactFilter2D();
                var count = bulletCollider.OverlapCollider(filter.NoFilter(), colliders);
                if(count > 0)
                {
                    
                }
            }
        }

        ~BulletView()
        {
            game.GameLoop -= GameUpdate;
            bulletViewModel.BulletMoved -= MoveBullet;
            bulletViewModel.TimeElapsed -= Destruct;
            bulletViewModel.Reinitialized -= Show;
        }
    }
}
