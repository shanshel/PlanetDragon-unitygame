using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WinLoseScreen : MonoBehaviour
{
    public GameObject winObject, loseObject, backButton;
    public Text loseText;
    public static WinLoseScreen instance;

    private void Awake()
    {
        instance = this;
    }

    public void win()
    {
        Planet.planetinstance.StopAllCoroutines();
        GameManager.instance.toggleJoystick(false);


        Planet.planetinstance.transform.DOKill();
        Player.playerInstance.transform.DOKill();

        AudioManager.instance.gameObject.SetActive(false);
        Planet.planetinstance.gameObject.SetActive(false);

        winObject.SetActive(true);
        winObject.transform.DOShakeScale(1f, .2f, 6, 60, false).SetLoops(-1, LoopType.Restart);

        backButton.SetActive(true);
        ScoreManager.instance.saveMultiScore(+10);
        ScoreManager.instance.unlockMultiPlayerAchievements(true, PlayerPrefs.GetInt("muPoints", 0));
    }

    public void lose(int matchRank)
    {
        Planet.planetinstance.StopAllCoroutines();
        GameManager.instance.toggleJoystick(false);

        Planet.planetinstance.transform.DOKill();
        Player.playerInstance.transform.DOKill();

        AudioManager.instance.gameObject.SetActive(false);
        Planet.planetinstance.gameObject.SetActive(false);

        loseObject.SetActive(true);
        loseObject.transform.DOShakeScale(1f, .2f, 6, 60, false).SetLoops(-1, LoopType.Restart);

        backButton.SetActive(true);
        var losePoints = -10;
        if (matchRank == 3)
        {
            losePoints = 5;
        }
        else if (matchRank == 2)
        {
            losePoints = 0;
        }
        loseText.text = "-" + losePoints.ToString();
        ScoreManager.instance.saveMultiScore(losePoints);
    }

    private void OnDestroy()
    {
        winObject.transform.DOPause();
        loseObject.transform.DOPause();
    }
}
