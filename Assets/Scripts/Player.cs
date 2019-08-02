using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public static Player playerInstance;
    public GameObject playerModel;
    public GameObject hiddenBodyPart;
    //public GameObject playerHead;
    public float lerpTime = 0.4f;
    public float lerpTo = .1f;
    private float unTouchAbleTimer = 1f;
    private void Awake()
    {
        playerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        startLerping();
        makePlayerUnTouchAbleForWhile();
    }

    public void startLerping()
    {
        playerModel.transform.DOMoveX(lerpTo, lerpTime).SetLoops(-1, LoopType.Yoyo);
        playerModel.transform.DOMoveY(playerModel.transform.position.y + .05f, lerpTime).SetLoops(-1, LoopType.Yoyo);
    }

    public void stopLerping()
    {
        playerModel.transform.DOPause();
    }

    private float nextActionTime = 0.0f;
    private float period = .05f;

    // Update is called once per frame
    private bool isLeaping;
    private float nextLeap = 0.0f;
    private float leapEvery = 1f;
    void Update()
    {
        //if timescale != 1 then player died 
        if (GameManager.instance.isGamePaused() || GameManager.instance.isLoadingNextLevel || GameManager.instance.gameTimeScale != 1) return;



 


        var worldUp = Vector3.up;
        if (Planet.planetinstance.lastDir.y > 0.4f)
        {
            worldUp = Vector3.back;
        }

        playerModel.transform.LookAt(new Vector3(-Planet.planetinstance.lastDir.x * 5f, -Planet.planetinstance.lastDir.y * 5f, 7f), worldUp);


        unTouchAbleTimer -= (Time.deltaTime * GameManager.instance.gameTimeScale);
    
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            Instantiate(hiddenBodyPart, playerModel.transform.position, playerModel.transform.rotation, Planet.planetinstance.activeModel.transform);
        }


        int layerMask = 1 << 0;
        RaycastHit hit;

        //ray 



        if (
            Physics.Raycast(playerModel.transform.position, transform.TransformDirection(Planet.planetinstance.activeModel.transform.position - playerModel.transform.position), out hit, 1.5f, layerMask)
            )
        {
         
            if (hit.collider.tag == "planet")
            {
                
                Planet.planetinstance.hitPiece(hit.collider.gameObject, hit.point);
            }
        }
    
    
    
    }

    public void makePlayerUnTouchAbleForWhile()
    {
        unTouchAbleTimer = 1f;
        StartCoroutine(playerFlashing());

    }

    private IEnumerator playerFlashing()
    {

        while (unTouchAbleTimer > 0f)
        {
            PlayerHead.headInstante.gameObject.SetActive(false);
            yield return new WaitForSeconds(.05f);
            PlayerHead.headInstante.gameObject.SetActive(true);
            yield return new WaitForSeconds(.05f);
        }
        playerModel.SetActive(true);
       
    }

    public void die()
    {
        if (!GameManager.instance.isLoadingNextLevel && unTouchAbleTimer <= 0f)
        {
            GameManager.instance.GameOver();
        }
    }


    public void beforeLevelingUp()
    {
        playerModel.transform.Find("dragon").Find("BreatheFire").gameObject.SetActive(false);
    }

    public void afterLevelingUp()
    {
        playerModel.transform.Find("dragon").Find("BreatheFire").gameObject.SetActive(true);
    }

}
