using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyUI : MonoBehaviour
{
   public Text loadingText, currentPlayerCount, roomCode;

    private void Start()
    {
        if (PlayersManager.RoomCode != "")
        {
            roomCode.text = "Code: " + PlayersManager.RoomCode;
        }

    }
}
