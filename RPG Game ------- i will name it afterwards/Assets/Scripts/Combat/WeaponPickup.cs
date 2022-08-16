using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRayCastable
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float hideTime = 5f;
        [SerializeField] GameObject PickupMesh;
        GameObject player;
        
        void Start()
        {
            player = GameObject.FindWithTag("Player");
        }
        
        void OnTriggerEnter(Fighter fighter)
        {
            if (fighter.gameObject.tag == "Player")
            {
                Pickup(fighter.GetComponent<Fighter>());
            }
        }

        private void Pickup(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
            StartCoroutine(HideForSeconds(hideTime));
        }

        IEnumerator HideForSeconds(float time)
        {
            HidePickup(false);
            yield return new WaitForSeconds(time);
            HidePickup(true);
        }

        void HidePickup(bool showPickup)
        {
            GetComponent<Collider>().enabled = showPickup;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(showPickup);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Pickup(callingController.GetComponent<Fighter>());
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}
