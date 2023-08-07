using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources
{
    public static PlayerResources Instance;
    
    public PlayerController Player;
    public PlayerUIController UIController;
    public TeleporterSpawningComponent TeleporterSpawner;
    public CameraController CamController;

    public PlayerResources()
    {
        if (Instance != null) return;

        Instance = this;
    }

}
