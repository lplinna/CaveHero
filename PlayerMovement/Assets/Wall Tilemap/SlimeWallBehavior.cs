using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWallBehavior : MonoBehaviour
{
    // Start is called before the first frame update




    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy") && other.gameObject.name!="BatEnemy")
        {
            var c = GetComponent<Collider2D>();
            var b = c.ClosestPoint(other.transform.position);
            var d = b - new Vector2(other.transform.position.x, other.transform.position.y);

            var f = Vector2.LerpUnclamped(d.normalized, other.attachedRigidbody.velocity.normalized, 2.0f);
            other.attachedRigidbody.velocity = f;

            Debug.DrawLine(other.transform.position, b, Color.red);

        }

    }
}
