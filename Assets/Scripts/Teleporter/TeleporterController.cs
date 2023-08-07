using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TeleporterController : MonoBehaviour
{
    [HideInInspector] public Mesh TeleporterMesh;
    [SerializeField] SpawnDespawnBehavior OnSpawnDespawn;
    Vector3 fullSize;
    // Start is called before the first frame update
    void Awake()
    {
        TeleporterMesh = GetComponent<MeshFilter>().mesh;
        OnSpawnDespawn.RunSpawnBehavior(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * 2);
    }
}
