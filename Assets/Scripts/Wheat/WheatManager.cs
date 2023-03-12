using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Wheat
{
    public class WheatManager : MonoBehaviour
    {
        [SerializeField] private GameObject wheatGrown;
        [SerializeField] private GameObject wheatCut;
        [SerializeField] private GameObject stackPrefab;

        [SerializeField] private float timeToGrow;

        [SerializeField] private List<AudioClip> cutAudio;

        private BoxCollider _boxCollider;
        private AudioSource _audioSource;

        private Bag _bag;

        private void Awake()
        {
            _bag = GameObject.FindGameObjectWithTag("Player").GetComponent<Bag>();
            ChangeWheatModel(true);
            _boxCollider = GetComponent<BoxCollider>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void ChangeWheatModel(bool isGrown)
        {
            wheatCut.SetActive(!isGrown);
            wheatGrown.SetActive(isGrown);
        }
        private IEnumerator CutWheat()
        {
            var newStack = Instantiate(stackPrefab, transform.position,Quaternion.identity);
            _audioSource.clip = cutAudio[Random.Range(0, cutAudio.Count)];
            _audioSource.Play();
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
