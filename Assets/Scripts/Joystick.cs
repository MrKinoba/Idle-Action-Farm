using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    [SerializeField]private Image joystickZoneImage;
    [SerializeField]private Image joystickImage;
    
    private bool _touchStart = false;
    
    private RectTransform _touchArea;
    private float _radius;

    private Vector2 _startPosition;
    private Vector2 _touchPosition;
    private Vector2 _joystickPosition;

    
    public Vector2 Movement {get; private set;  }

    private void Awake()
    {
        ChangeVisibility(false);
        _touchArea = GetComponent<RectTransform>();
        _radius = joystickZoneImage.rectTransform.sizeDelta.x;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (!_touchStart && RectTransformUtility.RectangleContainsScreenPoint(_touchArea,touch.position))
            {
                _touchStart = true;
            }

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startPosition = touch.position;
                    ChangeVisibility(true);
                    joystickZoneImage.rectTransform.position = touch.position;
                    break;
                
                case TouchPhase.Moved:
                    _touchPosition = touch.position;
                    _joystickPosition  = Vector2.ClampMagnitude(_touchPosition-_startPosition, _radius);
                    Movement = _joystickPosition.normalized;
                    joystickImage.rectTransform.position = _joystickPosition + _startPosition;
                    break;
            }
        }
        else
        {
            ChangeVisibility(false);
            _touchStart = false;
            Movement = Vector2.zero;
        }
    }

    private void ChangeVisibility(bool Visibility)
    {
        joystickZoneImage.enabled = Visibility;
        joystickImage.enabled = Visibility;
    }
    
    private void OnDisable()
    {
        Movement = Vector2.zero;
    }
}
