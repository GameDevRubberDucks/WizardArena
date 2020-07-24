using UnityEngine;

[System.Serializable]
public class Spell_Slot
{
    public Spell_Base m_spell;
    [HideInInspector] public float m_cooldownTimeComplete;
    [HideInInspector] public float m_cooldownPercentage;
    [HideInInspector] public bool m_readyToUse;
}

public class Spell_Controller : MonoBehaviour
{
    //--- Public Variables ---//
    public Spell_Slot[] m_spellSlots;



    //--- Private Constants ---//
    private readonly int c_alpha1Idx = (int)KeyCode.Alpha1;
    private readonly int c_alpha9Idx = (int)KeyCode.Alpha9;



    //--- Private Variables ---//
    private AimController m_aimController;
    private Spell_Slot m_currentSpellSlot;
    private int m_currentSpellIdx;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_aimController = FindObjectOfType<AimController>();
        m_currentSpellSlot = null;
        m_currentSpellIdx = -1;

        // Init the spell slots
        InitSpellSlots();
    }

    private void Update()
    {
        // Update the spell cooldowns
        UpdateSpellCooldowns();

        // Switch between spells by pressing the number keys 1 - 9
        for (int keyIdx = c_alpha1Idx, slotIdx = 0; keyIdx <= c_alpha9Idx; keyIdx++, slotIdx++)
        {
            // Toggle the bound spells to be on or off depending on the key pressed
            if (Input.GetKeyDown((KeyCode)keyIdx))
                ToggleSpellSlot(slotIdx);
        }

        // If the player left clicks, cast the selected spell
        if (Input.GetMouseButtonDown(0))
            CastSpell();
    }



    //--- Methods ---//
    public void InitSpellSlots()
    {
        // Set up all of the spell slots
        for(int i = 0; i < m_spellSlots.Length; i++)
        {
            // Grab the reference to the spell slot and get it set up
            var spellSlot = m_spellSlots[i];
            spellSlot.m_readyToUse = true;
            spellSlot.m_cooldownPercentage = 1.0f;
            spellSlot.m_cooldownTimeComplete = spellSlot.m_spell.m_cooldownLength;
        }
    }

    public void UpdateSpellCooldowns()
    {
        // Loop through all of the spells and update their cooldowns
        for (int i = 0; i < m_spellSlots.Length; i++)
        {
            // Grab the object
            var spellSlot = m_spellSlots[i];

            // If the spell is ready to use, just move on since there is no cooldown to worry about
            if (spellSlot.m_readyToUse)
                continue;

            // Otherwise, we should update the cooldown remaining on the spell slot
            spellSlot.m_cooldownTimeComplete += Time.deltaTime;
            spellSlot.m_cooldownPercentage = spellSlot.m_cooldownTimeComplete / spellSlot.m_spell.m_cooldownLength;

            // Check if the spell is now ready to use
            spellSlot.m_readyToUse = (spellSlot.m_cooldownPercentage >= 1.0f);
        }

        // TODO: Update spell cooldown UI directly or trigger an event to do so
        // ...
    }

    public void ToggleSpellSlot(int _slotIdx)
    {
        // Grab the information about the spell slot
        Spell_Slot spellSlot = m_spellSlots[_slotIdx];

        // Grab the actual spell object from the slot
        Spell_Base boundSpell = spellSlot.m_spell;

        // If the spell is not ready to use yet, back out and continue waiting for the cooldown
        if (spellSlot.m_readyToUse)
        {
            // Toggle the aiming indicator corresponding to the spell
            m_aimController.IndicatorToggle(boundSpell.m_indicatorType);

            // If the spell is already active, turn it off. Otherwise, activate it
            if (m_currentSpellSlot != null && boundSpell == m_currentSpellSlot.m_spell)
            {
                m_currentSpellSlot = null;
                m_currentSpellIdx = -1;
            }
            else
            {
                m_currentSpellSlot = spellSlot;
                m_currentSpellIdx = _slotIdx;
            }

            // TODO: Update the UI indicator in the HUD to show the selected spell directly or through an event
            // ...
        }
    }   

    public void CastSpell()
    {
        // If there is no currently bound spell, just back out
        if (m_currentSpellSlot == null || m_currentSpellIdx == -1)
            return;

        // Spawn the spell FX on the target indicator's current position and rotation
        var targetObj = m_aimController.GetIndicatorObject();
        Vector3 targetPos = targetObj.transform.position;
        Quaternion targetRot = targetObj.transform.rotation;
        Instantiate(m_currentSpellSlot.m_spell.m_fxPrefab, targetPos, targetRot, null);

        // Disable the spell and start its cooldown
        m_currentSpellSlot.m_readyToUse = false;
        m_currentSpellSlot.m_cooldownPercentage = 0.0f;
        m_currentSpellSlot.m_cooldownTimeComplete = 0.0f;

        // Switch off the spell slot
        m_currentSpellSlot = null;
        m_currentSpellIdx = -1;

        // Deactivate the indicator
        m_aimController.DeactivateIndicator();
        //ToggleSpellSlot(m_currentSpellIdx);
    }
}
