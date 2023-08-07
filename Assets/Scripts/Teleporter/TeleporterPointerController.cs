using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TeleporterPointerController : MonoBehaviour
{
    [SerializeField] Color validColor;
    [SerializeField] Color invalidColor;
    Material instanceMaterial;
    bool isValidLocation;
    private void Awake()
    {
        instanceMaterial = GetComponent<MeshRenderer>().material;
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
