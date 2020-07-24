using UnityEngine;

public class Spell_Controller : MonoBehaviour
{
    //--- Public Variables ---//
    public Spell_Base[] m_spellSlots;



    //--- Private Constants ---//
    private readonly int c_alpha1Idx = (int)KeyCode.Alpha1;
    private readonly int c_alpha9Idx = (int)KeyCode.Alpha9;



    //--- Private Variables ---//
    private AimController m_aimController;
    private Spell_Base m_currentSpell;
    private int m_currentSpellIdx;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_aimController = FindObjectOfType<AimController>();
        m_currentSpell = null;
        m_currentSpellIdx = -1;
    }

    private void Update()
    {
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
    public void ToggleSpellSlot(int _slotIdx)
    {
        // Determine what spell is bound to that slot
        Spell_Base boundSpell = m_spellSlots[_slotIdx];

        // Toggle the aiming indicator corresponding to the spell
        m_aimController.IndicatorToggle(boundSpell.m_indicatorType);

        // If the spell is already active, turn it off. Otherwise, activate it
        if (boundSpell == m_currentSpell)
        {
            m_currentSpell = null;
            m_currentSpellIdx = -1;
        }
        else
        {
            m_currentSpell = boundSpell;
            m_currentSpellIdx = _slotIdx;
        }

        // TODO: Update the UI indicator in the HUD to show the selected spell
        // ...
    }   

    public void CastSpell()
    {
        // If there is no currently bound spell, just back out
        if (m_currentSpell == null || m_currentSpellIdx == -1)
            return;

        // Spawn the spell FX on the target indicator's current position and rotation
        var targetObj = m_aimController.GetIndicatorObject();
        Vector3 targetPos = targetObj.transform.position;
        Quaternion targetRot = targetObj.transform.rotation;
        Instantiate(m_currentSpell.m_fxPrefab, targetPos, targetRot, null);

        // Deactivate the current spell
        ToggleSpellSlot(m_currentSpellIdx);
    }
}
