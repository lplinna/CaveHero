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
        Bat.AddComponent<ChallengeEnemy>();
        Spider.AddComponent<ChallengeEnemy>();
        Bat.GetComponent<ChallengeEnemy>().challenge = this.gameObject;
        Spider.GetComponent<ChallengeEnemy>().challenge = this.gameObject;
        this.gameObject.transform.position.Set(172.44f, -21.823f, -38.146f);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            tilemapWall.SetActive(true);
            triggered = false;
            this.gameObject.transform.position = Vector3.zero;
            Debug.Log("Active");

            for (int i = 0; i < 5; i++)
            {
                randX = Random.Range(156.32f, 188.16f);
                randY = Random.Range(-30.66f, -20.99f);
                whereToSpawn = new Vector2(randX, randY);
                Instantiate(Bat, whereToSpawn, Quaternion.identity);
            }
            GameObject[] wave1 = GameObject.FindGameObjectsWithTag("ChallengeEnemies");
            if (wave1.Length == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    randX = Random.Range(156.32f, 188.16f);
                    randY = Random.Range(-32.66f, -20.99f);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Spider, whereToSpawn, Quaternion.identity);
                }
                GameObject[] wave2 = GameObject.FindGameObjectsWithTag("ChallengeEnemies");
                if (wave2.Length == 0)
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
                    GameObject[] wave3 = GameObject.FindGameObjectsWithTag("ChallengeEnemies");
                    if (wave3.Length == 0)
                    {
                        Debug.Log("Finished!!");
                    }
                }
            }
        }
    }
}
