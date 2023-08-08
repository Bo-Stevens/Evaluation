using Fusion;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkObject))]
public class PlayerCountUI : NetworkBehaviour, ISpawned
{
    [SerializeField] Text playerCountText;
    [SerializeField] public int PlayerCount;

    public void PlayerJoined()
    {
        PlayerCount += 1;
        SetPlayerText();
    }

    public void SetPlayerText()
    {
        playerCountText.text = PlayerCount + "";
    }
    public void PlayerLeft()
    {
        PlayerCount -= 1;
        SetPlayerText();
    }

}
