using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        [SerializeField] float fadeInTime = .2f;
        
        IEnumerator Start()
        {
            Fadar fadar = FindObjectOfType<Fadar>();
            fadar.FadeOutImmidiate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fadar.FadeIn(fadeInTime);
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}
