using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXUnit : SimpleEffect
{
    [System.Serializable]
    private class VFX
    {
        public GameObject vfx;
        public Transform vfxTransform;
    }

    [Header("VFX Attributes")] 
    
    [SerializeField] private VFX deadVFX;
    [SerializeField] private VFX healVFX;
    [SerializeField] private VFX hitVFX;

    public override void ApplyDeadEffect()
    {
        base.Destroy();

        InstantiateParticle(deadVFX.vfx,deadVFX.vfxTransform);
    }

    public override void ApplyHealEffect()
    {
        InstantiateParticle(healVFX.vfx,healVFX.vfxTransform);
    }

    public override void ApplyHitEffect()
    {
        InstantiateParticle(hitVFX.vfx,hitVFX.vfxTransform);
    }

    private void InstantiateParticle(GameObject vfx, Transform particleTransform)
    {
        Transform particlePosition;
        
        particlePosition = particleTransform ? particleTransform : this.transform;
        
        Instantiate(vfx, particlePosition.position, particlePosition.rotation);
    }
}
