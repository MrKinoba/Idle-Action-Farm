using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class StacksPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stacksText;
        
        public static readonly UnityEvent<int,int> StacksChange = new UnityEvent<int, int>();

        private void Awake()
        {
            StacksChange.AddListener(OnStacksChange);
        }

        private void OnStacksChange(int value, int maxStacks)
        {
            stacksText.text = $"{value} / {maxStacks}";
        }
    }
}
