using UnityEngine;

namespace Asteroids2
{
    internal sealed class AsteroidFactory : IAsteroidFactory
    {
        private IGame game;
        private GameObject asteroid;
        private Pool<AsteroidViewModel> asteroidPool;
        private IDamageManager damageManager;
        private Camera mainCamera;
        private float worldScreenWidth;
        private float worldScreenHeight;
        private float spawnRadius;
        private System.Random rnd;

        private const float asteroidMaxHealth = 1000.0f;
        private const float spawnRadiusScaleFactor = 1.2f;
        private const float forceValue = 2.5f;
        private const float maxTime = 10.0f;

        public AsteroidFactory(IGame _game, IDamageManager _damageManager)
        {
            game = _game;
            asteroidPool = new Pool<AsteroidViewModel>(10);
            damageManager = _damageManager;
            asteroid = Resources.Load<GameObject>("Asteroid");
            mainCamera = Camera.main;
            worldScreenHeight = Mathf.Abs(mainCamera.transform.position.z*2*Mathf.Tan(Mathf.Deg2Rad*mainCamera.fieldOfView/2));
            worldScreenWidth = worldScreenHeight*mainCamera.aspect;
            spawnRadius = Mathf.Sqrt(Mathf.Pow(worldScreenWidth, 2) + Mathf.Pow(worldScreenHeight, 2))*spawnRadiusScaleFactor/2;
            rnd = new System.Random();
        }

        public void GetAsteroid()
        {
            AsteroidViewModel asteroid;
            Vector3 position;
            Vector2 force;

            if(!asteroidPool.TryGet(out asteroid))
            {
                asteroid = CreateAsteroid();
                asteroidPool.Add(asteroid);
            }

            RandomValues(out position, out force);
            asteroid.Reinitialize(position, force);
        }

        public AsteroidViewModel CreateAsteroid()
        {
            var newAsteroid = Object.Instantiate(asteroid);
            var asteroidModel = new AsteroidModel(asteroidMaxHealth, maxTime);
            var asteroidViewModel = new AsteroidViewModel(asteroidModel);
            var asteroidView = newAsteroid.GetComponent<AsteroidView>();
            asteroidView.Init(asteroidViewModel, game, damageManager);

            return asteroidViewModel;
        }

        public void RandomValues(out Vector3 position, out Vector2 force)
        {
            float angle;
            Vector2 point;

            angle = (float)rnd.NextDouble()*2*Mathf.PI;
            position = new Vector3(Mathf.Cos(angle)*spawnRadius, Mathf.Sin(angle)*spawnRadius, 0.0f);
            point = RandomVector(worldScreenWidth, worldScreenHeight);
            force = (new Vector2(point.x - position.x, point.y - position.y)).normalized*forceValue;
        }

        public Vector2 RandomVector(float width, float height)
        {
            return new Vector2( (float)((rnd.NextDouble() - 0.5)*width), (float)((rnd.NextDouble() - 0.5)*height) );
        }
    }
}
