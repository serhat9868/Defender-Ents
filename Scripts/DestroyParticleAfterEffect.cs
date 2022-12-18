using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleAfterEffect : MonoBehaviour
{
    float lifetime;
    float time;

    [System.Obsolete]
    void Start()
    {
        lifetime = GetComponent<ParticleSystem>().startLifetime;
    }

    void Update()
    {
        if (time > lifetime)
        {
            Destroy(gameObject);
        }

        time += Time.deltaTime;
    }
}
