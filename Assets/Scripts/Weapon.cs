using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform _firePoint;

    [SerializeField] private Animator _animator;

    [SerializeField] private WeaponData _data;

    private int fire;
        
    // Start is called before the first frame update
    void Start()
    {
        fire = Animator.StringToHash("fire");
    }

    // Update is called once per frame
    void Update()
    {
        HandleWeaponAimPos();
        HandleFire();
    }


    public void SetWeaponData()
    {
        
    }


    private void HandleWeaponAimPos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var targetVec = (mousePos - transform.position).normalized;
        var angle = Mathf.Atan2(targetVec.y, targetVec.x) * Mathf.Rad2Deg;

        if (angle > -90 && angle < 90)
        {
            PlayerController.Instance.ChangeModelRotation(new Vector3(0, 0, 0));
        }
        else
        {
            PlayerController.Instance.ChangeModelRotation(new Vector3(0, 180, 0));
        }
        
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void HandleFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var shootingDirection = new Vector2(mousePos.x, mousePos.y) -
                                    new Vector2(transform.position.x, transform.position.y);
            
            StartCoroutine(CameraController.Instance.Shake(0.05f, 0.05f, -shootingDirection, 0.25f));
            
            _animator.SetTrigger(fire);
        }
    }
    
    
}
