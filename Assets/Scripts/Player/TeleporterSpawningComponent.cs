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
    Camera playerCam;
    int cubeCount;

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
        pointerPrefab.transform.position = spawnPosition + new Vector3(0, pointerYOffset, 0);
        pointerPrefab.SetValidStatus(Vector3.Dot(hit.normal, Vector3.up) > 0.5f);
    }

    void OnMouseLeftClick(InputAction.CallbackContext context)
    {
        if (cubeCount >= MaxCubeAmount || EventSystem.current.IsPointerOverGameObject() || !pointerPrefab.IsValidLocation()) return;
        SpawnTeleporter(spawnPosition);
    }
    public void SpawnTeleporter(Vector3 position)
    {
        TeleporterController teleporter = Instantiate(teleporterCubePrefab.gameObject, position, Quaternion.identity).GetComponent<TeleporterController>();
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
