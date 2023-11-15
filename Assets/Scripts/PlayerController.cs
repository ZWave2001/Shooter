using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _model;

    [SerializeField] private Vector2 _direction;


    private int _walk;
    private int _jump;

    public event EventHandler OnFire;

    public class FireArgs : EventArgs
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        _walk = Animator.StringToHash("walk");
        _jump = Animator.StringToHash("jump");
    }

    public void ChangeModelRotation(Vector3 angles)
    {
        _model.eulerAngles = angles;
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        _direction.x = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(_direction.x) > 0.1f)
        {
            _animator.SetBool(_walk, true);

            // transform.rotation = Quaternion.Euler(0,_direction.x > 0.2f ? 0 : 180, 0);
        }
        else
        {
            _animator.SetBool(_walk, false);
        }
        
        _rb.velocity = new Vector2(_direction.x * CharacterData.speed, _rb.velocity.y);
        
        
       

        #endregion
        
        
    }
}
