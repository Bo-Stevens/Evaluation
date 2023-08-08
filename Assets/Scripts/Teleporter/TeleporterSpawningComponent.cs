using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Fusion;
using UnityEngine.EventSystems;

class TeleporterPair
{
    public GameObject teleporterParent;
    public NetworkObject One;
    public NetworkObject Two;
    public GameObject PointerOne;
    public GameObject PointerTwo;
}

public class TeleporterSpawningComponent : MonoBehaviour
{
    public int MaxCubeAmount;
    [SerializeField] TeleporterPointerController pointerPrefab;
    [SerializeField] TeleporterDummy teleporterDummy;
    [SerializeField] TeleporterController teleporterCubePrefab;
    TeleporterPair pairToBuild;
    Vector3 spawnPosition;
    Vector3 spawnNormal;
    Camera playerCam;
    int cubeCount;
    bool isValid;
    float pointerYOffset;

    // Start is called before the first frame update
    void Start()
    {
        pointerPrefab = Instantiate(pointerPrefab.gameObject).GetComponent<TeleporterPointerController>();
        playerCam = GetComponent<Camera>();
        //Should come up with something more intuitive
        PlayerManager.Player.ActiveControlScheme.CameraMovement.MouseMoved.performed += OnMouseMoved;
        PlayerManager.Player.ActiveControlScheme.TeleporterPlacement.LeftMouseClicked.performed += OnMouseLeftClick;
        PlayerManager.Player.ActiveControlScheme.TeleporterPlacement.Escape.performed += OnEscape;
        pointerYOffset = pointerPrefab.GetComponent<MeshFilter>().mesh.bounds.extents.y;
        Deactivate();
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

    void OnEscape(InputAction.CallbackContext context)
    {
        Deactivate();
    }

    void OnMouseLeftClick(InputAction.CallbackContext context)
    {
        if (cubeCount >= MaxCubeAmount || EventSystem.current.IsPointerOverGameObject() || !pointerPrefab.IsValidLocation()) return;
        SpawnTeleporter(spawnPosition);
    }

    public void SpawnTeleporter(Vector3 position)
    {
        if(pairToBuild == null)
        {
            pairToBuild = new TeleporterPair();
            pairToBuild.One = InstantiateTeleporter(position);
            pairToBuild.PointerOne = Instantiate(teleporterDummy.gameObject, position, Quaternion.identity);
            pairToBuild.PointerOne.transform.position += new Vector3(0, pointerYOffset, 0);
        }
        else
        {
            pairToBuild.Two = InstantiateTeleporter(position);
            pairToBuild.One.GetComponent<TeleporterController>().active = true;
            pairToBuild.Two.GetComponent<TeleporterController>().active = true;
            pairToBuild.One.GetComponent<TeleporterController>().Owned = true;
            pairToBuild.Two.GetComponent<TeleporterController>().Owned = true;
            pairToBuild.One.GetComponent<TeleporterController>().Partner = pairToBuild.Two.GetComponent<TeleporterController>();
            pairToBuild.Two.GetComponent<TeleporterController>().Partner = pairToBuild.One.GetComponent<TeleporterController>();
            pairToBuild.PointerTwo = Instantiate(teleporterDummy.gameObject, position, Quaternion.identity);
            pairToBuild.PointerTwo.transform.position += new Vector3(0, pointerYOffset, 0);
            Destroy(pairToBuild.PointerOne);
            Destroy(pairToBuild.PointerTwo);
            pairToBuild = null;
        }
    }

    NetworkObject InstantiateTeleporter(Vector3 position)
    {
        NetworkObject teleporter = PlayerManager.Runner.Spawn(teleporterCubePrefab.gameObject);
        teleporter.transform.position = position;
        Vector3 newForwards = Vector3.Cross(teleporter.transform.right, spawnNormal);
        Quaternion rotation = Quaternion.LookRotation(newForwards);
        teleporter.transform.rotation = rotation;
        teleporter.transform.position += new Vector3(0, teleporter.GetComponent<TeleporterController>().TeleporterMesh.mesh.bounds.size.y / 2f, 0);
        cubeCount += 1;
        PlayerManager.UIController.SetPlacementCountString(cubeCount + "");
        return teleporter;
    }

    public void TeleporterDeleted()
    {
        cubeCount -= 1;
        PlayerManager.UIController.SetPlacementCountString(cubeCount + "");
    }

    public void Activate()
    {
        pointerPrefab.gameObject.SetActive(true);
        PlayerManager.Player.ActiveControlScheme.TeleporterPlacement.Enable();
    }
    public void Deactivate()
    {
        if(pairToBuild != null)
        {
            if (pairToBuild.One != null) { PlayerManager.Runner.Despawn(pairToBuild.One); Destroy(pairToBuild.PointerOne); PlayerManager.TeleporterSpawner.TeleporterDeleted(); }
            if (pairToBuild.Two != null) { PlayerManager.Runner.Despawn(pairToBuild.Two); Destroy(pairToBuild.PointerTwo); PlayerManager.TeleporterSpawner.TeleporterDeleted(); }
            pairToBuild = null;
        }
        pointerPrefab.gameObject.SetActive(false);
        PlayerManager.Player.ActiveControlScheme.UnitOrdering.Enable();
        PlayerManager.Player.ActiveControlScheme.TeleporterPlacement.Disable();
    }
}
