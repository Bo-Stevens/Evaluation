using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterDummy : MonoBehaviour
{
    [SerializeField] PointerSoundAsset soundAsset;
    private void Awake()
    {
        PlayerManager.PlayerEarsAudioSource.PlayOneShot(soundAsset.OnSpawn);
    }
}
