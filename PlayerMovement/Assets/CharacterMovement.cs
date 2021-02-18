using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float currEnergy = 0.0f;
    public float maxEnergy = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        currEnergy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        // Aiming for Character !!DOES NOT WORK WHEN CAMERA IS ATTACHED TO PLAYER!!
        //var posMouse = Camera.main.WorldToScreenPoint(transform.position);
        //var dirMouse = Input.mousePosition - posMouse;
        //var angleMouse = Mathf.Atan2(dirMouse.y, dirMouse.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angleMouse, Vector3.forward);


        Vector3 pos = transform.position;

        Debug.Log(currEnergy + " " + speed);
        /**
         * If energy is at zero, no sprint
         * If energy is above 10, allow sprint
         * If energy reaches 0, stop sprint
         * Energy max is 100
         */
        //if (Input.GetKey("left shift"))
        //{
        //    if (!(currEnergy <= 0))
        //    {
        //        speed = 14;
        //        Drain(0.12f);
        //    }
        //    if (currEnergy < 10)
        //    {
        //        speed = 8;
        //    }
        //}
        //else
        //{
        //    speed = 10;
        //    if (currEnergy < 100)
        //    {
        //        Gain(0.1f);
        //    }
        //}

        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
        }

        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
        }

        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;
    }

   
}
