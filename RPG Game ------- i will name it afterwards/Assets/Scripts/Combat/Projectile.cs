using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Attributes;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed;
        [SerializeField] bool isHoming;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 2f;
        GameObject instigator = null;
        Health target = null;
        [SerializeField] float damage = 0f;

        void Start()
        {
            if (target == null) return;
            if (!isHoming) transform.LookAt(GetAimLocation());
        }
    
        void Update()
        {
            if (target == null) return;
            if (isHoming && !target.isDead) 
            {
                 transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }

        public void SetTarget(Health target, GameObject instigator,float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (target.isDead) return;
            target.TakeDamage(instigator ,damage);

            projectileSpeed = 0f;

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }
        
            foreach (GameObject hits in destroyOnHit)
            {
                Destroy(hits);
            }

            Destroy(gameObject, lifeAfterImpact);
        }
    }

}