using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Wheat
{
    public class WheatMove : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float speedMultiplier;
        [SerializeField] private float minDistance;

        private GameObject _target;
        private float _endOffset;


        public void StartMovingToPlayer(GameObject target, float offset)
        {
            _target = target;
            _endOffset = offset;
            StartCoroutine(MoveToPlayerRoutine());
        }

        private IEnumerator MoveToPlayerRoutine()
        {
            var vectorOffset = new Vector3(0, _endOffset, 0);
            while (Vector3.Distance(transform.position,
                       _target.transform.position + (_target.transform.up * _endOffset)) > minDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    _target.transform.position + (_target.transform.up * _endOffset), speed * Time.deltaTime);
                speed += speedMultiplier * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            transform.parent = _target.transform;
            transform.localEulerAngles = Vector3.zero;
            transform.localPosition = vectorOffset;
        }

        public void StartMovingToBarn(Vector3 barbPoint)
        {
            StartCoroutine(MoveToBarnRoutine(barbPoint));
        }
        
        private IEnumerator MoveToBarnRoutine(Vector3 barnPoint)
        {
            transform.parent = null;
            transform.DOMove(barnPoint, 0.5f);
            while (Vector3.Distance(transform.position, barnPoint) > minDistance)
            {
                yield return new WaitForEndOfFrame();
            }

            DOTween.Kill(transform);
            Destroy(gameObject);
        }
    }
}