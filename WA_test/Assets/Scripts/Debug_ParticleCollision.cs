using UnityEngine;

public class Debug_ParticleCollision : MonoBehaviour
{
    // Notes:
    // - For this function to be called, there are a couple of things to keep in mind
    // - This object NEEDS a collider of some kind
    // - This object DOES NOT need a rigidbody
    // - The collision particle system in the FX NEEDS to have the "Collider" module enabled
    //      > The collision particle system NEEDS to be set to World / 3D collision
    //      > The collision particle system NEEDS to have "Send Collision Messages" enabled
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("OnParticleCollision() with " + other.gameObject.name);
    }

}
