using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeRoom : MonoBehaviour
{
    public GameObject tilemapWall, endChallengeWall;
    public bool triggered;
    public GameObject Enemy1, Enemy2, Enemy3;
    public int wave1Enemy1, wave2Enemy1, wave3Enemy1, wave1Enemy2, wave2Enemy2, wave3Enemy2, wave1Enemy3, wave2Enemy3, wave3Enemy3;
    public float rangeX1, rangeX2, rangeY1, rangeY2;
    public float randX, randY;
    public Vector2 whereToSpawn;
    public GameObject[] wave1, wave2, wave3;
    public bool wave1Trigger = false, wave2Trigger = false, wave3Trigger = false;
    public bool debugMode;
    public float changedPositionX, changedPositionY, changedPositionZ;
    public DoNotDestroy challengeRoomTrigger = GameObject.FindGameObjectWithTag("Player").GetComponent<DoNotDestroy>();


    // Start is called before the first frame update
    void Start()
    {
        tilemapWall.SetActive(false);
        triggered = false;
        Enemy1.GetComponent<ChallengeEnemy>().challenge = this.gameObject;
        Enemy2.GetComponent<ChallengeEnemy>().challenge = this.gameObject;
        Enemy3.GetComponent<ChallengeEnemy>().challenge = this.gameObject;

        changeEnemySprites(Enemy1);
        changeEnemySprites(Enemy2);
        changeEnemySprites(Enemy3);

        this.gameObject.transform.position.Set(changedPositionX, changedPositionY, changedPositionZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (!debugMode)
        {
            if (triggered && !challengeRoomTrigger.getChallengeTrigger())
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

                for (int i = 0; i < wave1Enemy2; i++)
                {
                    randX = Random.Range(rangeX1, rangeX2);
                    randY = Random.Range(rangeY1, rangeY2);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Enemy2, whereToSpawn, Quaternion.identity);
                }

                for (int i = 0; i < wave1Enemy3; i++)
                {
                    randX = Random.Range(rangeX1, rangeX2);
                    randY = Random.Range(rangeY1, rangeY2);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Enemy3, whereToSpawn, Quaternion.identity);
                }
                wave1Trigger = true;
                Debug.Log("Wave1");
            }

            wave1 = GameObject.FindGameObjectsWithTag("ChallengeEnemies");
            //Debug.Log("wave 1:" + wave1.Length);

            if (wave1.Length == 0 && !wave2Trigger && wave1Trigger)
            {
                for (int i = 0; i < wave2Enemy1; i++)
                {
                    randX = Random.Range(rangeX1, rangeX2);
                    randY = Random.Range(rangeY1, rangeY2);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Enemy1, whereToSpawn, Quaternion.identity);
                }

                for (int i = 0; i < wave2Enemy2; i++)
                {
                    randX = Random.Range(rangeX1, rangeX2);
                    randY = Random.Range(rangeY1, rangeY2);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Enemy2, whereToSpawn, Quaternion.identity);
                }

                for (int i = 0; i < wave2Enemy3; i++)
                {
                    randX = Random.Range(rangeX1, rangeX2);
                    randY = Random.Range(rangeY1, rangeY2);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Enemy3, whereToSpawn, Quaternion.identity);
                }
                wave2Trigger = true;
                Debug.Log("Wave2");
            }

            if (wave2Trigger)
            {
                wave2 = GameObject.FindGameObjectsWithTag("ChallengeEnemies");
                //Debug.Log("Wave 2: " + wave2.Length);
            }


            if (wave2.Length == 0 && !wave3Trigger && wave2Trigger)
            {
                for (int i = 0; i < wave3Enemy1; i++)
                {
                    randX = Random.Range(rangeX1, rangeX2);
                    randY = Random.Range(rangeY1, rangeY2);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Enemy1, whereToSpawn, Quaternion.identity);
                }

                for (int i = 0; i < wave3Enemy2; i++)
                {
                    randX = Random.Range(rangeX1, rangeX2);
                    randY = Random.Range(rangeY1, rangeY2);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Enemy2, whereToSpawn, Quaternion.identity);
                }

                for (int i = 0; i < wave3Enemy3; i++)
                {
                    randX = Random.Range(rangeX1, rangeX2);
                    randY = Random.Range(rangeY1, rangeY2);
                    whereToSpawn = new Vector2(randX, randY);
                    Instantiate(Enemy3, whereToSpawn, Quaternion.identity);
                }
                wave3Trigger = true;
                Debug.Log("Wave3");
            }

            if (wave3Trigger)
            {
                wave3 = GameObject.FindGameObjectsWithTag("ChallengeEnemies");
                //Debug.Log("Wave 3: " + wave3.Length);
            }


            if (wave3.Length == 0 && wave3Trigger)
            {
                if (!challengeRoomTrigger.getChallengeTrigger())
                {
                    Debug.Log("Finished!!");
                    SoundManager.PlaySound("WinChallenge");
                    tilemapWall.SetActive(false);

                    endChallengeWall.SetActive(false);
                    challengeRoomTrigger.setChallengeTrigger(true);
                }
            }
        }
        else
        {
            if (!challengeRoomTrigger.getChallengeTrigger())
            {
                Debug.Log("Finished!!");
                SoundManager.PlaySound("WinChallenge");
                tilemapWall.SetActive(false);

                endChallengeWall.SetActive(false);
                challengeRoomTrigger.setChallengeTrigger(true);
            }
        }
    }

    public void changeEnemySprites(GameObject enemy) 
    {
        if (enemy.name.Contains("Bat"))
        {
            if (SceneManager.GetActiveScene().name == "SlimeLevel")
            {
                enemy.GetComponent<BatBehavior>().batType = 0;
            }
            else
            {
                enemy.GetComponent<BatBehavior>().batType = 2;
            }
        }

        if (enemy.name.Contains("Beetle"))
        {
            if (SceneManager.GetActiveScene().name == "IceLevel")
            {
                enemy.GetComponent<BeetleBehavior>().beetleType = 1;
            }
            else
            {
                enemy.GetComponent<BeetleBehavior>().beetleType = 2;
            }
        }

        if (enemy.name.Contains("Spider"))
        {
            if (SceneManager.GetActiveScene().name == "SlimeLevel")
            {
                enemy.GetComponent<SpiderBehavior>().spiderType = 0;
            }
            else if (SceneManager.GetActiveScene().name == "IceLevel")
            {
                enemy.GetComponent<SpiderBehavior>().spiderType = 1;
            }
            else
            {
                enemy.GetComponent<SpiderBehavior>().spiderType = 2;
            }
        }
    }

}
