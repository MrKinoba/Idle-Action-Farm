using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UI;
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
        while (Vector2.Distance(transform.position,_target.transform.position)>15)
        {
            yield return new WaitForEndOfFrame();
        }
        MoneyUI.AddMoney?.Invoke();
        
        DOTween.Kill(transform);
        Destroy(gameObject);
    }
}
