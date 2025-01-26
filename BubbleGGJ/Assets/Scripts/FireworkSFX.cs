using System.Collections.Generic;
using UnityEngine;

public class FireworkSFX : MonoBehaviour
{
    private ParticleSystem particleEmitter;
    private int previousNumberOfParticles;
    private int currentNumberOfParticle;
    [SerializeField] AudioSource emitSoundSource;
    [SerializeField] private List<AudioClip> fireworkSounds;

    void Start()
    {
        particleEmitter = gameObject.GetComponent<ParticleSystem>();
        previousNumberOfParticles = 0;
    }

    void Update()
    {
        int currentNumberOfParticles = particleEmitter.particleCount;
        if (currentNumberOfParticles > previousNumberOfParticles)
        {
            emitSoundSource.PlayOneShot(fireworkSounds[Random.Range(0, fireworkSounds.Count)] );
        }
        previousNumberOfParticles = currentNumberOfParticles;
    }
}
