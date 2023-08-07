using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TeleporterSpawningComponent : MonoBehaviour
{
    [SerializeField] GameObject pointerPrefab;
    [SerializeField] TeleporterController teleporterCubePrefab;
    [SerializeField] int maxCubeAmount;

    public delegate void CubeSpawned(int cubeCount);
    public event CubeSpawned OnCubeSpawned;

    Vector3 spawnPosition;
    Camera playerCam;
    int cubeCount;


    // Start is called before the first frame update
    void Start()
    {
        pointerPrefab = Instantiate(pointerPrefab);
        playerCam = GetComponent<Camera>();
        //Should come up with something more intuitive
        GetComponent<CameraController>().Player.ActiveControlScheme.CameraMovement.MouseMoved.performed += OnMouseMoved;
        //GetComponent<CameraController>().Player.ActiveControlScheme.CameraMovement.LeftMouseClicked.performed += OnMouseLeftClick;
    }

    void OnMouseMoved(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = context.ReadValue<Vector2>();
        Ray rayToCast = playerCam.ScreenPointToRay(new Vector3(screenPosition.x, screenPosition.y));
        RaycastHit hit;
        Physics.Raycast(rayToCast, out hit);
        spawnPosition = hit.point;
        pointerPrefab.transform.position = spawnPosition;
    }

    void OnMouseLeftClick(InputAction.CallbackContext context)
    {
        if (cubeCount >= maxCubeAmount) return; 
        TeleporterController teleporter = Instantiate(teleporterCubePrefab.gameObject, spawnPosition, Quaternion.identity).GetComponent<TeleporterController>();
        teleporter.transform.position += new Vector3(0, teleporter.TeleporterMesh.bounds.size.y / 2f, 0);
        cubeCount += 1;
        if(OnCubeSpawned != null) OnCubeSpawned.Invoke(cubeCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
