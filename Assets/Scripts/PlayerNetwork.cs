using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetwork : NetworkBehaviour
{
    [SyncVar]
    public int score;
    public int lastScore;
    [SyncVar]
    public float playedTime;
    [SyncVar]
    public int scoreGoal = 0;
    [SyncVar]
    public float timeGoal = 60;
    [SyncVar]
    public string winnerID = "";
    [SyncVar]
    public string playerName;

    [SyncVar]
    public string playerStatus = "alive";

    private string localPlayerName = "";
    private bool showedWinLoseScreen = false;
    public PlayerSnailSlider playerSnailObject;

    public string playerID;
    
    private void Start()
    {
        
        playerSnailObject.transform.SetParent(UIManager.instance.playerSnailsContainer.transform, false);
        playerSnailObject.gameObject.SetActive(true);
        
        if (isLocalPlayer)
        {
            localPlayerName = ScoreManager.instance.getPlayerName();

            if (localPlayerName == "")
            {
                localPlayerName = "Player" + playerID;
            }
            playerSnailObject.transform.SetAsLastSibling();
            playerSnailObject.setMyPlayerDefaultColor();
            playerSnailObject.NameBackground.color = new Color(.9f, 1f, .9f);
            CmdSetPlayerName(localPlayerName);
        }
        else
        {
            playerSnailObject.transform.SetAsFirstSibling();
        }

        if (isServer)
        {
            scoreGoal = SharedGameInfo.instance.scoreGoal;
            timeGoal = SharedGameInfo.instance.timeGoal;
        }
    }

    private void Update()
    {
        if (isServer)
        {
            if (!showedWinLoseScreen)
                playedTime = SharedGameInfo.instance.MultiPlayerPlayedTime;
        }

        if (isLocalPlayer)
        {
            PlayerRankSlider.instance.setScoreGoal(scoreGoal);
            CmdSendPlayerScore(GameManager.instance.score);
            score = GameManager.instance.score;

            if (winnerID != "" && !showedWinLoseScreen)
            {
                showedWinLoseScreen = true;
                if (winnerID == playerID)
                {
                    WinLoseScreen.instance.win();
                }
                else
                {
                    WinLoseScreen.instance.lose(PlayersManager.getMyPlayerRankInMatch(playerID));
                }
            }

            GameManager.instance.setPlayedTime(playedTime);
            var time = Mathf.RoundToInt(timeGoal - playedTime);
            if (time < 0)
                time = 0;
            UIManager.instance.setTimeText(time.ToString());


            if (GameManager.instance.isGameOver && playerStatus == "alive")
                CmdKillPlayer();
        }


        //Not Synced Values should always updated 
    

        
        playerSnailObject.localSlider.value = Mathf.Lerp(playerSnailObject.localSlider.value, score, 2 * Time.deltaTime);
        playerSnailObject.localSlider.maxValue = scoreGoal;

        if (playerName == localPlayerName)
        {
            playerSnailObject.PlayerName.text = "Me";
        }
        else
        {
            playerSnailObject.PlayerName.text = playerName;
        }


  
        if (playerStatus == "dead")
        {
            playerSnailObject.SnailImage.color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(Time.time, .5f));
        }
        else
        {
            playerSnailObject.SnailImage.color = Color.white;
        }

    }

    [Command]
    public void CmdSendPlayerScore(int playerScore)
    {
        if (!InGameScene.instance.isGameStarted)
            return;
        if ((score - playerScore) > 2000)
            return;
        
        score = playerScore;

        if (score >= scoreGoal && playedTime > 10f)
        {
            PlayersManager.declearWinner();
        }
        else if (playedTime >= timeGoal)
        {
            PlayersManager.declearWinner();
        }

    }

    [Command]
    public void CmdSetPlayerName(string _playerName)
    {
        
        if (_playerName == "")
        {
            playerName = "Player" + playerID;
        }
        else
        {
            playerName = _playerName;
        }
    }

    [Command]
    public void CmdKillPlayer()
    {
        Debug.Log("kill the player");
        playerStatus = "dead";
        StartCoroutine(playerRevive());
    }

    public IEnumerator playerRevive()
    {
        yield return new WaitForSeconds(1f);
        playerStatus = "alive";
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        playerID = GetComponent<NetworkIdentity>().netId.ToString();
        PlayersManager.registerPlayer(playerID, this);
        playerSnailObject.name = playerID;
    }

    private void OnDisable()
    {
        playerID = GetComponent<NetworkIdentity>().netId.ToString();
        PlayersManager.unRegisterPlayer(playerID);
        
    }

}
