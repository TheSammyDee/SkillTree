using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public class Debugger : SingletonBehaviour<Debugger>
    {
        [SerializeField]
        bool debugOnScreen = false;

        [SerializeField]
        bool debugInConsole = true;

        [SerializeField]
        Text text;

        private void Start()
        {
            text.gameObject.SetActive(debugOnScreen);
        }

        public void Log(string message)
        {
            if (debugInConsole)
                Debug.Log(message);

            if (debugOnScreen)
                text.text = text.text + message + "\n";
        }
    }
}