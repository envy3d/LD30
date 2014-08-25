using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

    public LayerMask layersToHit;
    public float damage;

    public bool active = false;

    void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (layersToHit.IsInLayerMask(other.gameObject.layer))
            {
                other.GetComponent<Combatant>().TakeDamage(damage);
            }
        }
    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }
}
