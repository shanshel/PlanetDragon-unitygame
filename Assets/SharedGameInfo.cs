using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SharedGameInfo : NetworkBehaviour
{
    [SerializeField]
    private int[] avilableScoreGoar;

    public int scoreGoal;
    public float timeGoal;
    public bool timerStart = false;
    public float MultiPlayerPlayedTime;
    public static SharedGameInfo instance;
    private float internalTimer = 0f;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        scoreGoal = avilableScoreGoar[Random.Range(0, avilableScoreGoar.Length)];
        timeGoal = Mathf.Floor(scoreGoal / 20);
    }

    private void Update()
    {
        if (!timerStart)
        {
            internalTimer += Time.deltaTime;
            if (internalTimer > 1f)
            {
                timerStart = true;
            }
        }
    

        if (timerStart)
        {
            if ((Time.unscaledDeltaTime - Time.deltaTime) > 1f)
                MultiPlayerPlayedTime += Time.deltaTime;
            else
                MultiPlayerPlayedTime += Time.unscaledDeltaTime;
        }
    }


}
