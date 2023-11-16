using UnityEngine;

namespace DefaultNamespace
{
    public class WeaponHandler : MonoBehaviour
    {
        private GameObject _currentWeapon;
        
        public WeaponData[] weaponDatas = new[]
        {
            new WeaponData()
            {
                Name = "Deagle",
                BulletSpeed = 2000,
                BulletType = WeaponData.BulletTypes.ShortGunBullet,
                FireRate = 2,
                ShakeMultiple = 1,
            },
            new WeaponData()
            {
                Name = "Ak47",
                BulletSpeed = 2000,
                BulletType = WeaponData.BulletTypes.RifleBullet,
                FireRate = 10,
                ShakeMultiple = 2,
            },
        };
        
        
        public void SetWeapon(int weaponIndex)
        {
            if (_currentWeapon != null)
                DestroyImmediate(_currentWeapon);
            
            var obj = Resources.Load<GameObject>($"Entitys/Weapons/{weaponDatas[weaponIndex].Name}");
            _currentWeapon = Instantiate(obj, transform.position, Quaternion.identity, transform);
            _currentWeapon.GetComponent<Weapon>().SetWeaponData(weaponDatas[weaponIndex]);
        }
        
    }
}