using UnityEngine;

public class Spell_Object : MonoBehaviour
{
    //--- Public Variables ---//
    public Spell_Base m_spellData;
    public ParticleSystem m_parentParticleFX;

    

    //--- Unity Methods ---//
    void Update()
    {
        // When the particle system and all of its children are finished firing, delete it
        if (!m_parentParticleFX.IsAlive(true))
            Destroy(this.gameObject);
    }
}
