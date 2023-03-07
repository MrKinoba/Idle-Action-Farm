using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoneyAnimation : MonoBehaviour
{
    private RectTransform _target;


    private void Start()
    {
        transform.DOMove(_target.transform.position,0.5f);
        StartCoroutine(CheckForTargetHit());
    }

    public void SetTarget(RectTransform target)
    {
        _target = target;
    }

    private IEnumerator CheckForTargetHit()
    {
        while (Vector2.Distance(transform.position,_target.transform.position)>1)
        {
            yield return new WaitForEndOfFrame();
        }

        DOTween.Kill(transform);
        Destroy(gameObject);
    }
}
