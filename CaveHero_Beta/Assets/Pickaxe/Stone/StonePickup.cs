using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePickup : MonoBehaviour
{
    public Transform parentStone;
    public Vector3 offset;
    public GameObject stonePickup;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(parentStone.position.x - offset.x, parentStone.position.y - offset.y, this.transform.position.z);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void OnCollisionEnter2D(Collision2D collision) {
    //    if (!parentStone.gameObject.activeSelf)
    //    {
    //        if (collision.gameObject.CompareTag("Player"))
    //        {
    //            this.gameObject.SetActive(false);
    //        }
    //    }
    //}
}
