using UnityEngine;

namespace Asteroids2
{
    internal sealed class AsteroidView : MonoBehaviour, IGameUpdate
    {
        private IAsteroidViewModel asteroidViewModel;
        private IGame game;
        private Rigidbody2D asteroidRigidbody;

        public void Init(IAsteroidViewModel _asteroidViewModel, IGame _game, IDamageManager _damageManager)
        {
            asteroidViewModel = _asteroidViewModel;
            game = _game;
            asteroidRigidbody = GetComponent<Rigidbody2D>();
            asteroidViewModel.Reinitialized += Show;
            asteroidViewModel.TimeElapsed += Destruct;
            asteroidViewModel.Destroyed += Destruct;

            _damageManager.Add(GetComponent<BoxCollider2D>().GetInstanceID(), asteroidViewModel);
        }

        public void Show(Vector3 position, Vector2 force)
        {
            gameObject.transform.position = position;
            gameObject.SetActive(true);
            asteroidRigidbody.AddForce(force, ForceMode2D.Impulse);
            game.GameLoop -= GameUpdate;
            game.GameLoop += GameUpdate;
        }

        public void Hide()
        {
            game.GameLoop -= GameUpdate;
            gameObject.SetActive(false);
            asteroidViewModel.isFree = true;
        }

        public void Destruct()
        {
            Hide();
        }

        public void GameUpdate(float frameTime)
        {
            asteroidViewModel.GameUpdate(frameTime);
        }

        ~AsteroidView()
        {
            asteroidViewModel.Reinitialized -= Show;
            asteroidViewModel.TimeElapsed -= Destruct;
            asteroidViewModel.Destroyed -= Destruct;
            game.GameLoop -= GameUpdate;
        }
    }
}
