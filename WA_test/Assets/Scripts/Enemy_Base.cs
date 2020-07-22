using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Base: MonoBehaviour
{
    //--- Public Variabls ---//

    //Enemy Stats (Out of 100 for now)
    public float speed = 0.0f;
    public float strength = 0.0f;
    public float health = 0.0f;
    public float currentHP = 0.0f;


    // Start is called before the first frame update
    void Start()
    {

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
    }


    // Make Basic AI 
    /*
     * 
     * Code Goes Here
     * 
     */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DealDamage(strength, collision.gameObject);
        }
    }

}
