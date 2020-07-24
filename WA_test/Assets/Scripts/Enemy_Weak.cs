using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Weak: Enemy_Base
{
    // Start is called before the first frame update
    void Start()
    {
        health = 25.0f;
        speed = 90.0f;
        strength = 10.0f;
        currentHP = health;
        this.GetComponent<NavMeshAgent>().speed = speed;
        player = FindObjectOfType<Character_Control>();
        spawner = FindObjectOfType<Spawn_Controller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SeekPlayer();
        DeathCheck();
    }
}
