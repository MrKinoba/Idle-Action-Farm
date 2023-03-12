using System;
using System.Collections;
using System.Numerics;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private GameObject barnPoint;
        [SerializeField] private RectTransform moneyIcon;
        [SerializeField] private GameObject moneyPrefab;
        [SerializeField] private GameObject parent;
        
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private int stackCost;
        
        public static readonly UnityEvent CoinSpawn = new UnityEvent();
        public static readonly UnityEvent AddMoney = new UnityEvent();

        private Camera _camera;
        private int _money;
        private bool _isAddingMoney;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            CoinSpawn.AddListener(PlayTransferAnimation);
            AddMoney.AddListener(OnMoneyAdd);
            _camera = Camera.main;
            _audioSource = GetComponent<AudioSource>();
        }

        private void PlayTransferAnimation()
        {
            var barnScreenPosition = _camera.WorldToScreenPoint(barnPoint.transform.position);
            var newMoney = Instantiate(moneyPrefab,parent.transform.position,Quaternion.identity);
            newMoney.transform.SetParent(parent.transform,false);
            newMoney.transform.position = barnScreenPosition;
            newMoney.GetComponent<MoneyAnimation>().SetTarget(moneyIcon);
        }

        private void OnMoneyAdd()
        {
            _money += stackCost;
            _audioSource.Play();
            
            if (!_isAddingMoney)
                StartCoroutine(AddMoneyRoutine());
        }

        private IEnumerator AddMoneyRoutine()
        {
            _isAddingMoney = true;
            
            transform.DOShakeRotation(10,new Vector3(0,0,5),60,30,false);
            while (int.Parse(moneyText.text)<_money-1)
            {
                moneyText.text = ((int.Parse(moneyText.text) + 2).ToString());
                yield return new WaitForEndOfFrame();
            }

            moneyText.text = _money.ToString();
            DOTween.Kill(transform);
            transform.DORotate(Vector3.zero,0.01f);
            _isAddingMoney = false;
        }
    }
}
