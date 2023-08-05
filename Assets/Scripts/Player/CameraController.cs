using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public Transform Follow;
    [SerializeField] float cameraMovementSpeed;
    [SerializeField] float cameraDirectionDragTime;
    [SerializeField] float zoomMovementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Bounds validMovementSpace;
    ControlScheme activeControlScheme;
    Vector2 movementDirection;
    Vector3 movementPostion;
    bool swiveling;

    private void Awake()
    {
        activeControlScheme = new ControlScheme();
        activeControlScheme.CameraMovement.Enable();
        activeControlScheme.CameraMovement.Movement.performed += CameraMoved;
        activeControlScheme.CameraMovement.Movement.canceled += CameraMoved;
        activeControlScheme.CameraMovement.Zoom.performed += Zoom;
        activeControlScheme.CameraMovement.SwivelMode.performed += SetSwivelState;
        activeControlScheme.CameraMovement.SwivelMode.canceled += SetSwivelState;
        activeControlScheme.CameraMovement.SwivelCamera.performed += SwivelCamera;
    }

    private void Update()
    {
        movementPostion = transform.position + Quaternion.Euler(0,transform.rotation.eulerAngles.y,0) * new Vector3(movementDirection.x * Time.deltaTime, 0, movementDirection.y * Time.deltaTime) * cameraMovementSpeed;

        if (movementPostion.x < validMovementSpace.extents.x && movementPostion.x > -validMovementSpace.extents.x)
        {
            transform.position = new Vector3(movementPostion.x, transform.position.y, transform.position.z);
        }
        if(movementPostion.z < validMovementSpace.extents.z && movementPostion.z > -validMovementSpace.extents.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, movementPostion.z);
        }
    }

    void CameraMoved(InputAction.CallbackContext context)
    {
        DOTween.To(() => movementDirection, x => movementDirection = x, context.ReadValue<Vector2>(), cameraDirectionDragTime); 
    }

    void Zoom(InputAction.CallbackContext context)
    {
        int zoomInOut = context.ReadValue<float>() > 0 ? 1 : -1;
        Vector3 zoomDirection = transform.forward * zoomInOut * zoomMovementSpeed;
        transform.position += zoomDirection;
    }

    void SetSwivelState(InputAction.CallbackContext context)
    {
        swiveling = context.ReadValue<float>() > 0 ? true : false;
    }
    void SwivelCamera(InputAction.CallbackContext context)
    {
        if (!swiveling) return;
        RaycastHit hit;
        Vector2 direction = context.ReadValue<Vector2>();
        Physics.Raycast(transform.position, transform.forward, out hit, 100f);
        Debug.DrawLine(transform.position, hit.point);

        transform.RotateAround(hit.point, Vector3.up, rotationSpeed * Time.deltaTime * direction.x);
        transform.RotateAround(hit.point, Vector3.right, rotationSpeed * Time.deltaTime * direction.y * -1);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}
