using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner1 : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab;
        
        void Start()
        {
            Spawn(11);
        }

        public void Spawn(float damageAmount)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
        }
    }

}