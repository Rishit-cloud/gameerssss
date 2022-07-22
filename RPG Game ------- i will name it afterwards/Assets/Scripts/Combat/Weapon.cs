using UnityEngine;
using RPG.Core;

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
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";
        
        public void Spawn(Transform rightHandTransform, Transform leftHandTransform ,Animator animator)
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);
            
            if (WeaponPrefab != null)
            {   
                Transform handTransform;
                if (isRightHand == true) handTransform = rightHandTransform;
                else handTransform = leftHandTransform;
                GameObject weapon = Instantiate(WeaponPrefab, handTransform);
                weapon.name = weaponName;
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

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHandTransform, Transform leftHandTransform, Health target)
        {
            Transform handTransform;
            if (isRightHand == true) handTransform = rightHandTransform;
            else handTransform = leftHandTransform;
            Projectile projectileInstance = Instantiate(projectile, handTransform.position, Quaternion.identity);
            projectileInstance.SetTarget(target, playerDamage);
        }

        public float GetDamage()
        {
            return playerDamage;
        }

        public float GetRange()
        {
            return weaponRange;
        }

        void DestroyOldWeapon(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform oldWeapon = rightHandTransform.Find(weaponName);
            if (oldWeapon == null) oldWeapon = leftHandTransform.Find(weaponName);
            if (oldWeapon == null) return;

            oldWeapon.name = "Destroying";
            Destroy(oldWeapon.gameObject);
        }
    }
}