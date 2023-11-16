using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _model;

    [SerializeField] private Vector2 _direction;

    public float extendingDuration;
    public float pressingTime;
    public bool onRelease;
    
    private int _walkAnim;
    private int _jumpAnim;

    public bool isGrounded;

    public int weaponIndex;

    [SerializeField] private WeaponHandler _weaponHandler;
    public event EventHandler OnFire;
    public class FireArgs : EventArgs
    {
        
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        _walkAnim = Animator.StringToHash("walk");
        _jumpAnim = Animator.StringToHash("jump");
        _weaponHandler = GetComponentInChildren<WeaponHandler>();

        weaponIndex = 0;
        _weaponHandler.SetWeapon(weaponIndex);
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
            _animator.SetBool(_walkAnim, true);

            // transform.rotation = Quaternion.Euler(0,_direction.x > 0.2f ? 0 : 180, 0);
        }
        else
        {
            _animator.SetBool(_walkAnim, false);
        }
        
        _rb.velocity = new Vector2(_direction.x * CharacterData.speed, _rb.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onRelease = false;
            StartCoroutine(CheckExtendingDuration());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            onRelease = true;
            pressingTime = 0;
        }
        
        //Make Jumping Feel Better 
        if (_rb.velocity.y > 0)
            _rb.gravityScale = CharacterData.JumpUpGravity;
        else
            _rb.gravityScale = CharacterData.JumpDownGravity;
        #endregion
        
        #region CheckGround
        var colTrans = _collider.transform;
        var origin = colTrans.position - _collider.bounds.extents.y * colTrans.up;
        
        var raycastAll = Physics2D.RaycastAll(origin, -colTrans.up, 0.2f, LayerMask.GetMask("Ground"));
        isGrounded = (raycastAll.Length != 0);
        #endregion

        #region ChangeWeapon

        if (Input.GetKeyDown(KeyCode.Q))
        {
            weaponIndex += 1;
            if (weaponIndex > 1)
                weaponIndex = 0;
            _weaponHandler.SetWeapon(weaponIndex);
        }
        

        #endregion
    }


    
    IEnumerator CheckExtendingDuration()
    {
        while (pressingTime < extendingDuration)
        {
            pressingTime += Time.deltaTime;
            if (onRelease)
            {
                // Debug.Log("短按");
                Jump(CharacterData.shortJumpHeight);
                yield break;
            }
            yield return null;
        }

        if (!onRelease)
        {
            // Debug.Log("长按");
            Jump(CharacterData.longJumpHeight);
        }
    }

    private void Jump(float height)
    {
        if (isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x,
                Mathf.Sqrt(Physics2D.gravity.y * -2 * height * CharacterData.JumpUpGravity));
            _animator.SetTrigger(_jumpAnim);
        }
    }
    
    
    
    private void OnDrawGizmos()
    {
        var colTrans = _collider.transform;
        var origin = colTrans.position - _collider.bounds.extents.y * colTrans.up;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin - colTrans.up * 0.2f);
    }
}
