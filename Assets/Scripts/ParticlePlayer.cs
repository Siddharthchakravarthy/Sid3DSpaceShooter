using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public void PlayParticles() {
        particles.Play();
    }
}
