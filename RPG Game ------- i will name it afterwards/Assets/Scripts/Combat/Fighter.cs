using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        bool isManualAttacking = false;
        Health target;
        Mover mover;
        Animator animator;
        [SerializeField] float attackSpeed = 1;
        float timeSinceLastAttack = 0;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaltWeapon = null;
        [SerializeField] string defaltWeaponName = "Unarmed";
        Weapon currentWeapon = null;
        


        void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();

            Weapon weapon = Resources.Load<Weapon>(defaltWeaponName);
            EquipWeapon(weapon);
        }

        public void EquipWeapon(Weapon weapon)
        {   
            currentWeapon = weapon;
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (target.isDead) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();

                if (gameObject.tag == "Player")
                {
                    // for setting of manual attacking or auto attacking
                    if (isManualAttacking)
                    {
                        ManualAttackBehaviour();
                    }
                    else if (!isManualAttacking)
                    {
                        AutoAttackBehaviour();
                    }
                    
                }
                else
                {
                    AutoAttackBehaviour();
                }
            }
        }

        void AutoAttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > attackSpeed)
            {
                animator.SetTrigger("isAttacking");
                timeSinceLastAttack = 0;
            }
        }

        void ManualAttackBehaviour()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (timeSinceLastAttack > attackSpeed)
                {
                    // TakeDamage() metord is called in the Hit() event
                    animator.SetTrigger("isAttacking");
                    timeSinceLastAttack = 0;
                    transform.LookAt(target.transform);
                }
            }
        }

        // this is a animation event
        void Hit()
        {
            if (target == null) return;

            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetDamage());
            }
        }

        void Shoot()
        {
            Hit();
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }

        public void Attack(GameObject combatTarget)
        {
            // GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            
            Health testToTarget = combatTarget.GetComponent<Health>();
            if (!testToTarget.isDead && testToTarget != null)
            {
                return true;
            }
            return false;
        }

        public void Cancel()
        {
            // animator.SetTrigger("dontAttack");
            target = null;
            GetComponent<Mover>().Cancel();
        }

    }

}
