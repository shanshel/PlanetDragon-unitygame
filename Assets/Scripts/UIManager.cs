using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text timeTextObject, currentLevel, scoreText, gameOverScoreText;
    public GameObject optionScreen, pauseScreen, ingameScreen, playerSnailsContainer, customMatchScreen;
    public LobbyUI LobbyUI;
    public Slider musicSlider, SFXSlider, speedSlider;
    public string defaultCodeText = "";
    [SerializeField]
    public Text privateCodeText;
    public GameObject privateCodeInputObject;



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }



    public void setTimeText(string txt)
    {
        if (!InGameScene.instance.isGameStarted) return;
        timeTextObject.text = txt;
    }

    public void onOptionClicked()
    {
        AudioManager.instance.playSFX(3);
        optionScreen.SetActive(true);
    }
    public void closeOption()
    {
        AudioManager.instance.playSFX(3);
        optionScreen.SetActive(false);
    }

    public void setMusicLevel()
    {
        AudioManager.instance.setMusicLevel();
    }

    public void setSFXLevel()
    {
        AudioManager.instance.setSFXLevel();
    }

    public void resumeGame()
    {
        Player.playerInstance.startLerping();
        Planet.planetinstance.transform.position = new Vector3(0f, 0f, 0f);
        AudioManager.instance.playSFX(3);
        pauseScreen.SetActive(false);
        GameManager.instance.resumeGame();
    }

    public void pauseGame()
    {
        Player.playerInstance.stopLerping();
        Planet.planetinstance.transform.position = new Vector3(100f, 0f, 0f);
        AudioManager.instance.playSFX(3);
        pauseScreen.SetActive(true);
        GameManager.instance.pauseGame();
    }

    public void backToMainMenu()
    {
        AudioManager.instance.playSFX(3);
        LevelLoader.instance.LoadLevel(0);
    }

    public void setCurrentLevelText(string txt)
    {
        currentLevel.text = txt;
    }

    public void setScoreText(string txt)
    {
        scoreText.text = txt;
        gameOverScoreText.text = txt;
    }

    public void showGameOverMenu()
    {
        transform.Find("GameOverMenu").gameObject.SetActive(true);
    }

    public void onPlayAgainClicked()
    {
        GameManager.instance.restartLevel();
    }

    public void setSpeedLevel(float value)
    {
        speedSlider.value = value;
    }

    public void UIQuickJoin()
    {
        LevelLoader.instance.fakeLoading(10f);
        CustomNetWork.instance.quickMatch();
    }



    public void UICreatePrivate()
    {
        LevelLoader.instance.fakeLoading(10f);
        CustomNetWork.instance.createPrivateRoom();
    }

    public void UIJoinPrivate()
    {

        if (privateCodeText.text == "" || privateCodeText.text == defaultCodeText)
        {
            shakeScale(privateCodeInputObject.gameObject);
            return;
        }
        CustomNetWork.instance.joinPrivateRoom(privateCodeText.text);
        LevelLoader.instance.fakeLoading(10f);
    }

    public void shakeScale(GameObject obj)
    {
        obj.transform.DOPause();
        obj.transform.DOShakeScale(.5f, .2f, 8, 60f, false);
    }

    public void UICreateLocal()
    {
        LevelLoader.instance.fakeLoading(10f);
        CustomNetWork.instance.createLocal();
    }

    public void UIJoinLocal()
    {
        LevelLoader.instance.fakeLoading(10f);
        CustomNetWork.instance.joinLocal();
    }

    public void openCustomMatchScreen()
    {
        customMatchScreen.SetActive(true);
    }

    public void closeCustomMatchScreen()
    {
        customMatchScreen.SetActive(false);
    }

    public void leaveMatch()
    {
        LevelLoader.instance.fakeLoading(1f);
        PlayersManager.disconnectFromServer();
    }


}
