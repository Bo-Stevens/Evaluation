using System.Collections;
using System.Collections.Generic;
using Fusion;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TeleporterController : NetworkBehaviour, ISpawned
{
    [HideInInspector] public TeleporterController Partner;
    [HideInInspector] public bool Owned;
    public MeshFilter TeleporterMesh;
    public Transform SpawnPoint;

    [Networked(OnChanged = nameof(OnInitialized))] public bool active { get; set;}
    [Networked(OnChanged = nameof(OnInitialized))] public bool destroy { get; set; }
    [SerializeField] SpawnDespawnBehavior OnSpawnDespawn;
    [SerializeField] float timeBeforeDespawning;
    [SerializeField] TeleporterSoundAsset soundAsset;
    AudioSource audioSource;
    PlayerController teleportingObject;
    float timer;

    private void Awake()
    {
        TeleporterMesh.gameObject.SetActive(false);
    }
    void InitializeNetworked()
    {
        audioSource = GetComponent<AudioSource>();
        OnSpawnDespawn.RunSpawnBehavior(transform);
        TeleporterMesh.gameObject.SetActive(true);
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
        if (Owned)
        {
            PlayerManager.TeleporterSpawner.TeleporterDeleted();
            Destroy(gameObject);
        }
    }
    void TeleportObject()
    {
        teleportingObject.Spawn();
        audioSource.PlayOneShot(soundAsset.OnTeleport);
        teleportingObject.transform.position = Partner.SpawnPoint.position;
        teleportingObject.transform.forward = Partner.gameObject.transform.forward;
        teleportingObject.NavAgent.ResetPath();
    }
    static void OnInitialized(Changed<TeleporterController> changed)
    {
        changed.Behaviour.InitializeNetworked();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * 2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!active) return;
        teleportingObject = other.GetComponent<PlayerController>();
        if (teleportingObject == null) return;
        teleportingObject.Despawn(TeleportObject);
    }
}
