using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementComponent : SimulationBehaviour
{
    CharacterController characterContoller;
    [SerializeField] float movementSpeed;

    private void Awake()
    {
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
