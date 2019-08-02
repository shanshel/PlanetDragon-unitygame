using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CloudOnce;
using System;

public class ScoreManager : MonoBehaviour
{
  
    public static ScoreManager instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        //Cloud.OnInitializeComplete += CloudOnInitComplete;
        //Cloud.Initialize(true, true);
    }

    public void CloudOnInitComplete()
    {
        //Cloud.OnInitializeComplete -= CloudOnInitComplete;
        //fetchDataFromTheCloud();


    }

    public void fetchDataFromTheCloud()
    {
        /*
         if (CloudOnce.CloudVariables.varVersion >= PlayerPrefs.GetInt("varVersion", 0))
         {
             //if They are in Same Version or cloud version bigger update the local informations
             PlayerPrefs.SetInt("bestScore", CloudOnce.CloudVariables.Score);
             PlayerPrefs.SetInt("muPoints", CloudOnce.CloudVariables.muPoints);
             PlayerPrefs.GetInt("varVersion", CloudOnce.CloudVariables.varVersion);
         }
         else if (PlayerPrefs.GetInt("varVersion", 0) > CloudOnce.CloudVariables.varVersion)
         {
             //if local bigger than cloud versions send data to the cloud
             sendDataToTheCloud();

         }
         */



     }

     public void sendDataToTheCloud()
     {
         /*
          CloudOnce.CloudVariables.Score = PlayerPrefs.GetInt("bestScore", 0);
          CloudOnce.CloudVariables.muPoints = PlayerPrefs.GetInt("muPoints", 0);
          CloudOnce.CloudVariables.varVersion = CloudOnce.CloudVariables.varVersion + 1;
          PlayerPrefs.SetInt("varVersion", CloudOnce.CloudVariables.varVersion);
          Cloud.Storage.Save();
          */

    }

    public void submitScoreToLeaderBoard()
     {
         /*
         Leaderboards.HighScore.SubmitScore(PlayerPrefs.GetInt("bestScore", 0));
         */
    }

    public delegate void EventHandler();
    public event EventHandler OnSuchEvent;

    public void saveScore(int score)
    {
       
        var localHighScore = PlayerPrefs.GetInt("bestScore", 0);
        if (score > localHighScore)
        {
            PlayerPrefs.SetInt("bestScore", score);
            PlayerPrefs.SetInt("varVersion", PlayerPrefs.GetInt("varVersion", 0) + 1);
        }
      
    }

    public void saveMultiScore(int addOrSum)
    {
        PlayerPrefs.SetInt("muPoints", PlayerPrefs.GetInt("muPoints", 0) + addOrSum);
        PlayerPrefs.SetInt("varVersion", PlayerPrefs.GetInt("varVersion", 0) + 1);
    }


    public string getPlayerName()
    {
        return "";
        //return Cloud.PlayerDisplayName;
    }

    public void unlockMultiPlayerAchievements(bool isWin, int muPoints)
    {
        /*
         if (isWin && muPoints > 10 && !CloudOnce.Achievements.BronzeMedal.IsUnlocked)
             CloudOnce.Achievements.BronzeMedal.Increment(CloudOnce.Achievements.BronzeMedal.Progress, CloudOnce.Achievements.BronzeMedal.Progress + 1);
         if (isWin && muPoints > 60 && !CloudOnce.Achievements.BronzeCup.IsUnlocked && CloudOnce.Achievements.BronzeMedal.IsUnlocked)
             CloudOnce.Achievements.BronzeCup.Increment(CloudOnce.Achievements.BronzeCup.Progress, CloudOnce.Achievements.BronzeCup.Progress + 1);
         if (isWin && muPoints > 120 && !CloudOnce.Achievements.BronzeCrown.IsUnlocked && CloudOnce.Achievements.BronzeCup.IsUnlocked)
             CloudOnce.Achievements.BronzeCrown.Increment(CloudOnce.Achievements.BronzeCrown.Progress, CloudOnce.Achievements.BronzeCrown.Progress + 1);

         if (isWin && muPoints > 240 && !CloudOnce.Achievements.SliverMedal.IsUnlocked && CloudOnce.Achievements.BronzeCrown.IsUnlocked)
             CloudOnce.Achievements.SliverMedal.Increment(CloudOnce.Achievements.SliverMedal.Progress, CloudOnce.Achievements.SliverMedal.Progress + 1);
         if (isWin && muPoints > 480 && !CloudOnce.Achievements.SliverCup.IsUnlocked && CloudOnce.Achievements.SliverMedal.IsUnlocked)
             CloudOnce.Achievements.SliverCup.Increment(CloudOnce.Achievements.SliverCup.Progress, CloudOnce.Achievements.SliverCup.Progress + 1);
         if (isWin && muPoints > 600 && !CloudOnce.Achievements.SliverCrown.IsUnlocked && CloudOnce.Achievements.SliverCup.IsUnlocked)
             CloudOnce.Achievements.SliverCrown.Increment(CloudOnce.Achievements.SliverCrown.Progress, CloudOnce.Achievements.SliverCrown.Progress + 1);


         if (isWin && muPoints > 1200 && !CloudOnce.Achievements.GoldMedal.IsUnlocked && CloudOnce.Achievements.SliverCrown.IsUnlocked)
             CloudOnce.Achievements.GoldMedal.Increment(CloudOnce.Achievements.GoldMedal.Progress, CloudOnce.Achievements.GoldMedal.Progress + 1);
         if (isWin && muPoints > 2400 && !CloudOnce.Achievements.GoldCup.IsUnlocked && CloudOnce.Achievements.GoldMedal.IsUnlocked)
             CloudOnce.Achievements.GoldCup.Increment(CloudOnce.Achievements.GoldCup.Progress, CloudOnce.Achievements.GoldCup.Progress + 1);
         if (isWin && muPoints > 4800 && !CloudOnce.Achievements.GoldCrown.IsUnlocked && CloudOnce.Achievements.GoldCup.IsUnlocked)
             CloudOnce.Achievements.GoldCrown.Increment(CloudOnce.Achievements.GoldCrown.Progress, CloudOnce.Achievements.GoldCrown.Progress + 1);
             */

    }

    public void unlockSinglePlayAchievements(int _score, int _level)
     {
         /*
         if (_level >= 1 && !CloudOnce.Achievements.ChickenPlanet.IsUnlocked)
             CloudOnce.Achievements.ChickenPlanet.Unlock();
         if (_level >= 2 && !CloudOnce.Achievements.SheepPlanet.IsUnlocked)
             CloudOnce.Achievements.SheepPlanet.Unlock();
         if (_level >= 3 && !CloudOnce.Achievements.PigPlanet.IsUnlocked)
             CloudOnce.Achievements.PigPlanet.Unlock();
         if (_level >= 4 && !CloudOnce.Achievements.GoatPlanet.IsUnlocked)
             CloudOnce.Achievements.GoatPlanet.Unlock();
         if (_level >= 5 && !CloudOnce.Achievements.HorsePlanet.IsUnlocked)
             CloudOnce.Achievements.HorsePlanet.Unlock();
         if (_level >= 6 && !CloudOnce.Achievements.CowPlanet.IsUnlocked)
             CloudOnce.Achievements.CowPlanet.Unlock();
         if (_level >= 7 && !CloudOnce.Achievements.BullPlanet.IsUnlocked)
             CloudOnce.Achievements.BullPlanet.Unlock();
         if (_level >= 8 && !CloudOnce.Achievements.HippoPlanet.IsUnlocked)
             CloudOnce.Achievements.HippoPlanet.Unlock();
         if (_level >= 9 && !CloudOnce.Achievements.StarFishPlanet.IsUnlocked)
             CloudOnce.Achievements.StarFishPlanet.Unlock();
         if (_level >= 10 && !CloudOnce.Achievements.WolfPlanet.IsUnlocked)
             CloudOnce.Achievements.WolfPlanet.Unlock();
         if (_level >= 11 && !CloudOnce.Achievements.TigerPlanet.IsUnlocked)
             CloudOnce.Achievements.TigerPlanet.Unlock();
         if (_level >= 12 && !CloudOnce.Achievements.PandaPlanet.IsUnlocked)
             CloudOnce.Achievements.PandaPlanet.Unlock();
         if (_level >= 13 && !CloudOnce.Achievements.DeerPlanet.IsUnlocked)
             CloudOnce.Achievements.DeerPlanet.Unlock();
         if (_level >= 14 && !CloudOnce.Achievements.DragonPlanet.IsUnlocked)
             CloudOnce.Achievements.DragonPlanet.Unlock();


         if (_score >= 5000)
             CloudOnce.Achievements.YouRock.Unlock();
         if (_score >= 10000)
             CloudOnce.Achievements.Unbelievable.Unlock();
         if (_score >= 20000)
             CloudOnce.Achievements.Epic.Unlock();

         if (_level >= 1)
             CloudOnce.Achievements.ChickenEater.Increment(1, 10000);
         if (_level >= 5)
             CloudOnce.Achievements.HorseRider.Increment(1, 10000);
         if (_level >= 8)
             CloudOnce.Achievements.PlanetEater.Increment(1, 10000);
             */
    }



}
