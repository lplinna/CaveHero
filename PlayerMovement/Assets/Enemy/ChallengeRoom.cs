using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeRoom : MonoBehaviour
{
    public GameObject tilemapWall;
    public bool triggered;
    public GameObject Bat, Spider;
    public float randX, randY;
    public Vector2 whereToSpawn;
    public int numDead;

    // Start is called before the first frame update
    void Start()
    {
        tilemapWall.SetActive(false);
        triggered = false;
        numDead = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            tilemapWall.SetActive(true);
            triggered = false;
            this.gameObject.transform.position.Set(172.44f, -12.28f, 0);
            Debug.Log("Active");

            if(numDead == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    randX = Random.Range(156.32f, 188.16f);
                    randY = Random.Range(-32.66f, -20.99f);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Bat, whereToSpawn, Quaternion.identity);
                }
                
            }
            else if (numDead == 5)
            {
                for (int i = 0; i < 3; i++)
                {
                    randX = Random.Range(156.32f, 188.16f);
                    randY = Random.Range(-32.66f, -20.99f);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Spider, whereToSpawn, Quaternion.identity);
                }
            }
            else if (numDead == 8)
            {
                for (int i = 0; i < 3; i++)
                {
                    randX = Random.Range(156.32f, 188.16f);
                    randY = Random.Range(-32.66f, -20.99f);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Spider, whereToSpawn, Quaternion.identity);
                }
                for (int i = 0; i < 3; i++)
                {
                    randX = Random.Range(156.32f, 188.16f);
                    randY = Random.Range(-32.66f, -20.99f);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Bat, whereToSpawn, Quaternion.identity);
                }
            }
        }
    }

    public void addDead(int num)
    {
        numDead += num;
    }
}
