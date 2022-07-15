using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon" ,menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject WeaponPrefab = null;
        [SerializeField] float playerDamage = 20;
        [SerializeField] AnimatorOverrideController weaponOverride = null;
        [SerializeField] AnimatorOverrideController enemyWeaponOverride = null;
        [SerializeField] bool isRightHand = true;
        [SerializeField] float weaponRange = 2f;
        
        public void Spawn(Transform rightHandTransform, Transform leftHandTransform ,Animator animator)
        {
            if (WeaponPrefab != null)
            {
                Transform handTransform;
                if (isRightHand == true) handTransform = rightHandTransform;
                else handTransform = leftHandTransform;
                Instantiate(WeaponPrefab, handTransform);
            }

            if (weaponOverride != null)
            {
                GameObject player = GameObject.FindWithTag("Player");;

                if (player.tag == "Player")
                {
                    animator.runtimeAnimatorController = weaponOverride;
                }
                else
                {   
                    if (enemyWeaponOverride == null) return;
                    animator.runtimeAnimatorController = enemyWeaponOverride;
                }
            }
        }

        public float GetDamage()
        {
            return playerDamage;
        }

        public float GetRange()
        {
            return weaponRange;
        }
    }
}