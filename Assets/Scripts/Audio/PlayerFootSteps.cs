using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerFootSteps : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footSteps;

    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnFootStep()
    {
        _audioSource.clip = footSteps[Random.Range(0, footSteps.Count)];
        _audioSource.Play();
    }
}
