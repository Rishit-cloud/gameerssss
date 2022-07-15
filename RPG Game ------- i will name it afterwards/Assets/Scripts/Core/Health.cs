using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Combat;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        AIController aiController;
        Fighter fighter;
        PlayerController playerController;
        NavMeshAgent navMeshAgent;
        Animator animator;
        [SerializeField] float enemyHealth = 100f;
        public bool isDead = false;

        void Start()
        {
            aiController = GetComponent<AIController>();
            animator = GetComponent<Animator>();
            fighter = GetComponent<Fighter>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            playerController = GetComponent<PlayerController>();
        }
        
        public void TakeDamage(float playerDamage)
        {
            enemyHealth = Mathf.Max(enemyHealth - playerDamage, 0);
            // Debug.Log(enemyHealth);
            if (enemyHealth == 0)
            {
                Die();
            }
        }

        void Die()
        {
            if (isDead)
            {
                Debug.Log("enemy is already dead");
                return;
            }
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            
            // Destroy(gameObject);

            if (gameObject.tag == "Player")
            {
                fighter.enabled = false;
                navMeshAgent.enabled = false;
                playerController.enabled = false;
            }
            else
            {
                GetComponent<AIController>().enabled = false;
                GetComponent<Fighter>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
            }
        }

        public object CaptureState()
        {
            return enemyHealth;
        }

        public void RestoreState(object state)
        {
            enemyHealth = (float)state;
            if (enemyHealth == 0)
            {
                Die();
            }
        }
    }
}
