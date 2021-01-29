using UnityEngine;
using UnityEngine.EventSystems;

namespace EmulatedCursor
{
    public class Cube : MonoBehaviour, IInteractable
    {
        private Rigidbody _rigidbody;
        private Vector3 _startPosition;
        private Quaternion _startRotation;
            

        private const float Force = 40f;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }

        public void Reset()
        {
            _rigidbody.velocity = Vector3.zero;
            transform.position = _startPosition;
            transform.rotation = _startRotation;
        }


        public void Interact()
        {
            _rigidbody.AddForce(Vector3.forward*Force);
        }
        
    }
 
}
