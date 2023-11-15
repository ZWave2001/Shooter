using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class ButtonExtension : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        public int _tapTimes;
        public float resetTapTime;
        
        public bool _isPressing;
        public float _pressingTime;
        public float duration;


        public Action OnClick;
        public Action OnLongPress;
        public Action OnDoubleClick;

        private void Start()
        {
            OnClick = (() =>
            {
                print("Click");
            });

            OnLongPress = (() =>
            {
                print("LongPress");
            });

            OnDoubleClick = (() =>
            {
                print("DoubleClick");
            });
        }

        IEnumerator ResetTapTimes()
        {
            yield return new WaitForSeconds(resetTapTime);
            _tapTimes = 0;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _tapTimes++;
            StartCoroutine(ResetTapTimes());
            OnClick?.Invoke();
            
            if (_tapTimes >= 2)
                OnDoubleClick?.Invoke();
        }

        

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressing = true;
        }
        
        
        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressing = false;
            _pressingTime = 0;
        }

        private void Update()
        {
            if (_isPressing)
            {
                _pressingTime += Time.deltaTime;
                if (_pressingTime > duration)
                {
                    OnLongPress?.Invoke();
                    _pressingTime = 0;
                    _isPressing = false;
                }
            }
        }

        
    }
}