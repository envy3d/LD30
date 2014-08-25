using UnityEngine;
using System.Collections;

public abstract class AbstractCombatant : MonoBehaviour
{
    public abstract void TakeDamage(float dmgAmount);
}
