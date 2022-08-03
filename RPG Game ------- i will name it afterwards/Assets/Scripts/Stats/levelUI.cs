using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class levelUI : MonoBehaviour
    {
        BaseStats baseStats;

        void Start()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        void Update()
        {
            string levelDisplay = string.Format("{0:0}", baseStats.GetLevel());
            GetComponent<Text>().text = levelDisplay;
        }
    }
}