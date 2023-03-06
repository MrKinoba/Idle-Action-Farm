using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private Joystick joystick;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var movement = joystick.Movement;
        
        if (movement != Vector2.zero)
        {
            ApplyVelocity(movement);
            ApplyRotation(movement);
        }
        else
        {
            Idle();
        }
    }

    private void Idle()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
    
    private void ApplyRotation(Vector2 movement)
    {
        _rigidbody.transform.localRotation = Quaternion.LookRotation(_rigidbody.velocity);
    }

    private void ApplyVelocity(Vector2 movement)
    {
        _rigidbody.velocity = new Vector3(movement.x, 0, movement.y) * maxSpeed;
    }
}