using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class TouchToStart : MonoBehaviour
    {
        private void Start()
        {
            transform.DOShakeScale(100,0.1f,1,45);
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                DOTween.Kill(transform);
                Destroy(gameObject);
            }
        }
    }
}
