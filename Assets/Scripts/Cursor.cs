using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EmulatedCursor
{
    public class Cursor : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private KeyCode button;
        private GraphicRaycaster _raycaster;
        private const float Speed = 400f;

        private void Awake()
        {
            _raycaster = FindObjectOfType<GraphicRaycaster>();
        }

        void Update()
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), 
                Input.GetAxis("Vertical"));

            if (input != Vector2.zero)
            {
                transform.Translate(input*Time.deltaTime*Speed);
            }
            
            if (Input.GetKey(button))
            {
                ObjectTouch();
                UiTouch();
            }
        }

        void ObjectTouch()
        {
            Ray ray = Camera.main.ScreenPointToRay(transform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50))
            {
                var cube = hit.collider.GetComponent<IInteractable>();

                cube?.Interact();
            }
        }

        void UiTouch()
        {
            var pointerEvent = new PointerEventData(EventSystem.current);
            pointerEvent.position = transform.position;
            var raycastResults = new List<RaycastResult>();
            _raycaster.Raycast(pointerEvent,raycastResults);
            foreach (var result in raycastResults)
            {
                ExecuteEvents.Execute<IPointerClickHandler>(result.gameObject, pointerEvent,
                    ExecuteEvents.pointerClickHandler);
            }
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
    

}
