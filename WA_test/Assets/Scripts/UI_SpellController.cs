using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SpellController : MonoBehaviour
{
    public Spell_Controller spellController;
    public GameObject[] slot;


    //private vaiarbles
    private Image[] icon;
    private Image[] CDshading;
    private TextMeshProUGUI[] timer;
    private TextMeshProUGUI[] slotLabel;

    //for conviant variables

    private float[] cd_percent;
    private float[] cd_timer;

    // Start is called before the first frame update
    void Start()
    {
        spellController = FindObjectOfType<Spell_Controller>();

        //init local vars
        icon = new Image[slot.Length];
        CDshading = new Image[slot.Length];
        timer = new TextMeshProUGUI[slot.Length];
        slotLabel = new TextMeshProUGUI[slot.Length];
        cd_timer = new float[slot.Length];
        cd_percent = new float[slot.Length];


       
        //set CD and sprite icons.
        for (int i = 0; i < slot.Length; i++)
        {
            icon[i] = slot[i].transform.Find("Skill Icon").GetComponent<Image>();
            CDshading[i] = slot[i].transform.Find("Cooldown Shading").GetComponent<Image>();
            timer[i] = slot[i].transform.Find("Timer").GetComponent<TextMeshProUGUI>();
            slotLabel[i] = slot[i].transform.Find("Slot keylabel").GetComponent<TextMeshProUGUI>();

            //get the sprite once we have it.
            icon[i].sprite = spellController.m_spellSlots[i].m_spell.m_icon;

            //get slot label
            slotLabel[i].text = (i+1).ToString();
        }

        //update the cd timers
        timerUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        timerUpdate();
        //update each spell's spell display
        for (int i = 0; i < slot.Length; i++)
        {
            //if cd is ready, then fill amount should be 0 and the timer should be hidden
            if (spellController.m_spellSlots[i].m_readyToUse)
            {
                CDshading[i].fillAmount = 0.0f;
                timer[i].gameObject.SetActive(false);
            }
            else
            {
                //re-enable the timer display
                timer[i].gameObject.SetActive(true);
                //get the cooldown percentage from the skill controller
                CDshading[i].fillAmount = cd_percent[i];
                //calculate the remaining timer cd
                timer[i].text = (cd_timer[i]).ToString("#.0");
            }



           
        }

    }

    //updates all the cd stuff such as the percentage and the timer count down.
    private void timerUpdate()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            cd_percent[i] = spellController.m_spellSlots[i].m_cooldownPercentage;
            cd_timer[i] = spellController.m_spellSlots[i].m_spell.m_cooldownLength - (spellController.m_spellSlots[i].m_spell.m_cooldownLength * cd_percent[i]);
        }
    }

}
