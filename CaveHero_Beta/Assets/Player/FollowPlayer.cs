using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public bool stop;
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, this.transform.position.z);
        }
    }

    public void StopFollow()
    {
        stop = true;
        //transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, this.transform.position.z);
    }
}
