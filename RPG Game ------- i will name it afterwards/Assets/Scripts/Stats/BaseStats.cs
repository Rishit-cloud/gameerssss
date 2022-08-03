using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        void Update()
        {
            if (gameObject.tag == "Player")
            {
                Debug.Log(GetLevel());
            }
        }


        public float GetStat(Stat stat)
        {
            return progression.GetStat(characterClass, stat, GetLevel());
        }

        public int GetLevel()
        {
            if (GetComponent<Experience>() == null) return startingLevel;
            
            float currentXP = GetComponent<Experience>().GetExperience();
            int penultimateLevel = progression.GetLevels( characterClass, Stat.ExperienceToLevelUp);
            
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(characterClass, Stat.ExperienceToLevelUp, level);
                if (currentXP <= XPToLevelUp)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }
    }
}
