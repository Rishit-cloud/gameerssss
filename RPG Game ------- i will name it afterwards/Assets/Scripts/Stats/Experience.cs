using UnityEngine;
using RPG.Saving;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experienceReward = 0;

        public void GainExperience(float experience)
        {
            experienceReward += experience;
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