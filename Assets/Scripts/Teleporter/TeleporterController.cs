using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TeleporterController : MonoBehaviour
{
    [HideInInspector] public Mesh TeleporterMesh;
    [SerializeField] SpawnDespawnBehavior OnSpawnDespawn;
    [SerializeField] float timeBeforeDespawning;
    float timer;
    Vector3 fullSize;
    // Start is called before the first frame update
    void Awake()
    {
        TeleporterMesh = GetComponent<MeshFilter>().mesh;
        OnSpawnDespawn.RunSpawnBehavior(transform);
        StartCoroutine(AwaitDeath());
    }

    IEnumerator AwaitDeath()
    {
        while(timer < timeBeforeDespawning)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        OnSpawnDespawn.RunDespawnBehavior(transform, DeleteGameObject);

    }

    void DeleteGameObject()
    {
        Destroy(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * 2);
    }
}
