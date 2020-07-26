using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_waveDisplay : MonoBehaviour
{
    //public stuff
    public Wave_List waveList;

    //private variables
    private TextMeshProUGUI display;

    // Start is called before the first frame update
    void Start()
    {
        waveList = FindObjectOfType<Wave_List>();
        display = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        display.text = "Wave: " + waveList.getCurrentWave().ToString();
    }
}
