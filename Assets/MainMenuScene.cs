using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuScene : MonoBehaviour
{
    public Text mainMenuMuScoreText;
    public Text mainMenuSinScoreText;

    private void Start()
    {
        mainMenuMuScoreText.text = PlayerPrefs.GetInt("muPoints", 0).ToString();
        mainMenuSinScoreText.text = PlayerPrefs.GetInt("bestScore", 0).ToString();
    }

}
