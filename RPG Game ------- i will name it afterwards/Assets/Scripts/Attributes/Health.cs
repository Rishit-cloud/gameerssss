using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Combat;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        
        
        AIController aiController;
        Fighter fighter;
        PlayerController playerController;
        NavMeshAgent navMeshAgent;
        Animator animator;
        float enemyHealth = 100f;
        public bool isDead = false;
        bool isRestored = false;

        void Start()
        {
            aiController = GetComponent<AIController>();
            animator = GetComponent<Animator>();
            fighter = GetComponent<Fighter>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            playerController = GetComponent<PlayerController>();

            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            
            if (isRestored != true)
            {
                enemyHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }
        
        public void TakeDamage( GameObject instigator, float damage)
        {
            Debug.Log(gameObject.name + " took damage: " + damage);
            
            enemyHealth = Mathf.Max(enemyHealth - damage, 0);
            if (enemyHealth == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return enemyHealth;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));

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

        public float GetHealthPercentage()
        {
            return 100 * (enemyHealth / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        void RegenerateHealth()
        {
            float regenHealth = regenerationPercentage/100 * GetComponent<BaseStats>().GetStat(Stat.Health);
            enemyHealth = Mathf.Max(enemyHealth, regenHealth);
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
                isRestored = true;
            }
        }
    }
}
