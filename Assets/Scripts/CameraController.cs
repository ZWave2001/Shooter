// Author: ZWave
// Time: 2023/11/10 15:18
// --------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class CameraController : MonoSingleton<CameraController>
    {
        private Camera _camera;

        private struct PointInSpace
        {
            public Vector3 position;
            public float time;
        }

        [Header("Camera Follow")]
        [SerializeField]
        [Tooltip("The transform to follow")]
        private GameObject _followTarget;

        [SerializeField] [Tooltip("The offset between the traget and the camera")]
        private Vector3 _offset;

        [FormerlySerializedAs("delay")] [SerializeField] [Tooltip("The delay before the camera starts to follow the target")]
        private float _delay;

        [FormerlySerializedAs("speed")] [SerializeField] [Tooltip("The speed used in the lerp function when the camera follows the traget")]
        private float _speed;

        
        
        private Queue<PointInSpace> _pointsInSpace = new ();

        protected override void Awake()
        {
            base.Awake();
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            _pointsInSpace.Enqueue(new PointInSpace(){position = _followTarget.transform.position, time = Time.time});

            while (_pointsInSpace.Count > 0 && _pointsInSpace.Peek().time <= Time.time - _delay + Mathf.Epsilon)
            {
                _camera.transform.position = Vector3.Slerp(_camera.transform.position,
                    _pointsInSpace.Dequeue().position + _offset, Time.deltaTime * _speed);
            }
        }


        /// <summary>
        /// 相机振动
        /// </summary>
        /// <param name="amplitude">振动幅度</param>
        /// <param name="duration">振动时长</param>
        /// <param name="direction">相机移动方向</param>
        /// <param name="magnitude">相机移动长度</param>
        /// <returns></returns>
        public IEnumerator Shake(float amplitude, float duration, Vector2 direction = default, float magnitude = 0)
        {
            while (duration > 0)
            {
                var offsetX = Random.Range(-0.5f, 0.5f) * amplitude;
                var offsetY = Random.Range(-0.5f, 0.5f) * amplitude;
                _camera.transform.position += new Vector3(offsetX, offsetY, 0);
                
                if (direction != default)
                {
                    _camera.transform.position += new Vector3(magnitude * direction.x, magnitude * direction.y, 0) * Time.deltaTime;
                }
                
                duration -= Time.deltaTime;
                yield return null;
            }
            
        }
    }
}