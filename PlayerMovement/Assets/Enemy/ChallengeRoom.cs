using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeRoom : MonoBehaviour
{
    public GameObject tilemapWall, endChallengeWall;
    public bool triggered;
    public GameObject Bat, Spider;
    public float randX, randY;
    public Vector2 whereToSpawn;
    public GameObject[] wave1, wave2, wave3;
    public bool wave1Trigger = false, wave2Trigger = false, wave3Trigger = false;

    // Start is called before the first frame update
    void Start()
    {
        tilemapWall.SetActive(false);
        triggered = false;
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
            wave1Trigger = true;
        }

        wave1 = GameObject.FindGameObjectsWithTag("ChallengeEnemies");
        //Debug.Log("wave 1:" + wave1.Length);

        if (wave1.Length == 0 && !wave2Trigger && wave1Trigger)
        {
            for (int i = 0; i < 3; i++)
            {
                randX = Random.Range(156.32f, 188.16f);
                randY = Random.Range(-32.66f, -20.99f);
                whereToSpawn = new Vector2(randX, randY);
                Instantiate(Spider, whereToSpawn, Quaternion.identity);
            }
            wave2Trigger = true;
        }

        if(wave2Trigger)
        {
            wave2 = GameObject.FindGameObjectsWithTag("ChallengeEnemies");
            //Debug.Log("Wave 2: " + wave2.Length);
        }
        

        if (wave2.Length == 0 && !wave3Trigger && wave2Trigger)
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
            wave3Trigger = true;
            
        }

        if(wave3Trigger)
        {
            wave3 = GameObject.FindGameObjectsWithTag("ChallengeEnemies");
            //Debug.Log("Wave 3: " + wave3.Length);
        }
        

        if (wave3.Length == 0 && wave3Trigger)
        {
            Debug.Log("Finished!!");
            tilemapWall.SetActive(false);

            endChallengeWall.SetActive(false);
        }
    }
}
