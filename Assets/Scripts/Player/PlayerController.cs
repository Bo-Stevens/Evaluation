using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : SimulationBehaviour
{
    [HideInInspector] public ControlScheme ActiveControlScheme;
    public GameObject VisualComponent;

    [SerializeField] RotateScaleSpawnDespawn spawnDespawnBehavior;
    CharacterController characterContoller;
    NavMeshAgent agent;

    private void Awake()
    {
        ActiveControlScheme = new ControlScheme();
        ActiveControlScheme.CameraMovement.Enable();
        ActiveControlScheme.UnitOrdering.Enable();
        characterContoller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        spawnDespawnBehavior.RunSpawnBehavior(transform);
    }
    private void Start()
    {
        PlayerResources.Instance.CamController.Follow = transform;
    }

    public void MoveTo(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
    public void FlipMouseControlSceme()
    {
        if (ActiveControlScheme.UnitOrdering.enabled)
        {
            ActiveControlScheme.UnitOrdering.Disable();
            ActiveControlScheme.TeleporterPlacement.Enable();
        }
        else
        {
            ActiveControlScheme.UnitOrdering.Enable();
            ActiveControlScheme.TeleporterPlacement.Disable();
        }
    }

}
