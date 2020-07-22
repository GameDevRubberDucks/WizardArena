using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Weak: Enemy_Base
{
    // Start is called before the first frame update
    void Start()
    {

        health = 25;
        speed = 90;
        strength = 10;
        this.GetComponent<NavMeshAgent>().speed = speed;
        player = FindObjectOfType<Character_Control>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SeekPlayer();
    }
}
