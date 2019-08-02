using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayersManager : MonoBehaviour
{
    public static Dictionary<string, PlayerNetwork> players = new Dictionary<string, PlayerNetwork>();
    public static string RoomCode = "";
    public static void registerPlayer(string _playerID, PlayerNetwork _player)
    {
        players.Add(_playerID, _player);
        Debug.Log("player Register: " + _playerID);
        _player.transform.name = _playerID;
    }

    public static void unRegisterPlayer(string _playerID)
    {
        
        players.Remove(_playerID);
        var objectToDestroy = PlayerRankSlider.instance.playerSnailParent.transform.Find(_playerID).gameObject;
        if (objectToDestroy != null)
        {
            Destroy(PlayerRankSlider.instance.playerSnailParent.transform.Find(_playerID).gameObject);
        }

    }

    public static int getPlayerCount()
    {
        return players.Count;
    }
    
    public static PlayerNetwork getPlayer(string _playerID)
    {
        return players[_playerID];
    }

    public static string getPlayerName()
    {
        return "PlayerName_" + Random.Range(0, 100000);
    }


    public static int getGlobalScoreGoal()
    {
        int scoreGoal = 0;
        foreach (PlayerNetwork _player in players.Values)
        {
            scoreGoal = _player.scoreGoal;
            break;
        }
        return scoreGoal;
    }

    public static PlayerNetwork winnerPlayer;
    public static void declearWinner()
    {

        int highestScore = 0;
        
        foreach (PlayerNetwork _player in players.Values)
        {
            if (_player.score > highestScore)
            {
                highestScore = _player.score;
                winnerPlayer = _player;
            }
        }

        foreach (PlayerNetwork _player in players.Values)
        {
            _player.winnerID = winnerPlayer.playerID;
        }

        
    }

    public static void disconnectFromServer()
    {
        var matchInfo = NetworkManager.singleton.matchInfo;
        NetworkManager.singleton.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, NetworkManager.singleton.OnDropConnection);
        NetworkManager.singleton.StopHost();
    }

    public static int getMyPlayerRankInMatch(string playerID)
    {
        var myScore = players[playerID].score;
        int MyRankInMatch = 1;

        foreach (PlayerNetwork _player in players.Values)
        {
            if (_player.score > myScore)
            {
                MyRankInMatch++;
            }
        }

        return MyRankInMatch;
    }

}
