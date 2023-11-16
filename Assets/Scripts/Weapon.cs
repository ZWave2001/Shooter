using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private Transform _shellPoint;


    [SerializeField] private Animator _animator;
    [SerializeField] private WeaponData _weaponData;

    private int _fireAnim;
    private float _readyForNextShoot;
        
    
        
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _fireAnim = Animator.StringToHash("fire");
    }

    // Update is called once per frame
    void Update()
    {
        HandleWeapon();
    }

    public void SetWeaponData(WeaponData data)
    {
        _weaponData = data;
    }

    private void HandleWeapon()
    {
        var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var shootingDirection = (mousePos - (Vector2)transform.parent.position).normalized;
        var angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;

        var tempScale = transform.localScale;
        if (angle is > -90 and < 90)
        {
            PlayerController.Instance.ChangeModelRotation(new Vector3(0, 0, 0));
            transform.localScale = new Vector3(tempScale.x, Mathf.Abs(tempScale.y), tempScale.z);
        }
        else
        {
            PlayerController.Instance.ChangeModelRotation(new Vector3(0, 180, 0));
            transform.localScale = new Vector3(tempScale.x, -Mathf.Abs(tempScale.y), tempScale.z);
        }
        
        
        transform.eulerAngles = new Vector3(0, 0, angle);


        if (Input.GetMouseButton(0))
        {
            if (Time.time > _readyForNextShoot)
            {
                _readyForNextShoot = Time.time + 1 / _weaponData.FireRate;
                Shoot(shootingDirection);
            }
        }
        
    }


    private void Shoot(Vector2 shootingDirection)
    {
      
        StartCoroutine(CameraController.Instance.
            Shake(0.1f * _weaponData.ShakeMultiple, 0.1f * _weaponData.ShakeMultiple, -shootingDirection, 2f * _weaponData.ShakeMultiple));
        _animator.SetTrigger(_fireAnim);

        var obj1 = Resources.Load<GameObject>($"Entitys/Bullets/{_weaponData.BulletType.ToString()}");
        GameObject bulletIns = Instantiate(obj1, _firePoint.position, _firePoint.rotation);
        bulletIns.GetComponent<Rigidbody2D>().AddForce(bulletIns.transform.right * _weaponData.BulletSpeed);
        Destroy(bulletIns, _weaponData.BulletLastTime);

        var obj2 = Resources.Load<GameObject>(
            $"Entitys/Shells/{_weaponData.BulletType.ToString().Replace("Bullet", "Shell")}");
        GameObject shellIns = Instantiate(obj2, _shellPoint.position, _shellPoint.rotation);
        shellIns.GetComponent<Rigidbody2D>().AddForce(new Vector2(shootingDirection.x, 2));
        Destroy(shellIns, 0.5f);
    }
    
    
}
