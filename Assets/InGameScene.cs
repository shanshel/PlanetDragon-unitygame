using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject offlinePlayerSnail, planetContainerObject, AudioContainerObject;
    private PlayerSnailSlider playerSnail;
    private int bestScore;
    public bool isGameStarted;
    public static InGameScene instance;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        if (!MultiPlayerStat.isMultiMode())
        {

            
          
            setUpOfflineSnail();
            startPlaying();
        }
   
    }

    private void setUpOfflineSnail()
    {
        var gameObj = GameObject.Instantiate(offlinePlayerSnail, UIManager.instance.playerSnailsContainer.transform);
        playerSnail = gameObj.GetComponent<PlayerSnailSlider>();
        playerSnail.PlayerName.text = "Me";

        bestScore = PlayerPrefs.GetInt("bestScore", 0);

        if (PlayerPrefs.GetInt("bestScore", 0) != 0)
        {
            playerSnail.localSlider.maxValue = bestScore;
            PlayerRankSlider.instance.lastRankSlider.gameObject.SetActive(true);
            PlayerRankSlider.instance.lastRankSlider.maxValue = playerSnail.localSlider.maxValue;
            PlayerRankSlider.instance.lastRankSlider.value = bestScore;

            PlayerRankSlider.instance.MaxScoreGoal.text = playerSnail.localSlider.maxValue.ToString();
            PlayerRankSlider.instance.MidScoreGoal.text = (playerSnail.localSlider.maxValue / 2).ToString();
        }
        else
        {
            playerSnail.localSlider.maxValue = 1000;
            PlayerRankSlider.instance.lastRankSlider.gameObject.SetActive(true);
            PlayerRankSlider.instance.lastRankSlider.maxValue = playerSnail.localSlider.maxValue;
            PlayerRankSlider.instance.lastRankSlider.value = 0;

            PlayerRankSlider.instance.MaxScoreGoal.text = playerSnail.localSlider.maxValue.ToString();
            PlayerRankSlider.instance.MidScoreGoal.text = (playerSnail.localSlider.maxValue / 2).ToString();
        }

      

       
    }

    private void startPlaying()
    {
        if (isGameStarted) return;
        AudioContainerObject.SetActive(true);
        planetContainerObject.SetActive(true);
        isGameStarted = true;
        UIManager.instance.LobbyUI.gameObject.SetActive(false);
    }

    private void offlineSnailUpdate()
    {
        if (MultiPlayerStat.isMultiMode()) return;

        if (PlayerPrefs.GetInt("bestScore", 0) != 0)
        {
            playerSnail.localSlider.maxValue = Mathf.Floor((bestScore * 1.5f) + (playerSnail.localSlider.value * 1.5f));
            playerSnail.localSlider.value = Mathf.Lerp(playerSnail.localSlider.value, GameManager.instance.score, Time.deltaTime * 1f);
            PlayerRankSlider.instance.MaxScoreGoal.text = Mathf.Floor(playerSnail.localSlider.maxValue).ToString();
            PlayerRankSlider.instance.MidScoreGoal.text = Mathf.Floor((playerSnail.localSlider.maxValue / 2)).ToString();

            PlayerRankSlider.instance.lastRankSlider.maxValue = playerSnail.localSlider.maxValue;
            PlayerRankSlider.instance.lastRankSlider.value = bestScore;
        }
        else
        {
            playerSnail.localSlider.maxValue = ( playerSnail.localSlider.value * 2f ) + 1000;
            playerSnail.localSlider.value = Mathf.Lerp(playerSnail.localSlider.value, GameManager.instance.score, Time.deltaTime * 1f);
            PlayerRankSlider.instance.MaxScoreGoal.text = Mathf.Floor(playerSnail.localSlider.maxValue).ToString();
            PlayerRankSlider.instance.MidScoreGoal.text = Mathf.Floor((playerSnail.localSlider.maxValue / 2)).ToString();

            PlayerRankSlider.instance.lastRankSlider.maxValue = playerSnail.localSlider.maxValue;
            PlayerRankSlider.instance.lastRankSlider.value = 0;
        }

    }

    private void startGameIfLobbyFull()
    {
        if (isGameStarted) return;

        UIManager.instance.LobbyUI.currentPlayerCount.text = PlayersManager.getPlayerCount().ToString() + "/" + CustomNetWork.instance.getMatchSize().ToString();
        if (CustomNetWork.instance.isFullLobby())
        {
            startPlaying();
        }
   
    }
    // Update is called once per frame
    void Update()
    {


        if (GameManager.instance.isGamePaused()) return;
        requestGamePlayTimeUpdate();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!MultiPlayerStat.isMultiMode())
            {
                
                AudioManager.instance.playSFX(3);
                UIManager.instance.pauseGame();
            }
            
        }
        offlineSnailUpdate();
        startGameIfLobbyFull();

        //PlayerRankSlider.instance.lastRankSlider.maxValue =;
        //playerSnail.localSlider.maxValue = playerSnail.localSlider.value + bestScore;

        /*
         * 
         * 
        PlayerRankSlider.instance.lastRankSlider.maxValue = Mathf.Lerp(PlayerRankSlider.instance.lastRankSlider.maxValue,  playerSnail.localSlider.value + 1000 + PlayerRankSlider.instance.lastRankSlider.value, 1f * Time.deltaTime);
        playerSnail.localSlider.maxValue = Mathf.Lerp(playerSnail.localSlider.maxValue, PlayerRankSlider.instance.lastRankSlider.maxValue, 1f * Time.deltaTime);
        PlayerRankSlider.instance.MaxScoreGoal.text = playerSnail.localSlider.maxValue.ToString();
        PlayerRankSlider.instance.MidScoreGoal.text = (playerSnail.localSlider.maxValue/2).ToString();
        */
    }

    void requestGamePlayTimeUpdate()
    {
        GameManager.instance.updateGamePlayTime();
    }
}
