using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [HideInInspector] public Transform Follow;
    [SerializeField] float cameraMovementSpeed;
    [SerializeField] float cameraDirectionDragTime;
    [SerializeField] float zoomMovementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float cameraRotationRadius;
    [SerializeField] Bounds validMovementSpace;
    Vector2 movementDirection;
    Vector2 rotation;
    Vector3 movementPostion;
    Vector3 centerPosition;
    bool swiveling;

    private void Start()
    {
        ControlScheme activeControlScheme = PlayerResources.Instance.Player.ActiveControlScheme;
        activeControlScheme.CameraMovement.Movement.performed += CameraMoved;
        activeControlScheme.CameraMovement.Movement.canceled += CameraMoved;
        activeControlScheme.CameraMovement.Zoom.performed += Zoom;
        activeControlScheme.CameraMovement.SwivelMode.performed += SetSwivelState;
        activeControlScheme.CameraMovement.SwivelMode.canceled += SetSwivelState;
        activeControlScheme.CameraMovement.MovedMouse.performed += SwivelCamera;
        activeControlScheme.UnitOrdering.LeftMouseClicked.performed += OnLeftClick;
    }

    private void Update()
    {
        movementPostion = centerPosition + Quaternion.Euler(0,transform.rotation.eulerAngles.y,0) * new Vector3(movementDirection.x * Time.deltaTime, 0, movementDirection.y * Time.deltaTime) * cameraMovementSpeed;

        if (movementPostion.x < validMovementSpace.extents.x && movementPostion.x > -validMovementSpace.extents.x)
        {
            centerPosition = new Vector3(movementPostion.x, centerPosition.y, centerPosition.z);
        }
        if(movementPostion.z < validMovementSpace.extents.z && movementPostion.z > -validMovementSpace.extents.z)
        {
            centerPosition = new Vector3(centerPosition.x, centerPosition.y, movementPostion.z);
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
        cameraRotationRadius -= (zoomDirection.x + zoomDirection.z);
        transform.position += zoomDirection;
    }

    void SetSwivelState(InputAction.CallbackContext context)
    {
        swiveling = context.ReadValue<float>() > 0 ? true : false;
    }
    void SwivelCamera(InputAction.CallbackContext context)
    {
        if (!swiveling) return;

        Vector2 direction = context.ReadValue<Vector2>();
        rotation += direction / 100f;
        Vector3 circle = (new Vector3(Mathf.Cos(rotation.x), 0, Mathf.Sin(rotation.x)) * cameraRotationRadius) + centerPosition;
        transform.position = circle + new Vector3(0, transform.position.y, 0);
        transform.LookAt(new Vector3(centerPosition.x, transform.position.y, centerPosition.z));
        transform.rotation = Quaternion.Euler(new Vector3(45, transform.eulerAngles.y, transform.eulerAngles.z));
        /*        RaycastHit hit;
                Vector2 direction = context.ReadValue<Vector2>();
                Physics.Raycast(transform.position, transform.forward, out hit, 100f);
                Debug.DrawLine(transform.position, hit.point);

                transform.RotateAround(hit.point, Vector3.up, rotationSpeed * Time.deltaTime * direction.x);
                transform.RotateAround(hit.point, Vector3.right, rotationSpeed * Time.deltaTime * direction.y * -1);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);*/
    }

    void OnLeftClick(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Vector2 screenPosition = Input.mousePosition;
        Ray rayToCast = GetComponent<Camera>().ScreenPointToRay(new Vector3(screenPosition.x, screenPosition.y));
        RaycastHit hit;
        Physics.Raycast(rayToCast, out hit);
        PlayerResources.Instance.Player.MoveTo(hit.point);
    }

}
