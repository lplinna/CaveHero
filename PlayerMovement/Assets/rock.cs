using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{

    public GameObject pickup;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    
    }

    private void OnDestroy()
    {
        Instantiate(pickup, transform.position, drop.transform.rotation);
    }
}
