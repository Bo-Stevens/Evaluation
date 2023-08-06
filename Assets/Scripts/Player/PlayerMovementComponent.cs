using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementComponent : SimulationBehaviour
{
    [HideInInspector] public ControlScheme ActiveControlScheme;
    [HideInInspector] public PlayerUIController PlayerUI;
    [HideInInspector] public CameraController CameraController;
    [HideInInspector] public CubeSpawningComponent CubeSpawner;
    [SerializeField] float movementSpeed;
    
    CharacterController characterContoller;
    

    private void Awake()
    {
        ActiveControlScheme = new ControlScheme();
        ActiveControlScheme.CameraMovement.Enable();
        characterContoller = GetComponent<CharacterController>();
    }
    private void Start()
    {
        CameraController.Follow = transform;
        CameraController.Player = this;
        CubeSpawner = CameraController.gameObject.GetComponent<CubeSpawningComponent>();
        CubeSpawner.OnCubeSpawned += OnCubeSpawned;
    }
    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * movementSpeed;
        characterContoller.Move(move);
        if(move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    public void OnCubeSpawned(int cubeCount)
    {
        PlayerUI.SetPlacementCountString(cubeCount + "");
    }

}
