using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeRoom : MonoBehaviour
{
    public GameObject tilemapWall, endChallengeWall;
    public bool triggered;
    public GameObject Enemy1, Enemy2;
    public int wave1Enemy1, wave3Enemy1, wave2Enemy2, wave3Enemy2;
    public float rangeX1, rangeX2, rangeY1, rangeY2;
    public float randX, randY;
    public Vector2 whereToSpawn;
    public GameObject[] wave1, wave2, wave3;
    public bool wave1Trigger = false, wave2Trigger = false, wave3Trigger = false;
    public bool debugMode;
    public float changedPositionX, changedPositionY, changedPositionZ;

    


    // Start is called before the first frame update
    void Start()
    {
        tilemapWall.SetActive(false);


        triggered = false;
        Enemy1.GetComponent<ChallengeEnemy>().challenge = this.gameObject;
        Enemy2.GetComponent<ChallengeEnemy>().challenge = this.gameObject;
        this.gameObject.transform.position.Set(changedPositionX, changedPositionY, changedPositionZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (!debugMode)
        {


            if (triggered)
            {
                tilemapWall.SetActive(true);
                triggered = false;
                this.gameObject.transform.position = Vector3.zero;
                Debug.Log("Active");

                for (int i = 0; i < wave1Enemy1; i++)
                {
                    randX = Random.Range(rangeX1, rangeX2);
                    randY = Random.Range(rangeY1, rangeY2);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Enemy1, whereToSpawn, Quaternion.identity);
                }
                wave1Trigger = true;
            }

            wave1 = GameObject.FindGameObjectsWithTag("ChallengeEnemies");
            //Debug.Log("wave 1:" + wave1.Length);

            if (wave1.Length == 0 && !wave2Trigger && wave1Trigger)
            {
                for (int i = 0; i < wave2Enemy2; i++)
                {
                    randX = Random.Range(rangeX1, rangeX2);
                    randY = Random.Range(rangeY1, rangeY2);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Enemy2, whereToSpawn, Quaternion.identity);
                }
                wave2Trigger = true;
            }

            if (wave2Trigger)
            {
                wave2 = GameObject.FindGameObjectsWithTag("ChallengeEnemies");
                //Debug.Log("Wave 2: " + wave2.Length);
            }


            if (wave2.Length == 0 && !wave3Trigger && wave2Trigger)
            {
                for (int i = 0; i < wave3Enemy2; i++)
                {
                    randX = Random.Range(rangeX1, rangeX2);
                    randY = Random.Range(rangeY1, rangeY2);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Enemy2, whereToSpawn, Quaternion.identity);
                }
                for (int i = 0; i < wave3Enemy1; i++)
                {
                    randX = Random.Range(rangeX1, rangeX2);
                    randY = Random.Range(rangeY1, rangeY2);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Enemy1, whereToSpawn, Quaternion.identity);
                }
                wave3Trigger = true;

            }

            if (wave3Trigger)
            {
                wave3 = GameObject.FindGameObjectsWithTag("ChallengeEnemies");
                //Debug.Log("Wave 3: " + wave3.Length);
            }


            if (wave3.Length == 0 && wave3Trigger)
            {
                Debug.Log("Finished!!");
                //SoundManager.PlaySound("WinChallenge");
                tilemapWall.SetActive(false);

                endChallengeWall.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Finished!!");
            //SoundManager.PlaySound("WinChallenge");
            tilemapWall.SetActive(false);

            endChallengeWall.SetActive(false);
        }
    }
}
