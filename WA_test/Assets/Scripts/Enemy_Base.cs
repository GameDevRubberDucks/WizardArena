using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy_Base: MonoBehaviour
{
    //--- Setup Variables ---//
    [HideInInspector] public Character_Control player;
    [HideInInspector] public Spawn_Controller spawner;
    public Animator enemyAnim;
    public string enemyHitTrigger;
    public string enemyDeathTrigger;

    //--- Public Variables ---//
    public Image m_healthBarFill;

    //Enemy Stats (Out of 100 for now)
    public float speed = 0.0f;
    public float strength = 0.0f;
    public float health = 0.0f;
    public float currentHP = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
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

    public void TakeDamage(float damageRecieved)
    {
        currentHP -= damageRecieved;

        m_healthBarFill.fillAmount = currentHP / health;

        // Play the animation if there is an animator attached
        if (enemyAnim != null)
            enemyAnim.SetTrigger(enemyHitTrigger);
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

            // Play the animation if there is an animator attached
            if (enemyAnim != null)
                enemyAnim.SetTrigger(enemyDeathTrigger);
        }
    }


    // Make Basic AI 
    public void SeekPlayer()
    {
        this.GetComponent<NavMeshAgent>().destination = player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Player")
        //{
        //    DealDamage(strength, collision.gameObject);
        //}
        if (collision.gameObject.tag == "Spell_Collision")
        {
            Debug.Log("Meteor Hit");
        }
    }

}
