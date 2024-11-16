using System.Collections.Generic;
using UnityEngine;

public enum TerrainType {
    Foliage,
    Grass,
    Grates,
    Metal,
    Pebbles,
    Stone,
    Wood
}

public class PlayerFootsteps : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip foliage1, foliage2, foliage3, foliage4;
    public AudioClip grass1, grass2, grass3, grass4;
    public AudioClip grates1, grates2, grates3, grates4;
    public AudioClip metal1, metal2, metal3, metal4;
    public AudioClip pebbles1, pebbles2, pebbles3, pebbles4;
    public AudioClip stone1, stone2, stone3, stone4;
    public AudioClip wood1, wood2, wood3, wood4;
    private Dictionary<TerrainType, AudioClip[]> allSounds;
    
    void Start()
    {
        allSounds = new Dictionary<TerrainType, AudioClip[]>();
        allSounds[TerrainType.Foliage] = new AudioClip[] {foliage1, foliage2, foliage3, foliage4};
        allSounds[TerrainType.Grass] = new AudioClip[] {grass1, grass2, grass3, grass4};
        allSounds[TerrainType.Grates] = new AudioClip[] {grates1, grates2, grates3, grates4};
        allSounds[TerrainType.Metal] = new AudioClip[] {metal1, metal2, metal3, metal4};
        allSounds[TerrainType.Pebbles] = new AudioClip[] {pebbles1, pebbles2, pebbles3, pebbles4};
        allSounds[TerrainType.Stone] = new AudioClip[] {stone1, stone2, stone3, stone4};
        allSounds[TerrainType.Wood] = new AudioClip[] {wood1, wood2, wood3, wood4};
    }

    public void PlayFootstepSound(TerrainType terrain)
    {
        AudioClip[] terrainSounds = allSounds[terrain];
        audioSource.pitch = Random.Range(0.75f, 1.25f);
        audioSource.PlayOneShot(terrainSounds[Random.Range(0, terrainSounds.Length)]);
    }

    void Update()
    {
        
    }
}
