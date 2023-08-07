using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] CameraController playerCameraPrefab;
    [SerializeField] PlayerUIController playerUI;
    public void PlayerJoined(PlayerRef player)
    {
        if(player == Runner.LocalPlayer)
        {
            NetworkObject playerInstance = Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
            CameraController cameraInstance = Instantiate(playerCameraPrefab.gameObject).GetComponent<CameraController>();
            PlayerUIController ui = Instantiate(playerUI.gameObject).GetComponent<PlayerUIController>();
            PlayerController controller = playerInstance.GetComponent<PlayerController>();
            playerInstance.transform.position += new Vector3(0, controller.VisualComponent.GetComponent<MeshFilter>().mesh.bounds.extents.y, 0);

            playerInstance.GetBehaviour<PlayerController>().CameraController = cameraInstance;
            cameraInstance.Follow = playerInstance.transform;
            cameraInstance.Player = playerInstance.GetComponent<PlayerController>();
            playerInstance.GetComponent<PlayerController>().PlayerUI = ui;
        }
    }
}
