using System.Collections;
using System.Collections.Generic;
using Barn;
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

        [SerializeField] private float barnTransferDelay;
        [SerializeField] private Transform barnPoint;
        
        private List<(GameObject,WheatMove)> _wheatList;
        private bool _inBarn;

        public readonly UnityEvent<GameObject> AddWheat = new UnityEvent<GameObject>();

        private void Awake()
        {
            _inBarn = false;
            _wheatList = new List<(GameObject,WheatMove)>();
            AddWheat.AddListener(AddStack);
            BarnTrigger.EnterBarn.AddListener(StartTransferToBarn);
            BarnTrigger.ExitBarn.AddListener(StopTransferToBarn);
        }


        private void AddStack(GameObject newStack)
        {
            _wheatList.Add((newStack,newStack.GetComponent<WheatMove>()));
            _wheatList[_wheatList.Count - 1].Item2.StartMovingToPlayer(bagParentPoint,_wheatList.Count*stacksOffset);
        }
        public bool CheckForFreeSpace()
        {
            if (_wheatList.Count >= maxAmount)
            {
                return false;
            }

            return true;
        }

        private void StartTransferToBarn()
        {
            _inBarn = true;
            StartCoroutine(TransferToBarn());
        }

        private void StopTransferToBarn()
        {
            _inBarn = false;
        }

        private IEnumerator TransferToBarn()
        {
            while (_inBarn && _wheatList.Count!=0)
            {
                _wheatList[_wheatList.Count - 1].Item2.StartMovingToBarn(barnPoint.position);
                _wheatList.Remove(_wheatList[_wheatList.Count - 1]);
                yield return new WaitForSeconds(barnTransferDelay);
            }
        }
    }
}
