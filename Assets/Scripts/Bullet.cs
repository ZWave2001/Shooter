using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 bulletDirection;
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     var tempCol = other.collider;
    //     if (tempCol.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //     {
    //         Destroy(this.gameObject);
    //         var obj = Resources.Load<GameObject>("Entitys/Effects/fire_effect");
    //         var effect = Instantiate(obj, tempCol.ClosestPoint((Vector2)transform.position), Quaternion.identity);
    //         Destroy(effect, 0.2f);
    //     }
    //     
    //     if (tempCol.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //     {
    //         var enemy = other.gameObject.GetComponentInParent<EnemyController>();
    //         enemy.GetHit();
    //         Destroy(gameObject);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var tempCol = other.GetComponent<Collider2D>();
        if (tempCol.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(this.gameObject);
            var obj = Resources.Load<GameObject>("Entitys/Effects/fire_effect");
            var effect = Instantiate(obj, tempCol.ClosestPoint((Vector2)transform.position), Quaternion.identity);
            Destroy(effect, 0.2f);
        }
    
        if (tempCol.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var enemy = other.GetComponentInParent<EnemyController>();
            enemy.GetHit();
            Destroy(gameObject);
        }
    }
}