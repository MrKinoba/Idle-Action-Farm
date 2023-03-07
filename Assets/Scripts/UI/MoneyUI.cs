using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private GameObject barnPoint;
        [SerializeField] private RectTransform moneyIcon;
        [SerializeField] private GameObject moneyPrefab;
        [SerializeField] private GameObject parent;
        
        public static readonly UnityEvent AddMoney = new UnityEvent();

        private Camera _camera;

        private void Awake()
        {
            AddMoney.AddListener(PlayTransferAnimation);
            _camera = Camera.main;
        }

        private void PlayTransferAnimation()
        {
            var barnScreenPosition = _camera.WorldToScreenPoint(barnPoint.transform.position);
            var newMoney = Instantiate(moneyPrefab,parent.transform.position,Quaternion.identity);
            newMoney.transform.SetParent(parent.transform,false);
            newMoney.transform.position = barnScreenPosition;
            newMoney.GetComponent<MoneyAnimation>().SetTarget(moneyIcon);
        }

    }
}
