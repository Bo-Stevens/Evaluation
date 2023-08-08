using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound/TeleporterSoundAsset")]
public class TeleporterSoundAsset : ScriptableObject
{
    public AudioClip OnDestroy;
    public AudioClip OnTeleport;
}
