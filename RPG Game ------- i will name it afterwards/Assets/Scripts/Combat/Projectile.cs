using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] Transform target = null;

    void Update()
    {
        if (target == null) return;
        transform.LookAt(target);
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }
}
