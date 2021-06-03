using UnityEngine;

namespace Asteroids2
{
    internal interface IBulletFactory
    {
        void GetBullet(Vector3 position, Vector3 direction);
    }
}
