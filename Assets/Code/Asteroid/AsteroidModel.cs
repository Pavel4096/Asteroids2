namespace Asteroids2
{
    internal sealed class AsteroidModel : IAsteroidModel
    {
        public float Health
        {
            get => health;
        }

        public float MaxHealth { get; }
        public float MaxTime { get; }

        private float health;

        public AsteroidModel(float _maxHealth, float _maxTime)
        {
            MaxHealth = _maxHealth;
            MaxTime = _maxTime;
            health = MaxHealth;
        }

        public void Damage(float damage)
        {
            health -= damage;
            if(health <= 0.0f)
                health = 0.0f;
        }
    }
}
