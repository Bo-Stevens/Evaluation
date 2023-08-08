using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(AudioSource))]
public class TeleporterPointerController : MonoBehaviour
{
    [SerializeField] Color validColor;
    [SerializeField] Color invalidColor;
    [SerializeField] PointerSoundAsset soundAsset;
    Material instanceMaterial;
    bool isValidLocation;
    private void Awake()
    {
        instanceMaterial = GetComponent<MeshRenderer>().material;
        PlayerManager.PlayerEarsAudioSource.PlayOneShot(soundAsset.OnSpawn);
    }
    public void SetValidStatus(bool valid)
    {
        isValidLocation = valid;

        if (isValidLocation) instanceMaterial.color = validColor;
        else instanceMaterial.color = invalidColor;
    }

    public bool IsValidLocation()
    {
        return isValidLocation;
    }
}
