using System;
using UnityEngine;
using UnityEngine.Events;

namespace Barn
{
    public class BarnTrigger : MonoBehaviour
    {
        public static readonly UnityEvent EnterBarn = new UnityEvent();
        public static readonly UnityEvent ExitBarn = new UnityEvent();
        
        private void OnTriggerEnter(Collider other)
        {
            EnterBarn?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            ExitBarn?.Invoke();
        }
    }
}
