using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;
using RPG.Core;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        void DisableControl(PlayableDirector playableDirector)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        void EnableControl(PlayableDirector playableDirector)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }   
}
