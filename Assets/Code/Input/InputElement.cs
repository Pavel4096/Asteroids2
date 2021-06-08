using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids2
{
    internal sealed class InputElement
    {
        public string inputName;

        public string InputCodesString
        {
            get
            {
                if(codeName == null)
                {
                    codeName = String.Empty;
                    foreach(var code in codes)
                    {
                        codeName += Enum.GetName(typeof(KeyCode), code) + "+";
                    }
                    codeName = codeName.Substring(0, codeName.Length - 1);
                }
                return codeName;
            }
        }

        public Action PressedNow;
        public Action ReleasedNow;
        public Action Holded;

        private string codeName;
        private List<KeyCode> codes;
        private bool pressed;

        public InputElement(string _inputName, List<KeyCode> _codes)
        {
            inputName = _inputName;
            codes = _codes;
            pressed = false;
        }

        public InputElement(string data)
        {
            var inputElementData = JsonUtility.FromJson<InputElementData>(data);

            inputName = inputElementData.name;
            codes = inputElementData.codes;
            pressed = false;
        }

        public bool TryRegister(string name, InputType type, Action method)
        {
            bool result = false;

            if(inputName.Equals(name))
            {
                switch(type)
                {
                    case InputType.Pressed:
                        result = RegisterIfUnused(ref PressedNow, method);
                        break;
                    case InputType.Released:
                        result = RegisterIfUnused(ref ReleasedNow, method);
                        break;
                    case InputType.Holded:
                        result = RegisterIfUnused(ref Holded, method);
                        break;
#if UNITY_EDITOR
                    default:
                        Debug.Log($"Input type '{type}' is not supported by input '{name}'");
                        break;
#endif
                }
            }
            
            return result;
        }

        public bool TryUnregister(string name, InputType type)
        {
            bool result = false;

            if(inputName.Equals(name))
            {
                switch(type)
                {
                    case InputType.Pressed:
                        PressedNow = null;
                        result = true;
                        break;
                    case InputType.Released:
                        ReleasedNow = null;
                        result = true;
                        break;
                    case InputType.Holded:
                        Holded = null;
                        result = true;
                        break;
#if UNITY_EDITOR
                    default:
                        Debug.Log($"Input type '{type}' is not supported by input '{name}'");
                        break;
#endif
                }
            }

            return result;
        }

        private bool RegisterIfUnused(ref Action input, Action method)
        {
            bool successful;

            if(input == null)
            {
                input = method;
                successful = true;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log($"Input is already used");
#endif
                successful = false;
            }

            return successful;
        }

        public void Update()
        {
            bool newPressed = true;

            foreach(var key in codes)
            {
                if(!Input.GetKey(key))
                    newPressed = false;
            }

            if(newPressed && !pressed)
                PressedNow?.Invoke();
            else if(!newPressed & pressed)
                ReleasedNow?.Invoke();
            if(pressed)
                Holded?.Invoke();
            pressed = newPressed;
        }

        public string Save()
        {
            return JsonUtility.ToJson(new InputElementData(inputName, codes));
        }

        [Serializable]
        private struct InputElementData
        {
            public string name;
            public List<KeyCode> codes;

            public InputElementData(string _name, List<KeyCode> _codes)
            {
                name = _name;
                codes = _codes;
            }
        }
    }
}
