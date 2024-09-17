using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids2
{
    internal sealed class InputController : IInputController
    {
        private List<InputElement> inputs;

        private const string default_filename = "inputs.config";

        public InputController()
        {
            inputs = new List<InputElement>();
            Load();
        }

        public void RegisterKey(string name, InputType type, Action method)
        {
            foreach(var input in inputs)
            {
                if(input.TryRegister(name, type, method))
                    break;
            }
        }

        public void UnregisterKey(string name, InputType type)
        {
            foreach(var input in inputs)
            {
                if(input.TryUnregister(name, type))
                    break;
            }
        }

        public void AddInput(InputElement input)
        {
            inputs.Add(input);
        }

        public void Save(string filename = default)
        {
            string config_filename;

            if(filename == null)
                filename = default_filename;
            config_filename = Path.Combine(Application.persistentDataPath, filename);

            using(var writer = new StreamWriter(config_filename))
            {
                foreach(var inputElement in inputs)
                {
                    writer.WriteLine(inputElement.Save());
                }
            }
        }

        public void Load(string filename = default)
        {
            string config_filename;
            string currentLine;

            if(filename == null)
                filename = default_filename;
            config_filename = Path.Combine(Application.persistentDataPath, filename);
            inputs = new List<InputElement>();

            if(File.Exists(config_filename))
            {
                using(var reader = new StreamReader(config_filename))
                {
                    do
                    {
                        currentLine = reader.ReadLine();
                        if(currentLine != null)
                        {
                            inputs.Add(new InputElement(currentLine));
                        }
                    } while(currentLine != null);
                }
            }
        }

        public void GameUpdate(float frameTime)
        {
            foreach(var input in inputs)
            {
                input.Update();
            }
        }
    }
}
