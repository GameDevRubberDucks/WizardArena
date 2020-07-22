using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Character_Control : MonoBehaviour
{
    //Public variables
    public float movementSpeed = 1;
    public GameObject rig;

    //private variables 
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //get and store rb reference
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //basic movement script from offical unity tutorial (https://learn.unity.com/tutorial/movement-basics#5c7f8528edbc2a002053b70f)

        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //store into the input as vec3
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if(moveHorizontal > 0.01f)
        {
            rig.transform.rotation = new quaternion(0f, 180f, 0f, 0f);
        }
        else if (moveHorizontal < -0.01f)
        {
            rig.transform.rotation = new quaternion(0f, 0f, 0f, 0f);
        }

            rb.AddForce(movement * movementSpeed);
    }


   
}
