using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.Attributes;
using System;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {   
        Fighter fighter;
        Health health;

        [SerializeField] float pickUpRange = 3f;


        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        GameObject pickUps;
        
        void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();

            pickUps = GameObject.FindWithTag("PickUp");
        }

        void Update()
        {
            if(InteractWithUI()) return;
            
            if (health.isDead) 
            {
                SetCursor(CursorType.None);
                return;
            }

            // if (IsPlayerInRange())
            // {
                if (InteractWithComponent()) return;
            // }
            if (InteractWithMovement()) return;

            SetCursor(CursorType.None);
        }

        bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit == true)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                IRayCastable[] raycastables = hit.transform.GetComponents<IRayCastable>();
                foreach (IRayCastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }


        void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }
        
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        bool IsPlayerInRange()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, pickUps.transform.position);
            return distanceToPlayer < pickUpRange;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 3f);
        }
    }

}