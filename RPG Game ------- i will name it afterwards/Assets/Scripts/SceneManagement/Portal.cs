using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        const string portalSaveFile = "throughPortalSave";

        enum DestinationIdentifier
        {
                A, B, C, D, E
        }
        
        
        [SerializeField] int buildSceneIndex;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
         
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {   
            DontDestroyOnLoad(gameObject);

            Fadar fadar = FindObjectOfType<Fadar>();
            yield return fadar.FadeOut(1f);

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            // save current scene
            savingWrapper.Save();
            // changin' the scene
            yield return SceneManager.LoadSceneAsync(buildSceneIndex);
            // load current scene
            savingWrapper.Load();
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            savingWrapper.Save();
            yield return new WaitForSeconds(.5f);
            yield return fadar.FadeIn(1.5f);

            Destroy(gameObject);
        }

        Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }

            return null;
        }

        void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;

        }
    }
}