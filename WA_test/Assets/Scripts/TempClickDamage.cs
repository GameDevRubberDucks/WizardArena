using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempClickDamage : MonoBehaviour
{
    public float damage = 25.0f;
    public Enemy_Balanced[] enemiesHitBalanced;
    public Enemy_Weak[] enemiesHitWeak;
    public Enemy_Strong[] enemiesHitStrong;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            enemiesHitBalanced = FindObjectsOfType<Enemy_Balanced>();
            enemiesHitWeak = FindObjectsOfType<Enemy_Weak>();
            enemiesHitStrong = FindObjectsOfType<Enemy_Strong>();
            for (int i =0 ; i < enemiesHitBalanced.Length; i++)
            {
                enemiesHitBalanced[i].TakeDamage(damage);
            }
            for (int i = 0; i < enemiesHitWeak.Length; i++)
            {
                enemiesHitWeak[i].TakeDamage(damage);
            }
            for (int i = 0; i < enemiesHitStrong.Length; i++)
            {
                enemiesHitStrong[i].TakeDamage(damage);
            }
        }
    }
}
