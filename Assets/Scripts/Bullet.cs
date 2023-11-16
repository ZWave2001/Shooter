using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(this.gameObject);
            var obj = Resources.Load<GameObject>("Entitys/Effects/fire_effect");
            var effect = Instantiate(obj, other.collider.ClosestPoint((Vector2)transform.position), Quaternion.identity);
            Destroy(effect, 0.2f);
        }
    }
}