using System.Collections.Generic;

namespace Asteroids2
{
    internal sealed class DamageManager : IDamageManager
    {
        private Dictionary<int, IDamageReceiver> receivers;
    }
}
