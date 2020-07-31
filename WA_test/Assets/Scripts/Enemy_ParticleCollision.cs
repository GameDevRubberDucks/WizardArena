using UnityEngine;

public class Enemy_ParticleCollision : MonoBehaviour
{
    //--- Private Variables ---//
    private GameObject m_lastSpellToHit;

    // Notes:
    // - For this function to be called, there are a couple of things to keep in mind
    // - This object NEEDS a collider of some kind
    // - This object DOES NOT need a rigidbody
    // - The collision particle system in the FX NEEDS to have the "Collider" module enabled
    //      > The collision particle system NEEDS to be set to World / 3D collision
    //      > The collision particle system NEEDS to have "Send Collision Messages" enabled
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Spell_Collision")
        {
            // Check if this spell already caused damage to the enemy
            // If it did, we don't want to take damage again
            if (other.gameObject == m_lastSpellToHit)
                return;

            // Store the spell object for later to make sure we don't get hit by it again
            m_lastSpellToHit = other.gameObject;

            // Grab the spell component from the other object
            var spellObjectComp = other.GetComponentInParent<Spell_Object>();

            // Determine how much damage that spell normally does
            float spellDmg = spellObjectComp.m_spellData.m_damage;

            // Apply the damage to the enemy
            this.GetComponentInParent<Enemy_Base>().TakeDamage(spellDmg);

            // Knock the enemy back based on the spell's knockback amount
            var forceDir = this.transform.position - other.transform.position;
            forceDir.y = 0.0f;
            forceDir.Normalize();
            forceDir *= spellObjectComp.m_spellData.m_knockbackStr;
            this.GetComponentInParent<Rigidbody>().AddForce(forceDir, ForceMode.Impulse);
        }
    }
}
