using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform _firePoint;

    public EventHandler<ShootingArgs> OnShoot;

    public class ShootingArgs : EventArgs
    {
        
    }
        
        
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        HandleWeaponAimPos();
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
    
    
}
