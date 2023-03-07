using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFieldAnimation : MonoBehaviour
{
    [SerializeField] private Animator PlayerAnimator;
    [SerializeField] private GameObject Scythe;

    private void Awake()
    {
        Scythe.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Scythe.SetActive(true);
            PlayerAnimator.SetLayerWeight(1,1);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Scythe.SetActive(false);
            PlayerAnimator.SetLayerWeight(1,0);
        }
    }
}
