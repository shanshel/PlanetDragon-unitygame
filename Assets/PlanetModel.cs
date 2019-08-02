using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlanetModel : MonoBehaviour
{
    
    public Material nMaterial, activeMaterial;
    private List<MeshRenderer> meshInTrans = new List<MeshRenderer>();
    private GameObject[] faces;
    public GameObject activeFacePrefab;
    public GameObject spikePrefab;
    public int maxTallLevel = 10;
    public int numberOfSpike = 5;
    /// <summary>
    ///  val * currentLevel
    /// </summary>
    public float playerTrailDuration = 20f;
    public float bodyPartLifetimeOffset = .2f;
    // Start is called before the first frame update


    void Start()
    {


        activeMaterial = Planet.planetinstance.planetMaterialsActive[Random.Range(0, Planet.planetinstance.planetMaterialsActive.Length)];
        nMaterial = Planet.planetinstance.planetMaterials[Random.Range(0, Planet.planetinstance.planetMaterials.Length)];
  

        var chCount = transform.Find("PlanetFaces").childCount;
        for (var x = 0; x < chCount; x++)
        {
            transform.Find("PlanetFaces").GetChild(x).GetComponent<MeshRenderer>().material = nMaterial;
        }
        setPlayerTall();
        StartCoroutine(spawnSpike(numberOfSpike));


    }

    // Update is called once per frame
    void Update()
    {}

    private void setPlayerTall()
    {
        ParticleSystem partical = transform.Find("PlanetItems").Find("Trail").Find("SnakeSkin").GetComponent<ParticleSystem>();
        var main = partical.main;
        var tallTimes = GameManager.instance.currentLevel;
        if (tallTimes > maxTallLevel)
        {
            tallTimes = maxTallLevel;
        }

        var finalDuration = ((tallTimes + 1 )/ 1.5f) * playerTrailDuration;
    
        main.startLifetime = new ParticleSystem.MinMaxCurve(main.startLifetime.constantMin, finalDuration);
        PlayerBody.dieTimer =  (finalDuration / 15 ) - bodyPartLifetimeOffset;
    }

    public void hitPiece(GameObject piece, Vector3 hitPoint)
    {
        var renderer = piece.GetComponent<MeshRenderer>();

        string hitMaterial = renderer.material.name.Replace("(Instance)", "").Trim();
        string compareToMaterial = nMaterial.name.Trim();


        if (!meshInTrans.Contains(renderer) && hitMaterial.Equals(compareToMaterial))
        {
            meshInTrans.Add(renderer);
            renderer.material = activeMaterial;



             GameManager.instance.increaseFilledFaces();
             CameraShake.instance.shakeCamera();
             AudioManager.instance.playSFX(0);
             GameManager.instance.addScore();
             spawnActiveFaceEffect(hitPoint);
         }
     }

     private void spawnActiveFaceEffect(Vector3 hitPoint)
     {
        Instantiate(Planet.planetinstance.fireUpPrefab, hitPoint, Quaternion.identity, transform);
        
     }

    private Vector3[] spawnedSpikes;

    public IEnumerator spawnSpike(int count)
    {
        
        spawnedSpikes = new Vector3[count];
        var i = 0;
        
        while (i < count)
        {
            var rand = Random.onUnitSphere;
            bool canSpawn = true;
            for (var x =0; x < spawnedSpikes.Length; x++)
            {
               if (Vector3.Distance(rand, spawnedSpikes[x]) < .5f)
                {
                    canSpawn = false;
                }
            }
            if (canSpawn)
            {
                Vector3 spawnPosition = rand * (.5f + 1.3f * 0.5f) + transform.position;
                GameObject newCharacter = Instantiate(spikePrefab, spawnPosition, Quaternion.identity, transform);
                newCharacter.transform.LookAt(transform.position);
                spawnedSpikes[i] = rand;
                i++;
            }
        }
        yield return null;

    }

    private void preparePlanet()
    {
        GameManager.instance.setTotalFace(transform.Find("PlanetFaces").childCount);
        transform.Find("PlanetItems").Find("Trail").gameObject.SetActive(true);
        

    }
 }
