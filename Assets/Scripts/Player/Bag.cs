using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Wheat;

namespace Player
{
    public class Bag : MonoBehaviour
    {
        [SerializeField] private int maxAmount;
        [SerializeField] private GameObject bagParentPoint;
        [SerializeField] private float stacksOffset;
        private List<GameObject> _wheatList;

        public readonly UnityEvent<GameObject> AddWheat = new UnityEvent<GameObject>();

        private void Awake()
        {
            _wheatList = new List<GameObject>();
            AddWheat.AddListener(AddStack);
        }


        private void AddStack(GameObject newStack)
        {
            _wheatList.Add(newStack);
            newStack.GetComponent<WheatMove>().StartMoving(bagParentPoint,_wheatList.Count*stacksOffset);
        }
        public bool CheckForFreeSpace()
        {
            if (_wheatList.Count >= maxAmount)
            {
                return false;
            }

            return true;
        }
    }
}
