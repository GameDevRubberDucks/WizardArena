using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Balanced : Enemy_Base
{
    // Start is called before the first frame update
    void Start()
    {
        health = 50.0f;
        speed = 50.0f;
        strength = 50.0f;
        currentHP = health;
        this.GetComponent<NavMeshAgent>().speed = speed;
        player = FindObjectOfType<Character_Control>();
        spawner = FindObjectOfType<Spawn_Controller>();
    }
        // Update is called once per frame
    void Update()
    {
        SeekPlayer();
        DeathCheck();
    }
}
