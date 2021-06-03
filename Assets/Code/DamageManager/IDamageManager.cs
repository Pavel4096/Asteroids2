namespace Asteroids2
{
    internal interface IDamageManager
    {
        void Add(int id, IDamageReceiver receiver);
        void Damage(int id, IScoreReceiver scoreReceiver, float damage);
    }
}
