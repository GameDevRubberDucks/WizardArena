using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Base: MonoBehaviour
{
    //--- Setup Variables ---//
    public Character_Control player;
    public Spawn_Controller spawner;
    //--- Public Variables ---//

    //Enemy Stats (Out of 100 for now)
    public float speed = 0.0f;
    public float strength = 0.0f;
    public float health = 0.0f;
    public float currentHP = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        //Find PLayer once we have a script on it
        player = FindObjectOfType<Character_Control>();
        spawner = FindObjectOfType<Spawn_Controller>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damageRecieved)
    {
        currentHP -= damageRecieved;
    }
    public void DealDamage(float damageDealt, GameObject player)
    {
        player.GetComponentInChildren<SpriteRenderer>().color = this.GetComponent<SpriteRenderer>().color;
        player.GetComponent<Player_Health>().TakeDamage(damageDealt);
    }

    public void DeathCheck()
    {
        if (currentHP <= 0.0f)
        {   
            spawner.EnemyKilled(this.gameObject);
        }
    }


    // Make Basic AI 
    public void SeekPlayer()
    {
        this.GetComponent<NavMeshAgent>().destination = player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DealDamage(strength, collision.gameObject);
        }
    }

}
