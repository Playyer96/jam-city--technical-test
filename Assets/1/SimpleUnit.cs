using UnityEngine;

public class SimpleUnit : UnitHealth
{
    [Header("Simple Unit Attributes")] [SerializeField] [Range(0, 100)]
    private int initialHealth = 100;

    [SerializeField] [Range(0, 1)] private float damageReduction = 0.5f;
    [SerializeField] private bool isImmortal;

    private SimpleEffect _simpleEffect;
    private int maxHealth;
    public int Health { get; set; }

    public float DamageReduction { get; set; }

    private void Start()
    {
        Health = initialHealth;
        maxHealth = initialHealth;
        DamageReduction = damageReduction;

        _simpleEffect = GetComponent<SimpleEffect>();
    }

    // Method to take damage
    public override void Damage(int damage)
    {
        // Check if the unit is dead
        if (Health <= 0)
        {
            // The unit is dead, don't apply any more damage
            return;
        }


        if (_simpleEffect) _simpleEffect.ApplyHitEffect();
        // If the unit is not immortal, apply the reduced damage and check if the health is below 0
        if (!isImmortal)
        {
            Health -= Mathf.RoundToInt(damage * (1 - DamageReduction));

            if (Health <= 0)
            {
                if (_simpleEffect) _simpleEffect.ApplyDeadEffect();
            }
            else
            {
            }
        }
        else
        {
            // If the unit is immortal, apply the reduced damage but make sure the health doesn't drop below 1
            Health -= Mathf.RoundToInt(damage * (1 - DamageReduction));

            if (Health < 1)
            {
                Health = 1;
            }
        }
    }

    public override void Heal(int healAmount)
    {
        if (Health <= maxHealth)
            Health += healAmount;

        if (_simpleEffect) _simpleEffect.ApplyHealEffect();
    }

    public override int GetCurrentHealth()
    {
        return Health;
    }
}