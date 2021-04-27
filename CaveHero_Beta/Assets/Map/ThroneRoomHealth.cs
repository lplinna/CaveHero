using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneRoomHealth : MonoBehaviour
{
    public float rangeX1, rangeX2, rangeY1, rangeY2;
    public float randX, randY;
    public Vector2 whereToSpawn;
    public GameObject healthPot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startSpawn()
    {
        StartCoroutine(spawn());
    }

    public void stopSpawn()
    {
        StopCoroutine(spawn());
    }

    private void whereToSpawnObjects()
    {
        randX = Random.Range(rangeX1, rangeX2);
        randY = Random.Range(rangeY1, rangeY2);
        whereToSpawn = new Vector2(randX, randY);
    }

    IEnumerator spawn()
    {
       while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                whereToSpawnObjects();
                Instantiate(healthPot, whereToSpawn, Quaternion.identity);
            }
            yield return new WaitForSeconds(25f);
        }
    }
}
