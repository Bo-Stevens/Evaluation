using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CubeSpawningComponent : MonoBehaviour
{
    [SerializeField] GameObject pointerPrefab;
    Camera playerCam;
    // Start is called before the first frame update
    void Start()
    {
        pointerPrefab = Instantiate(pointerPrefab);
        playerCam = GetComponent<Camera>();
        //Should come up with something more intuitive
        GetComponent<CameraController>().Player.ActiveControlScheme.CameraMovement.MouseMoved.performed += OnMouseMoved;
    }


    void OnMouseMoved(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = context.ReadValue<Vector2>();
        Ray rayToCast = playerCam.ScreenPointToRay(new Vector3(screenPosition.x, screenPosition.y));
        RaycastHit hit;
        Physics.Raycast(rayToCast, out hit);
        pointerPrefab.transform.position = hit.point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
