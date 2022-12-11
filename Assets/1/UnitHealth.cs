using UnityEngine;

public abstract class UnitHealth : MonoBehaviour
{
    public abstract void Damage(int damage);
    public abstract void Heal(int healAmount);
    public abstract int GetCurrentHealth();
}