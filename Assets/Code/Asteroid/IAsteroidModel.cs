namespace Asteroids2
{
    internal interface IAsteroidModel
    {
        float Health { get; }
        float MaxHealth { get; }
        float MaxTime { get; }

        void Damage(float damage);
    }
}
