using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Planet : MonoBehaviour
{
    public static Planet planetinstance;
    public Material[] planetMaterials;
    public Material[] planetMaterialsActive;
    public GameObject fireUpPrefab;
    private float initMoveSpeed;
    public float moveSpeed = 5f;
    public float maxSpeed = 200f;
    public float speedPerLevel = 5f;

    public GameObject[] planetModel;
    public GameObject pointPerfab;
    public GameObject lastActiveModel;
    public GameObject activeModel;
    public GameObject activeNextModel;

    public Vector3 lastDir;
    public Joystick joystick;
    private float currentMoveSpeed;
    private PlanetModel planetModelScript;

    private void Awake()
    {
        planetinstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        activeModel = GameObject.Instantiate(planetModel[0], transform);
        planetModelScript = activeModel.GetComponentInChildren<PlanetModel>();
        prepareLevel();
        AudioManager.instance.playMusic(0);
    }


    // Update is called once per frame

    
    void Update()
    {
        if (GameManager.instance.isGamePaused()) return;
        if (GameManager.instance.isLoadingNextLevel)
        {
            activeNextModel.transform.position = Vector3.MoveTowards(activeNextModel.transform.position, new Vector3(0, 0, 0), 5.0f * (Time.deltaTime * GameManager.instance.gameTimeScale));
            activeModel.transform.position += ( - activeNextModel.transform.position ).normalized * 3f * (Time.deltaTime * GameManager.instance.gameTimeScale);
            var lookAt = activeNextModel.transform.position;
        
            if ((GameManager.instance.currentLevel + 1) >= planetModel.Length )
            {
                //if last or higher
                
                Player.playerInstance.transform.LookAt(-planetModel[GameManager.instance.randomNextPlanetIndex].transform.position);
            }
            else
            {
                Player.playerInstance.transform.LookAt(-planetModel[GameManager.instance.currentLevel + 1].transform.position);

            }
            return;
        }
        if (!GameManager.instance.isLoadingNextLevel && activeModel.transform.position != Vector3.zero)
        {
            activeModel.transform.position = Vector3.MoveTowards(activeModel.transform.position, new Vector3(0, 0, 0), 5.0f * (Time.deltaTime * GameManager.instance.gameTimeScale));
        }

        var normalizeDir = joystick.Direction.normalized;
        var jVerical = normalizeDir.y;
        var jHorizontal = normalizeDir.x;

     






        if ((jVerical == 0 && jHorizontal == 0) || GameManager.instance.isGameOver)
        {
            if (lastDir.x == 0f && lastDir.y == 0f)
            {
                lastDir.x = 0f;
                lastDir.y = -1f;
            }
            jVerical = lastDir.y;
            jHorizontal = lastDir.x;
            activeModel.transform.Rotate(-jVerical * (Time.deltaTime * GameManager.instance.gameTimeScale) * currentMoveSpeed, jHorizontal * (Time.deltaTime * GameManager.instance.gameTimeScale) * currentMoveSpeed, 0f, Space.World);
        }
        else
        {
            activeModel.transform.Rotate(-jVerical * (Time.deltaTime * GameManager.instance.gameTimeScale) * currentMoveSpeed, jHorizontal * (Time.deltaTime * GameManager.instance.gameTimeScale) * currentMoveSpeed, 0f, Space.World);
            lastDir = normalizeDir;
        }

    }


    public void beforeLevelingUp()
    {

        if ( (GameManager.instance.currentLevel + 1) >= planetModel.Length)
        {
            activeNextModel = GameObject.Instantiate(planetModel[GameManager.instance.randomNextPlanetIndex], transform);
        }
        else
        {
            activeNextModel = GameObject.Instantiate(planetModel[GameManager.instance.currentLevel + 1], transform);
        }

        lastActiveModel = activeModel;
        GameObject.Destroy(lastActiveModel, 5f);
    }


    public void afterLevelingUp()
    {
        if (GameManager.instance.currentLevel != 0)
        {
            lastActiveModel.transform.Find("PlanetItems").Find("Trail").gameObject.SetActive(false);
            activeModel = activeNextModel;
            planetModelScript = activeModel.GetComponentInChildren<PlanetModel>();
        }
        prepareLevel();

    }

    public void prepareLevel()
    {

        currentMoveSpeed = moveSpeed + (speedPerLevel * GameManager.instance.currentLevel);
        if (currentMoveSpeed > maxSpeed) currentMoveSpeed = maxSpeed;

        CoreFace.instance.spawnCoreImage();
        GameManager.instance.setTotalFace(activeModel.transform.Find("PlanetFaces").childCount);
        UIManager.instance.setSpeedLevel((currentMoveSpeed - moveSpeed)/ (maxSpeed - moveSpeed));
        activeModel.transform.Find("PlanetItems").Find("Trail").transform.position = Player.playerInstance.transform.position;
        activeModel.transform.Find("PlanetItems").Find("Trail").gameObject.SetActive(true);
    }

    public void hitPiece(GameObject piece, Vector3 hitPoint)
    {
        planetModelScript.hitPiece(piece, hitPoint);
    }

}
