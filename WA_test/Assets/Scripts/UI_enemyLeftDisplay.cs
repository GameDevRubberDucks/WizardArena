using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class UI_enemyLeftDisplay : MonoBehaviour
{

    // Public variable
    public Spawn_Controller spawn_Controller;

    // private variable
    private TextMeshProUGUI display;

    // Start is called before the first frame update
    void Start()
    {
        spawn_Controller = FindObjectOfType<Spawn_Controller>();
        display = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        display.text = "Enemy Left: " + spawn_Controller.getEnemyLeft();
    }
}
