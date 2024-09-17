namespace Asteroids2
{
    internal interface IDamageReceiver
    {
        void Damage(IScoreReceiver scoreReceiver, float damage);
    }
}
