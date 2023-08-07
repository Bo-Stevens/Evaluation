using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] Text textField;

    private void Start()
    {
        textField.text = "0/" + PlayerManager.TeleporterSpawner.MaxCubeAmount;
    }

    public void SetPlacementCountString(string newValue)
    {
        textField.text = newValue + "/" + PlayerManager.TeleporterSpawner.MaxCubeAmount; 
    }
    public string GetPlacementCountString()
    {
        return textField.text;
    }

    public void TeleporterButtonPressed()
    {
        PlayerManager.Player.FlipMouseControlSceme();
    }
}
