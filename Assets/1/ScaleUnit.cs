using System;
using UnityEngine;
using DG.Tweening;

public class ScaleUnit : SimpleEffect
{
    [Header("Shrink Attributes")] 
    
    [SerializeField] private float scale = 0;

    [SerializeField] private float shrinkDuration = 0.2f;

    private Sequence sequence;
    private Guid uid;

    private void OnDestroy()
    {
        DOTween.Kill(uid);
        sequence = null;
    }

    public override void ApplyDeadEffect()
    {

        sequence = DOTween.Sequence().Append(
            transform.DOScale(scale, shrinkDuration).OnComplete(() =>
            {
                // Destroy the object when the animation is complete
                base.Destroy();
            }));

        //if your sequence gets an id upon creation, you can cache 
        //it and kill it later with that id. In my case, no id was 
        //given automatically at the start, so I created one.

        uid = System.Guid.NewGuid();
        sequence.id = uid;
        Debug.Log("sequence id now:" + sequence.id);
    }

    public override void ApplyHealEffect()
    {
        sequence = DOTween.Sequence().Append(
            transform.DOScale(1.5f, shrinkDuration).OnComplete(() =>
            {
                // Destroy the object when the animation is complete
            })).Append(transform.DOScale(1, shrinkDuration));

        //if your sequence gets an id upon creation, you can cache 
        //it and kill it later with that id. In my case, no id was 
        //given automatically at the start, so I created one.

        uid = System.Guid.NewGuid();
        sequence.id = uid;
        Debug.Log("sequence id now:" + sequence.id);
    }

    public override void ApplyHitEffect()
    {
        sequence = DOTween.Sequence().Append(
            transform.DOScale(0.8f, shrinkDuration).OnComplete(() =>
            {
                // Destroy the object when the animation is complete
            })).Append(transform.DOScale(1, shrinkDuration));

        //if your sequence gets an id upon creation, you can cache 
        //it and kill it later with that id. In my case, no id was 
        //given automatically at the start, so I created one.

        uid = System.Guid.NewGuid();
        sequence.id = uid;
        Debug.Log("sequence id now:" + sequence.id);
    }
}
