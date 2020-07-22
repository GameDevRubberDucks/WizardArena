using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Balanced : Enemy_Base
{
    // Start is called before the first frame update
    void Start()
    {
        health = 50;
        speed = 50;
        strength = 50;
        this.GetComponent<NavMeshAgent>().speed = speed;
        player = FindObjectOfType<Character_Control>();
    }
        // Update is called once per frame
        void Update()
    {
        SeekPlayer();
    }
}
