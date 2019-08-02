using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class CustomNetWork : MonoBehaviour
{
    public static CustomNetWork instance;

    private void Awake()
    {
        if (instance) return;
        instance = this;
    }
    private void Start()
    {
        
        
    }

    private uint roomSize = 4;
    private string roomName = "default";
    private string password = "";

    public void createRoom(string _roomName = "Online", string _password = "", int elo = 0)
    {
        if (_roomName != null && _roomName != "")
        {
            NetworkManager.singleton.matchMaker.CreateMatch(_roomName, roomSize, true, _password, "", "", elo, 0, NetworkManager.singleton.OnMatchCreate);
        }
    }

    public void quickMatch()
    {
        if (NetworkManager.singleton.matchMaker == null)
        {
            NetworkManager.singleton.StartMatchMaker();
        }

        NetworkManager.singleton.matchMaker.ListMatches(0, 5, "", true, 0, 0, OnMatchListQuickMatch);
    }


    public void OnMatchListQuickMatch(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        if (success)
        {
            if (matchList.Count == 0)
            {
                createRoom();
            }
            else
            {
                //join first match
                NetworkManager.singleton.matchMaker.JoinMatch(matchList[0].networkId, "", "", "", 0, 0, NetworkManager.singleton.OnMatchJoined);
            }
            return;
        }
        Debug.Log("ERROR: no success response when bring matches");
        //base.OnMatchList(success, extendedInfo, matchList);
    }


    public void createLocal()
    {
        NetworkManager.singleton.StartHost();
    }

    public void joinLocal()
    {
        NetworkManager.singleton.StartClient();
    }

    public void createPrivateRoom()
    {
        string roomCode = getRandomRoomCode();
        PlayersManager.RoomCode = roomCode;
        createRoom(roomCode, "SimplePassword3000");
    }

    public void joinPrivateRoom(string filterName)
    {
        Debug.Log(filterName);
        NetworkManager.singleton.matchMaker.ListMatches(0, 5, filterName, false, 0, 0, OnMatchListPrivateMatch);
    }

    public void OnMatchListPrivateMatch(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        if (success && matchList.Count > 0)
        {
      
            NetworkManager.singleton.matchMaker.JoinMatch(matchList[0].networkId, "SimplePassword3000", "", "", 0, 0, NetworkManager.singleton.OnMatchJoined);
          
            return;
        }
        UIManager.instance.shakeScale(UIManager.instance.privateCodeInputObject);
       
        //base.OnMatchList(success, extendedInfo, matchList);
    }

    public uint getMatchSize()
    {
        return NetworkManager.singleton.matchSize;
    }
    public bool isFullLobby()
    {

        if (PlayersManager.getPlayerCount() == NetworkManager.singleton.matchSize)
        {
            if (null != NetworkManager.singleton.matchInfo)
                NetworkManager.singleton.matchMaker.SetMatchAttributes(NetworkManager.singleton.matchInfo.networkId, false, 0, convertRoomToPrivateCallback);

            return true;
        }
        return false;
    }

    public void convertRoomToPrivateCallback(bool scuess, string extendedInfo)
    {
        Debug.Log(scuess);
    }

    public string getRandomRoomCode(int length = 7)
    {
        string characters = "0123456789asdfghjkqwertyupzxcvbm";
        string randomRoomName = "";
        for (int i = 0; i < length; i++)
        {
            var randomIndex = Random.Range(0, 31);
            string randomCharacter = characters.Substring(randomIndex, 1);
            randomRoomName = randomRoomName + characters.Substring(randomIndex, 1);
        }

        return randomRoomName;
    }

}
