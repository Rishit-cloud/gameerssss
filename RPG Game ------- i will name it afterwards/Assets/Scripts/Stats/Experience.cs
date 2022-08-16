using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experienceReward = 0;
        // public delegate void ExperienceGainedDelegate();
        public event Action onExperienceGained;

        public void GainExperience(float experience)
        {
            experienceReward += experience;
            onExperienceGained();
        }

        public float GetExperience()
        {
            return experienceReward;
        }

        public object CaptureState()
        {
            return experienceReward;
        }

        public void RestoreState(object state)
        {
            experienceReward = (float)state;
        }
    }
}