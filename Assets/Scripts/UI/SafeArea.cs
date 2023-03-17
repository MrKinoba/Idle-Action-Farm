using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform _panelSafeArea;

    private void Awake()
    {
        _panelSafeArea = GetComponent<RectTransform>();
        
        ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        var safeArea = Screen.safeArea;
        
        var anchorMin = safeArea.position;
        var anchorMax = anchorMin + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        _panelSafeArea.anchorMax = anchorMax;
        _panelSafeArea.anchorMin = anchorMin;
    }
}
