using System.Collections.Generic;

namespace Asteroids2
{
    internal sealed class DamageManager : IDamageManager
    {
        private Dictionary<int, IDamageReceiver> receivers;

        public DamageManager()
        {
            receivers = new Dictionary<int, IDamageReceiver>();
        }

        public void Add(int id, IDamageReceiver receiver)
        {
            if(!receivers.ContainsKey(id))
                receivers.Add(id, receiver);
        }

        public void Damage(int id, IScoreReceiver scoreReceiver, float damage)
        {
            if(receivers.ContainsKey(id))
            {
                receivers[id].Damage(scoreReceiver, damage);
            }
        }
    }
}
