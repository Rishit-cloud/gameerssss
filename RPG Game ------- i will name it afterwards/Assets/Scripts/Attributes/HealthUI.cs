using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;
using UnityEngine.UI;
using System;

namespace RPG.Attributes
{
    public class HealthUI : MonoBehaviour
    {
        Health health;
        
        void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            
        }

        void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}%", health.GetHealthPercentage());
        }
    }
}
