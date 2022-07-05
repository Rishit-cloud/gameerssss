using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class TriggerCinematics : MonoBehaviour
    {
        bool isTriggered = false;
        void OnTriggerEnter(Collider collider)
        {
            if (!isTriggered && collider.gameObject.tag == "Player") 
            {
                isTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}
