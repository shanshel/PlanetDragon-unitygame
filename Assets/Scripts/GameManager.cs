using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using CloudOnce;
using GameAnalyticsSDK;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    public int totalPlanetFaces, filledPlanetFace;
    public GameObject losePrefab;
    public bool isGameOver = false;
    
    public bool isLoadingNextLevel;
    public int currentLevel = 0;


    private float playedTime = 0f;
    private float lastPlanetPlayedTime = 0f;
    public int score;

    private float extraPointTime = .3f;
    private float lastPointTime;
    private int extraPointAmount = 0;

    private bool isGAInit = false;

    public bool gamePaused;
    public float gameTimeScale = 1f;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        instance = this;
        freshStart();
    }

    private void freshStart()
    {
        instance = this;
        gameTimeScale = 1f;
        Time.timeScale = 1f;
    }

    void Start()
    {
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(200, 10);

        gameTimeScale = 1f;
        if (!isGAInit)
        {
            GameAnalytics.Initialize();
            isGAInit = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (isGamePaused()) return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            levelUp();
        }
    }

    public bool isGamePaused()
    {
        return gamePaused;
    }

    public void increaseFilledFaces()
    {
        filledPlanetFace++;
        if (filledPlanetFace == totalPlanetFaces)
        {
            levelUp();
        }
    }

    public void levelUp()
    {
        Player.playerInstance.makePlayerUnTouchAbleForWhile();
        StartCoroutine(nextLevelCo());
        var pointToAdd = Mathf.RoundToInt(4000 / ((playedTime - lastPlanetPlayedTime) + 1));
        lastPlanetPlayedTime = playedTime;

        if (pointToAdd > 5000)
        {
            pointToAdd = 100;
        }
        addPointEffect(pointToAdd, true);
        score += pointToAdd;
        UIManager.instance.setScoreText(score.ToString());

    }
    private void addPointEffect(int pointToAdd, bool extraEffect = false)
    {
        var outLineColor = new Color(0f, 0f, 0f);
        var fontSize = .5f;
        if (pointToAdd > 1 && pointToAdd <= 2)
        {
            outLineColor = new Color(0.5547556f, 0.8867924f, 0.4643111f);
            fontSize = .7f;
        }
        else if (pointToAdd > 2 && pointToAdd <= 4)
        {
            outLineColor = new Color(0.3163937f, 0.7909012f, 0.8490566f);
            fontSize = 1f;
        }
        else if (pointToAdd > 4 && pointToAdd <= 6)
        {
            outLineColor = new Color(0.08690815f, 0.4701018f, 0.8773585f);
            fontSize = 1.2f;
        }
        else if (pointToAdd > 6 && pointToAdd <= 8)
        {
            outLineColor = new Color(0.5159042f, 0.1970897f, 9716981f);
            fontSize = 1.4f;
        }
        else if (pointToAdd > 8 && pointToAdd <= 10)
        {
            outLineColor = new Color(0.972549f, 0.1960784f, 0.8611093f);
            fontSize = 1.6f;
        }
        else if (pointToAdd > 125)
        {
            outLineColor = new Color(0.972549f, 0.1960784f, 0.2963955f);
            fontSize = 2f;
        }
      
        if (extraEffect)
        {
            outLineColor = new Color(0.972549f, 0.1960784f, 0.2963955f);
            fontSize = 2f;
        }

        var pointP = GameObject.Instantiate(Planet.planetinstance.pointPerfab, new Vector3(-0.08f, 0.84f, -1.5f), Quaternion.identity);
        TextMeshPro txt = pointP.GetComponentInChildren<TextMeshPro>();
        txt.text = "+" + pointToAdd.ToString();
        if (extraEffect)
        {
            txt.DOFade(0f, 3f);
        } else
        {
            txt.DOFade(0f, 1f);
        }
       
        pointP.transform.DOMoveY(pointP.transform.position.y + .4f, 1);
        txt.outlineColor = outLineColor;
        txt.fontSize = fontSize;
        Destroy(pointP, 1f);
    }


    private void sumPointEffect(int summedPoints)
    {
        var outLineColor = new Color(0.972549f, 0.1960784f, 0.2963955f);
        var fontSize = 2f;

        var pointP = GameObject.Instantiate(Planet.planetinstance.pointPerfab, new Vector3(-0.08f, 0.84f, -1.5f), Quaternion.identity);
        TextMeshPro txt = pointP.GetComponentInChildren<TextMeshPro>();
        txt.text = "-" + summedPoints.ToString();
        txt.DOFade(0f, 3f);
        pointP.transform.DOMoveY(pointP.transform.position.y + .4f, 1.5f);
        txt.outlineColor = outLineColor;
        txt.fontSize = fontSize;
        Destroy(pointP, 3f);
    }

    private void makePlayerSmaller()
    {
        if (currentLevel > 10) return;
        Player.playerInstance.transform.localScale -= (Player.playerInstance.transform.localScale * 0.01f);

        if ( (currentLevel+1) >= Planet.planetinstance.planetModel.Length)
        {
            Planet.planetinstance.activeNextModel.transform.Find("PlanetItems").Find("Trail").localScale -= (Planet.planetinstance.planetModel[randomNextPlanetIndex].transform.Find("PlanetItems").Find("Trail").localScale * 0.01f);
        }
        else
        {
            Planet.planetinstance.activeNextModel.transform.Find("PlanetItems").Find("Trail").localScale -= (Planet.planetinstance.planetModel[currentLevel].transform.Find("PlanetItems").Find("Trail").localScale * 0.01f);

        }

    }

  

    private void resetFaces()
    {
        totalPlanetFaces = 0;
        filledPlanetFace = 0;
    }

    public int randomNextPlanetIndex;
    public IEnumerator nextLevelCo()
    {
        
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Application.version, "game", score);
        
        if ((currentLevel+1) >= Planet.planetinstance.planetModel.Length )
        {
            randomNextPlanetIndex = Random.Range(2, 9);
        }
        isLoadingNextLevel = true;
        
        resetFaces();
        Planet.planetinstance.beforeLevelingUp();
        Player.playerInstance.beforeLevelingUp();
        AudioManager.instance.playSFX(1);
        CameraShake.instance.shakeCamera(1.3f);
        makePlayerSmaller();
        CoreFace.instance.winAnimate();
        yield return new WaitForSeconds(1.3f);

        levelingUpEffect();
        currentLevel++;
        Planet.planetinstance.afterLevelingUp();
        //Player.playerInstance.afterLevelingUp();
        AudioManager.instance.playMusic(currentLevel);
       
        UIManager.instance.setCurrentLevelText(( currentLevel + 1).ToString());
        isLoadingNextLevel = false;

        //UN achievementCheck();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, Application.version, "game");

        ScoreManager.instance.unlockSinglePlayAchievements(score, currentLevel);
    }


    private void levelingPointUpEffect()
    {
        var levelUpPoint = GameObject.Instantiate(Planet.planetinstance.pointPerfab, new Vector3(-0.08f, 0.84f, -1.5f), Quaternion.identity);
        TextMeshPro leveltxtPoint = levelUpPoint.GetComponentInChildren<TextMeshPro>();
        leveltxtPoint.DOFade(0, 1);
        levelUpPoint.transform.DOMoveY(levelUpPoint.transform.position.y + .4f, 1);
        Destroy(levelUpPoint, 1f);

    }
    private void levelingUpEffect()
    {
        var pos = UIManager.instance.currentLevel.transform.position;
        pos.y -= 40f;
        var comp = UIManager.instance.currentLevel.GetComponentInChildren<ParticleSystem>();
        comp.Play();
        AudioManager.instance.playSFX(4);
    }

    public void GameOver()
    {
        if (!isGameOver)
        {

            if (MultiPlayerStat.isMultiMode())
            {
                isGameOver = true;
                //Player.playerInstance.die();
                CameraShake.instance.shakeCamera(.07f, .05f);
                AudioManager.instance.playSFX(0);
                gameTimeScale = 0.1f;
                //Time.timeScale = 0.1f;
                GameObject.Instantiate(losePrefab, Planet.planetinstance.transform);
                StartCoroutine(multiModeGameOverCorot());
                AudioManager.instance.playSFX(2);
            }
            else
            {
                ScoreManager.instance.saveScore(score);
                
                isGameOver = true;
                //Player.playerInstance.die();
                CameraShake.instance.shakeCamera(.07f, .05f);

                AudioManager.instance.playSFX(0);
                Time.timeScale = 0.1f;
                gameTimeScale = 0.1f;
                GameObject.Instantiate(losePrefab, Planet.planetinstance.transform);

                StartCoroutine(gameOverCorot());
                AudioManager.instance.playSFX(2);
            }
            

            
            

       
        }
    
    }
    public IEnumerator gameOverCorot()
    {
        toggleJoystick(false);
        unLockMouse();
        yield return new WaitForSeconds(.3f);
        UIManager.instance.showGameOverMenu();
    }
    public IEnumerator multiModeGameOverCorot()
    {
        
        sumScore(PlayersManager.getGlobalScoreGoal() / 4);
        yield return new WaitForSeconds(3f);
        gameTimeScale = 1f;
        extraPointAmount = 0;
        currentLevel -= 2;
        if (currentLevel < 0)
        {
            currentLevel = 0;
        }
        Player.playerInstance.makePlayerUnTouchAbleForWhile();
        UIManager.instance.setScoreText(score.ToString());
        isLoadingNextLevel = true;
        resetFaces();
        Planet.planetinstance.beforeLevelingUp();
        Player.playerInstance.beforeLevelingUp();
        AudioManager.instance.playSFX(1);
        CameraShake.instance.shakeCamera(1.3f);

        //Should Make Player Default Again
        yield return new WaitForSeconds(1.3f);
        currentLevel++;
        Planet.planetinstance.afterLevelingUp();
        AudioManager.instance.playMusic(currentLevel);
        UIManager.instance.setCurrentLevelText((currentLevel + 1).ToString());
        isLoadingNextLevel = false;
        isGameOver = false;
    }

    public void restartLevel()
    {
        LevelLoader.instance.LoadLevel(1);
    }

    public void decreaseFilledFaces()
    {
        filledPlanetFace--;
    }

    public void setTotalFace(int totalFace)
    {
        totalPlanetFaces = totalFace;
    }


    public void pauseGame()
    {
       
        if (!MultiPlayerStat.isMultiMode())
        {
            if (!isGameOver)
            {
                gamePaused = true;
                gameTimeScale = 0f;
                Time.timeScale = 0f;
                unLockMouse();
                toggleJoystick(false);
            }
        }
      
    }

    public void toggleJoystick(bool activate)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        UIManager.instance.transform.Find("JoystickCan").gameObject.SetActive(activate);
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            pauseGame();
        }
        else
        {
            resumeGame();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
 
        if (!focus)
        {
            pauseGame();
        }
        else
        {
            resumeGame();
        }
    }



    public void resumeGame()
    {

        
        
        if (!MultiPlayerStat.isMultiMode())
        {
            if (!isGameOver)
            {
                gamePaused = false;
                Time.timeScale = 1f;
                gameTimeScale = 1f;
                lockMouse();
                toggleJoystick(true);
            }
        }
       
        
        
        
        
        
    }

    public void lockMouse()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void unLockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    
    public void addScore()
    {
        var pointToAdd = 1;

        if (extraPointAmount > 8) extraPointAmount = 8;

        if (Time.time <= lastPointTime)
        {
            pointToAdd =  pointToAdd + extraPointAmount;
            extraPointAmount += 1;
        }
        else
        {
            extraPointAmount = 0;
        }

        lastPointTime = Time.time + extraPointTime;

        addPointEffect(pointToAdd);
        score += pointToAdd;
        UIManager.instance.setScoreText(score.ToString());
    }

    public void sumScore(int scoreSum = 1000)
    {
        if (scoreSum > score)
            scoreSum = score;

        sumPointEffect(scoreSum);
        score -= scoreSum;
        UIManager.instance.setScoreText(score.ToString());
    }

    public void setPlayedTime(float time )
    {
        playedTime = time;
    }

    public void updateGamePlayTime()
    {

        if (MultiPlayerStat.isMultiMode()) return;


        if (isGameOver && !MultiPlayerStat.isMultiMode())
        {
            return;
        }

        if (!InGameScene.instance.isGameStarted)
        {
            return;
        }

        if ((Time.unscaledDeltaTime - Time.deltaTime) > 1f)
            playedTime += Time.deltaTime;
        else
            playedTime += Time.unscaledDeltaTime;


       
        UIManager.instance.setTimeText(Mathf.RoundToInt(playedTime).ToString());
     

    }

    public float getPlayedTime()
    {
        return playedTime;
    }
  
}
