using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] Text textField;
    
    public void SetPlacementCountString(string newValue)
    {
        textField.text = newValue + "/2";
    }
    public string GetPlacementCountString()
    {
        return textField.text;
    }
}
