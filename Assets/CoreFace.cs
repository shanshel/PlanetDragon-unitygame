using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoreFace : MonoBehaviour
{
    public static CoreFace instance;
    public List<GameObject> coreImages = new List<GameObject>();
    public AudioSource[] sfx;
    private GameObject currentCoreImage;
    private int playedIndex;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }
   
    public void spawnCoreImage()
    {
        var index = GameManager.instance.currentLevel;
        if (GameManager.instance.currentLevel >= coreImages.Count)
        {
            index = Random.Range(0, coreImages.Count - 1);
        }
        
        var objToSpawn = coreImages[index];
        var spawned = Instantiate(objToSpawn, Planet.planetinstance.activeModel.transform);
        SpeedSlider.instance.iconImage.sprite = spawned.GetComponentInChildren<SpriteRenderer>().sprite;
        StartCoroutine(setCurrentCoreFace(spawned, index));

    }
    private IEnumerator setCurrentCoreFace(GameObject initCoreFace, int lastIndex)
    {
        yield return new WaitForSeconds(2f);
        currentCoreImage = initCoreFace;
        currentCoreImage.transform.DOPunchScale(new Vector3(-.5f, -.5f), .7f, 3).SetLoops(-1, LoopType.Incremental);
        playedIndex = lastIndex;
        //currentCoreImage.transform.DOLocalRotate(new Vector3(0f,0f,0f), .1f).SetLoops(-1, LoopType.Incremental);

    }

    public void winAnimate()
    {
     
        sfx[playedIndex].Play();
        currentCoreImage.transform.DOComplete();
        currentCoreImage.transform.rotation = Quaternion.Euler(Planet.planetinstance.activeNextModel.transform.position);
        currentCoreImage.transform.DOPunchScale(new Vector3(1.7f, 1.7f), .7f, 1);
    }

}
