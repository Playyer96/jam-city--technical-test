using UnityEngine;

public abstract class SimpleEffect : MonoBehaviour
{
    public abstract void ApplyDeadEffect();
    public abstract void ApplyHealEffect();
    public abstract void ApplyHitEffect();

    public virtual void Destroy()
    {
        // Destroy the object
        GameObject.Destroy(this.gameObject);
    }
}
