using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Wheat
{
    public class WheatManager : MonoBehaviour
    {
        [SerializeField] private GameObject wheatGrown;
        [SerializeField] private GameObject wheatCut;
        [SerializeField] private GameObject stackPrefab;

        [SerializeField] private float timeToGrow;
        
        private BoxCollider _boxCollider;

        private Bag _bag;

        private void Awake()
        {
            _bag = GameObject.FindGameObjectWithTag("Player").GetComponent<Bag>();
            ChangeWheatModel(true);
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void ChangeWheatModel(bool isGrown)
        {
            wheatCut.SetActive(!isGrown);
            wheatGrown.SetActive(isGrown);
        }
        private IEnumerator CutWheat()
        {
            var newStack = Instantiate(stackPrefab, transform.position,Quaternion.identity);
            _bag.AddWheat?.Invoke(newStack);
            ChangeWheatModel(false);
            _boxCollider.enabled = false;
            
            yield return new WaitForSeconds(timeToGrow);
            
            ChangeWheatModel(true);
            _boxCollider.enabled = true;
        } 
        
        private void OnTriggerEnter(Collider other)
        {
            if (_bag.CheckForFreeSpace() && other.CompareTag("Scythe"))
            {
                StartCoroutine(CutWheat());
            }
        }
    }
}
