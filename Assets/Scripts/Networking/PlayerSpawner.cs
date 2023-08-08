using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] CameraController playerCameraPrefab;
    [SerializeField] PlayerUIController playerUI;

    public void PlayerJoined(PlayerRef player)
    {
        List<PlayerRef> players = Runner.ActivePlayers.ToList();
        if (player == Runner.LocalPlayer)
        {
            NetworkObject playerInstance = Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, player);
            PlayerController controller = playerInstance.GetComponent<PlayerController>();
            controller.Initialize();
            CameraController cameraInstance = Instantiate(playerCameraPrefab.gameObject).GetComponent<CameraController>();
            PlayerUIController ui = Instantiate(playerUI.gameObject).GetComponent<PlayerUIController>();
            PlayerCountUI countUi = Instantiate(ui.PlayerCount, ui.PlayerCount.transform.position, Quaternion.identity);
            ui.PlayerCount = countUi;
            countUi.gameObject.transform.SetParent(ui.gameObject.transform);
            countUi.PlayerCount = players.Count;
            countUi.SetPlayerText();

            PlayerManager.Player = controller;
            PlayerManager.CamController = cameraInstance;
            PlayerManager.UIController = ui;
            PlayerManager.TeleporterSpawner = cameraInstance.GetComponent<TeleporterSpawningComponent>();
            PlayerManager.Runner = Runner;
            PlayerManager.PlayerReference = player;

            playerInstance.transform.position += new Vector3(0, controller.VisualComponent.GetComponent<MeshFilter>().mesh.bounds.extents.y, 0);
            cameraInstance.Follow = playerInstance.transform;
        }
        else
        {
            PlayerManager.UIController.PlayerCount.PlayerJoined();
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        PlayerManager.UIController.PlayerCount.PlayerLeft();
    }
}
