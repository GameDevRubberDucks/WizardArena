using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempClickDamage : MonoBehaviour
{
    public float damage = 25.0f;
    public Enemy_Base[] enemiesHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            enemiesHit = FindObjectsOfType<Enemy_Base>();

            for (int i =0 ; i < enemiesHit.Length; i++)
            {
                enemiesHit[i].TakeDamage(damage);
            }

        }
    }
}
