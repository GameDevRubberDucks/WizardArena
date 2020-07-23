using UnityEngine;

public class Spell_Controller : MonoBehaviour
{
    //--- Public Variables ---//
    public Spell[] m_spellSlots;



    //--- Private Constants ---//
    private readonly int c_alpha0Idx = (int)KeyCode.Alpha0;
    private readonly int c_alpha9Idx = (int)KeyCode.Alpha9;



    //--- Private Variables ---//
    private Spell m_currentSpell;
    private int m_currentSpellIdx;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_currentSpell = null;
        m_currentSpellIdx = -1;
    }

    private void Update()
    {
        // Switch between spells by pressing the number keys 0 - 9
        for (int keyIdx = c_alpha0Idx, slotIdx = 0; keyIdx <= c_alpha9Idx; keyIdx++, slotIdx++)
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
    public void ToggleSpellSlot(int _slotIdx)
    {
        // Determine what spell is bound to that slot
        Spell boundSpell = m_spellSlots[_slotIdx];

        // If the spell is already active, turn it off. Otherwise, activate it
        if (boundSpell == m_currentSpell)
            m_currentSpell = null;
        else
            m_currentSpell = boundSpell;

        // TODO: Toggle the aiming indicator corresponding to the spell
        // ...

        // TODO: Update the UI indicator in the HUD to show the selected spell
        // ...
    }   

    public void CastSpell()
    {
        // TODO: Spawn the spell FX on the target indicator's current position
    }
}
