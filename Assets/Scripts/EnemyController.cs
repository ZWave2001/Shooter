using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private Collider2D _collider;

    [SerializeField] private Rigidbody2D _rb;

    public Vector2 direction;
    public float speed;
    
    private int _walkAnim;
    // Start is called before the first frame update
    void Start()
    {
        _rb.velocity = new Vector2(speed, 0);
        _walkAnim = Animator.StringToHash("walk");
        _animator.SetBool(_walkAnim, true);
    }

    // Update is called once per frame
    void Update()
    {
        var raycastHits = Physics2D.RaycastAll(transform.position, transform.right, _collider.bounds.extents.x + 0.1f,
            LayerMask.GetMask("Ground"));
        if (raycastHits.Length != 0)
        {
            if (transform.eulerAngles.y == 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                _rb.velocity = new Vector2(-speed, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _rb.velocity = new Vector2(speed, 0);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * (_collider.bounds.extents.x + 0.1f));
    }

    public void GetHit()
    {
        _rb.AddForce(new Vector2(_rb.velocity.x > 0 ? -1 : 1, 0) * 200);
    }

    IEnumerator StartBulletTime()
    {
        yield return null;
    }
}