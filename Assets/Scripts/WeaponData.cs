// Author: ZWave
// Time: 2023/11/15 16:37
// --------------------------------------------------------------------------


public class WeaponData
{
    public enum BulletTypes
    {
        RifleBullet,
        ShortGunBullet,
    }

    /// <summary>
    /// 武器名字
    /// </summary>
    public string Name
    {
        get;
        set;
    }
    
    /// <summary>
    /// 开火频率
    /// </summary>
    public float FireRate
    {
        get;
        set;
    }

    /// <summary>
    /// 振动倍数
    /// </summary>
    public float ShakeMultiple
    {
        get;
        set;
    }

    /// <summary>
    /// 子弹速度
    /// </summary>
    public float BulletSpeed
    {
        get;
        set;
    }

    /// <summary>
    /// 子弹种类
    /// </summary>
    public BulletTypes BulletType
    {
        get;
        set;
    }

    /// <summary>
    /// 子弹持续时间
    /// </summary>
    public float BulletLastTime
    {
        get
        {
            switch (BulletType)
            {
                case BulletTypes.RifleBullet:
                    return 5;
                    break;
                case BulletTypes.ShortGunBullet:
                    return 3;
                    break;
                default:
                    return 0;
            }
        }
    }
}
