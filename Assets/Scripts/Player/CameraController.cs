using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
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

    private void Awake()
    {
        PlayerManager.PlayerEarsAudioSource = GetComponent<AudioSource>();    
    }
    private void Start()
    {
        ControlScheme activeControlScheme = PlayerManager.Player.ActiveControlScheme;
        centerPosition = new Vector3(transform.position.x / 2f, transform.position.y, transform.position.z / 2f);
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
        //Updating the direction in which the camera will respond to player input

        movementPostion = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0) * new Vector3(movementDirection.x * Time.deltaTime, 0, movementDirection.y * Time.deltaTime) * cameraMovementSpeed;
        transform.position += movementPostion;
        centerPosition += movementPostion;
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

        Vector2 direction = -context.ReadValue<Vector2>();
        rotation += direction / 100f;
        float PiHalf = Mathf.PI / 2f;
        Vector3 circle = (new Vector3(Mathf.Cos(rotation.x * rotationSpeed + PiHalf), 0, Mathf.Sin(rotation.x * rotationSpeed - PiHalf)) * cameraRotationRadius ) + centerPosition;
        transform.position = circle;
        transform.LookAt(new Vector3(centerPosition.x, transform.position.y, centerPosition.z));
        transform.rotation = Quaternion.Euler(new Vector3(45, transform.eulerAngles.y, transform.eulerAngles.z));
    }

    void OnLeftClick(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        Vector2 screenPosition = Input.mousePosition;
        Ray rayToCast = GetComponent<Camera>().ScreenPointToRay(new Vector3(screenPosition.x, screenPosition.y));
        RaycastHit hit;
        Physics.Raycast(rayToCast, out hit);
        PlayerManager.Player.MoveTo(hit.point);
    }

}
