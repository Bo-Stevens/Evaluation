using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementComponent : SimulationBehaviour
{
    [HideInInspector] public ControlScheme ActiveControlScheme;
    CharacterController characterContoller;
    [SerializeField] float movementSpeed;


    private void Awake()
    {
        ActiveControlScheme = new ControlScheme();
        ActiveControlScheme.CameraMovement.Enable();
        characterContoller = GetComponent<CharacterController>();
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

}
