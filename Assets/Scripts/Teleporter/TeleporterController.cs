using System.Collections;
using System.Collections.Generic;
using Fusion;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TeleporterController : NetworkBehaviour, ISpawned
{
    [HideInInspector] public TeleporterController Partner;
    public MeshFilter TeleporterMesh;
    public Transform SpawnPoint;

    [Networked(OnChanged = nameof(OnInitialized))] bool initialized { get; set;}
    [Networked(OnChanged = nameof(OnAwake))] bool awake { get; set; }
    [SerializeField] SpawnDespawnBehavior OnSpawnDespawn;
    [SerializeField] float timeBeforeDespawning;
    [SerializeField] TeleporterSoundAsset soundAsset;
    AudioSource audioSource;
    PlayerController teleportingObject;
    float timer;

    public override void Spawned()
    {
        awake = true;
    }

    public void Initialize()
    {
        initialized = true;
    }
    void InitializeNetworked()
    {
        gameObject.SetActive(true);
        OnSpawnDespawn.RunSpawnBehavior(transform);
        StartCoroutine(AwaitDeath());
    }
    void AwakeNetworked()
    {
        audioSource = GetComponent<AudioSource>();
        if (initialized) return;
        gameObject.SetActive(false);
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
        if(PlayerManager.Runner == Runner) PlayerManager.TeleporterSpawner.TeleporterDeleted();
        if (gameObject == null) return;
        Destroy(gameObject);
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
    static void OnAwake(Changed<TeleporterController> changed)
    {
        changed.Behaviour.AwakeNetworked();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * 2);
    }
    private void OnTriggerEnter(Collider other)
    {
        teleportingObject = other.GetComponent<PlayerController>();
        if (teleportingObject == null) return;
        teleportingObject.Despawn(TeleportObject);
    }

}
