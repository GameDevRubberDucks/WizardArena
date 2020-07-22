using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weak: Enemy_Base
{
    // Start is called before the first frame update
    void Start()
    {

        health = 25;
        speed = 90;
        strength = 10;

    }

    // Update is called once per frame
    void Update()
    {
    }
}
