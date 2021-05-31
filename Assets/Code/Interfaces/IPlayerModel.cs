using UnityEngine;

namespace Asteroids2
{
    internal interface IPlayerModel
    {
        int Score { get; }
        void AddScore(int additionalScore);
        KeyCode RotateRightKey { get; }
        KeyCode RotateLeftKey { get; }
        KeyCode FireKey { get; }
        float Torque { get; }
    }
}
