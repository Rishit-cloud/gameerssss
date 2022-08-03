using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceUI : MonoBehaviour
{
    Experience experience;
    
    void Start()
    {
        experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
    }

    void Update()
    {
        string XPDisplay = string.Format("{0:0}", experience.GetExperience());
        GetComponent<Text>().text = XPDisplay;
    }
}

}