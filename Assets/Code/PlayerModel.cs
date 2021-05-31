using UnityEngine;

namespace Asteroids2
{
    internal sealed class PlayerModel : IPlayerModel
    {
        private int score;

        public int Score
        {
            get => score;
        }

        public KeyCode RotateRightKey { get; set; }
        public KeyCode RotateLeftKey { get; set; }
        public KeyCode FireKey { get; set; }
        public float Torque { get; private set; }

        public PlayerModel(KeyCode _rotateRightKey, KeyCode _rotateLeftKey, KeyCode _fireKey, float _torque)
        {
            RotateRightKey = _rotateRightKey;
            RotateLeftKey = _rotateLeftKey;
            FireKey = _fireKey;
            Torque = _torque;
        }

        public void AddScore(int additionalScore)
        {
            score += additionalScore;
        }
    }
}
