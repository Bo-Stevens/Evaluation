using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class TeleporterSpawningComponent : MonoBehaviour
{
    public int MaxCubeAmount;
    [SerializeField] TeleporterPointerController pointerPrefab;
    [SerializeField] TeleporterController teleporterCubePrefab;

    float pointerYOffset;
    Vector3 spawnPosition;
    Vector3 spawnNormal;
    Camera playerCam;
    int cubeCount;
    bool isValid;
    // Start is called before the first frame update
    void Start()
    {
        pointerPrefab = Instantiate(pointerPrefab.gameObject).GetComponent<TeleporterPointerController>();
        playerCam = GetComponent<Camera>();
        //Should come up with something more intuitive
        PlayerResources.Instance.Player.ActiveControlScheme.CameraMovement.MouseMoved.performed += OnMouseMoved;
        PlayerResources.Instance.Player.ActiveControlScheme.TeleporterPlacement.LeftMouseClicked.performed += OnMouseLeftClick;
        pointerYOffset = pointerPrefab.GetComponent<MeshFilter>().mesh.bounds.extents.y;
    }

    void OnMouseMoved(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = context.ReadValue<Vector2>();
        Ray rayToCast = playerCam.ScreenPointToRay(new Vector3(screenPosition.x, screenPosition.y));
        RaycastHit hit;
        Physics.Raycast(rayToCast, out hit);
        spawnPosition = hit.point;
        spawnNormal = hit.normal;
        isValid = Vector3.Dot(hit.normal, Vector3.up) > 0.5f;
        pointerPrefab.SetValidStatus(isValid);
        pointerPrefab.transform.position = spawnPosition + new Vector3(0, pointerYOffset, 0);

        //Only change the rotation if you're going to create a valid teleporter
        if (!isValid) { pointerPrefab.transform.rotation = Quaternion.identity; return; }
        Vector3 newForwards = Vector3.Cross(pointerPrefab.transform.right, spawnNormal);
        Quaternion rotation = Quaternion.LookRotation(newForwards);
        pointerPrefab.transform.rotation = rotation;
    }

    void OnMouseLeftClick(InputAction.CallbackContext context)
    {
        if (cubeCount >= MaxCubeAmount || EventSystem.current.IsPointerOverGameObject() || !pointerPrefab.IsValidLocation()) return;
        SpawnTeleporter(spawnPosition);
    }
    public void SpawnTeleporter(Vector3 position)
    {
        TeleporterController teleporter = Instantiate(teleporterCubePrefab.gameObject, position, Quaternion.identity).GetComponent<TeleporterController>();
        Vector3 newForwards = Vector3.Cross(teleporter.transform.right, spawnNormal);
        Quaternion rotation = Quaternion.LookRotation(newForwards);
        teleporter.transform.rotation = rotation;
        teleporter.transform.position += new Vector3(0, teleporter.TeleporterMesh.bounds.size.y / 2f, 0);
        cubeCount += 1;
        PlayerResources.Instance.UIController.SetPlacementCountString(cubeCount + "");
    }
    public void TeleporterDeleted()
    {
        cubeCount -= 1;
        PlayerResources.Instance.UIController.SetPlacementCountString(cubeCount + "");
    }
}
