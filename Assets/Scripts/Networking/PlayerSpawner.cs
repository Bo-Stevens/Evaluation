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
            playerInstance.GetBehaviour<PlayerMovementComponent>().CameraController = cameraInstance;
            cameraInstance.Follow = playerInstance.transform;
            cameraInstance.Player = playerInstance.GetComponent<PlayerMovementComponent>();
            playerInstance.GetComponent<PlayerMovementComponent>().PlayerUI = ui;
        }
    }
}
