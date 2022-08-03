using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float hideTime = 5f;
        [SerializeField] GameObject PickupMesh;
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                // Destroy(gameObject);
                StartCoroutine(HideForSeconds(hideTime));
            }       
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
    }
}
