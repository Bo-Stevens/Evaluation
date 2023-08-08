using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound/CharacterSoundAsset")]
public class CharacterSoundAsset : ScriptableObject
{
    public AudioClip OnSpawn;
    public AudioClip OnMove;
}
