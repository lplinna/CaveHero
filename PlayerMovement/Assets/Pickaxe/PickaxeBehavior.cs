using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeBehavior : MonoBehaviour
{
    public AudioSource audioSrc;

    public bool canHit;
    public bool PowerAttack;

    public static float damageModifier = 1f;
    public static float stoneDamageModifier = 1f;

    private void Start()
    {
        canHit = true;
        audioSrc = GetComponent<AudioSource>();
    }

    public void OnCollisionStay2D(Collision2D collision)
    {

        Debug.Log(collision.collider.gameObject.name);
        bool isEnemy = collision.gameObject.CompareTag("Bat") || collision.gameObject.CompareTag("Beetle") || collision.gameObject.CompareTag("Spider") || collision.gameObject.CompareTag("ChallengeEnemies");
        if (isEnemy)
        {
            Debug.Log("Enemy hit!");
            var dm = collision.gameObject.GetComponent<EnemyHealth>();
            if (canHit)
            {
                dm.Damage(30f * damageModifier);
                canHit = false;
            }

            if (PowerAttack)
            {
                dm.Damage(5f * damageModifier);
            }

        }

        if (collision.gameObject.CompareTag("Stone") && canHit)
        {
            Debug.Log("Stone hit!");
            var stone = collision.gameObject.GetComponent<StoneHealth>();
            stone.Damage(10f * stoneDamageModifier);
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
            else
            {
                audioSrc.Stop();
                audioSrc.Play();
            }
            canHit = false;
        }
    }
}
