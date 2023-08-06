using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerCameraPrefab;
    public void PlayerJoined(PlayerRef player)
    {
        if(player == Runner.LocalPlayer)
        {
            NetworkObject playerInstance = Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
            GameObject cameraInstance = Instantiate(playerCameraPrefab);
            cameraInstance.GetComponent<CameraController>().Follow = playerInstance.transform;
            cameraInstance.GetComponent<CameraController>().Player = playerInstance.GetComponent<PlayerMovementComponent>();
        }
    }
}
