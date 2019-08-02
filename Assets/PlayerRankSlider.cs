using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerRankSlider : MonoBehaviour
{
    public static PlayerRankSlider instance; 
    public Text MidScoreGoal, MaxScoreGoal;
    public GameObject playerSnailParent;
    public Slider lastRankSlider;

    private void Awake()
    {
        instance = this;
    }

    public void setScoreGoal(int score)
    {
        MidScoreGoal.text = (score / 2).ToString();
        MaxScoreGoal.text = score.ToString();
    }

}
