using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{

	private float timebeforeattack;
	public float starttimebeforeattack;

	public Transform attackPos;
	public LayerMask WhatIsEnemies;
	public float attackRange;
	public int Damage;

	void start()
	{


	}


	void update()
	{

		if (timebeforeattack <= 0)
		{

			timebeforeattack = starttimebeforeattack;

			if (Input.GetMouseButtonDown(0))
			{

				Collider2D[] EnemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsEnemies);
				for (int i = 0; i < EnemiesToDamage.Length; i++)
				{
					EnemiesToDamage[i].GetComponent<EnemyHealth>().currentHealth -= Damage;

				}
			}
		}
		else
		{
			timebeforeattack -= Time.deltaTime;
		}

	}

	void onDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackPos.position, attackRange);
	}
}
