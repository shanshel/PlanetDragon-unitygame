using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject creditMenu;
    public Text bestScoreText;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (UIManager.instance.privateCodeText != null)
            {
                UIManager.instance.privateCodeInputObject = UIManager.instance.customMatchScreen.GetComponentInChildren<InputField>().gameObject;
                UIManager.instance.defaultCodeText = UIManager.instance.privateCodeText.text;
            }


            if (PlayerPrefs.HasKey("bestScore"))
            {
                bestScoreText.text = PlayerPrefs.GetInt("bestScore").ToString();
            }
            else
            {
                bestScoreText.text = "0";
            }
        }
      
    }

    public void onStartClicked()
    {
        AudioManager.instance.playSFX(3);
        LevelLoader.instance.LoadLevel(1);
  
    }


    public void onScoreClicked()
    {
        AudioManager.instance.playSFX(3);
    }

    public void onCreditClicked()
    {
        AudioManager.instance.playSFX(3);
        creditMenu.SetActive(true);
    }

    public void onCreditClose()
    {
        AudioManager.instance.playSFX(3);
        creditMenu.SetActive(false);
    }


}
