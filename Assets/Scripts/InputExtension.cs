// Author: ZWave
// Time: 2023/11/15 12:04
// --------------------------------------------------------------------------

using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class InputExtension : MonoBehaviour
    {
        private KeyCode _keyCode;

        public bool onPress;
        public bool onRelease;
        public bool isPress;

        public float pressingTime;

        public float releaseTime;
        
       
        public float extendingDuration;
        
        public float delayingDuration;

        
        private void Awake()
        {
            _keyCode = KeyCode.S;
        }


        public void RegisterKeyCode(KeyCode keyCode)
        {
            _keyCode = keyCode;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                onPress = true;
                onRelease = false;
                StartCoroutine(CheckExtendingDuration());
            }

            if (Input.GetKeyUp(_keyCode))
            {
                onPress = false;
                onRelease = true;
                pressingTime = 0;
            }

        }


        IEnumerator CheckExtendingDuration()
        {
            while (pressingTime < extendingDuration)
            {
                pressingTime += Time.deltaTime;
                if (onRelease)
                {
                    Debug.Log("短按");
                    yield break;
                }
                yield return null;
            }

            while (!onRelease)
            {
                Debug.Log("长按");
                yield return null;
            }
        }
    }
}