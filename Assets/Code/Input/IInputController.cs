using System;

namespace Asteroids2
{
    internal interface IInputController : IGameUpdate
    {
        void AddInput(InputElement input);
        void Save(string filename = default);
        void Load(string filename = default);

        void RegisterKey(string name, InputType type, Action method);
        void UnregisterKey(string name, InputType type);
    }
}
