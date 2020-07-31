using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    //--- Public Variables ---//
    public float maxHealth = 100.0f;
    public float currentHealth = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        DeathCheck();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "EnemyAttack")
        {
            var enemyScript = collision.gameObject.GetComponentInParent<Enemy_Base>();
            TakeDamage(enemyScript.strength);
        }
    }

    private void DeathCheck()
    {
        if (currentHealth <= 0.0f)
        {
            Debug.Log("Player is Dead");
        }
    }
}
