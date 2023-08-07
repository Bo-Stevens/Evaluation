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
    [HideInInspector] public PlayerUIController PlayerUI;
    [HideInInspector] public CameraController CameraController;
    [HideInInspector] public TeleporterSpawningComponent CubeSpawner;
    public GameObject VisualComponent;

    [SerializeField] RotateScaleSpawnDespawn spawnDespawnBehavior;
    CharacterController characterContoller;
    NavMeshAgent agent;

    private void Awake()
    {
        ActiveControlScheme = new ControlScheme();
        ActiveControlScheme.CameraMovement.Enable();
        characterContoller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        spawnDespawnBehavior.RunSpawnBehavior(transform);
    }
    private void Start()
    {
        CameraController.Follow = transform;
        CameraController.Player = this;
        CubeSpawner = CameraController.gameObject.GetComponent<TeleporterSpawningComponent>();
        CubeSpawner.OnCubeSpawned += OnCubeSpawned;
    }
    public override void FixedUpdateNetwork()
    {
/*        if (!HasStateAuthority) return;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * agent.speed;
        characterContoller.Move(move);
        if(move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }*/
    }

    public void OnCubeSpawned(int cubeCount)
    {
        PlayerUI.SetPlacementCountString(cubeCount + "");
    }
    public void MoveTo(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
}
