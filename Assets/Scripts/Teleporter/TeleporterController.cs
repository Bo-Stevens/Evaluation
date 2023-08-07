using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TeleporterController : MonoBehaviour
{
    [HideInInspector] public Mesh TeleporterMesh;
    [HideInInspector] public TeleporterController Partner;
    public Transform SpawnPoint;

    [SerializeField] SpawnDespawnBehavior OnSpawnDespawn;
    [SerializeField] float timeBeforeDespawning;
    float timer;

    PlayerController teleportingObject;

    void Awake()
    {
        TeleporterMesh = GetComponent<MeshFilter>().mesh;
    }

    public void Initialize(TeleporterController partner)
    {
        Partner = partner;
        gameObject.SetActive(true);
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
        PlayerManager.TeleporterSpawner.TeleporterDeleted();
        //Scary code. Replace or treat with care
        if (transform.parent != null) Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;
        player.Despawn(TeleportObject);

    }

    void TeleportObject()
    {
        teleportingObject.Spawn();
        teleportingObject.transform.position = Partner.SpawnPoint.position;
        teleportingObject.transform.forward = Partner.gameObject.transform.forward;
        teleportingObject.NavAgent.destination = teleportingObject.transform.position;
    }
}
