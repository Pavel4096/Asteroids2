using UnityEngine;

namespace Asteroids2
{
    internal sealed class PlayerModel : IPlayerModel
    {
        public int Score
        {
            get => score;
        }

        public KeyCode RotateRightKey { get; set; }
        public KeyCode RotateLeftKey { get; set; }
        public KeyCode FireKey { get; set; }
        public float Torque { get; private set; }

        private int score;

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
