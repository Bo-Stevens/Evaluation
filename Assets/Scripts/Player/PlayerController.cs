using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : SimulationBehaviour
{
    [HideInInspector] public ControlScheme ActiveControlScheme;
    [HideInInspector] public NavMeshAgent NavAgent;
    public GameObject VisualComponent;

    [SerializeField] RotateScaleSpawnDespawn spawnDespawnBehavior;
    CharacterController characterContoller;

    private void Awake()
    {
        ActiveControlScheme = new ControlScheme();
        ActiveControlScheme.CameraMovement.Enable();
        ActiveControlScheme.UnitOrdering.Enable();
        characterContoller = GetComponent<CharacterController>();
        NavAgent = GetComponent<NavMeshAgent>();
        spawnDespawnBehavior.RunSpawnBehavior(transform);
    }
    private void Start()
    {
        PlayerManager.CamController.Follow = transform;
    }

    public void MoveTo(Vector3 pos)
    {
        NavAgent.SetDestination(pos);
    }

    public void FlipMouseControlSceme()
    {
        if (ActiveControlScheme.UnitOrdering.enabled)
        {
            ActiveControlScheme.UnitOrdering.Disable();
            PlayerManager.TeleporterSpawner.Activate();
        }
        else
        {
            ActiveControlScheme.UnitOrdering.Enable();
            PlayerManager.TeleporterSpawner.Deactivate();
        }
    }

    public void Despawn(DG.Tweening.TweenCallback onComplete)
    {
        spawnDespawnBehavior.RunDespawnBehavior(transform, onComplete);
    }
    public void Spawn()
    {
        spawnDespawnBehavior.RunSpawnBehavior(transform);
    }
}
