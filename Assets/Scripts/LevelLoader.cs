using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

   
    public Slider loader;
    public GameObject loadingScreen;

    private float fakeLerpValue = 0;
    private bool doLerp;

    private void Awake()
    {
       
        instance = this;
    }

    private void Update()
    {
        if (doLerp)
        {
            loader.value = Mathf.Lerp(loader.value, 1f, fakeLerpValue * Time.deltaTime);
            if (loader.value >= 1)
            {
                doLerp = false;
                loadingScreen.SetActive(false);
            }
        }
    }

    public void LoadLevel(int index)
    {
        StartCoroutine(asyncLoad(index));
    }

    IEnumerator asyncLoad(int index)
    {
        loadingScreen.SetActive(true);
        AsyncOperation opration = SceneManager.LoadSceneAsync(index);
        while (!opration.isDone || opration.progress < 1)
        {

            loader.value = opration.progress;
            yield return null;
        }
        loadingScreen.SetActive(false);
    }
    public void fakeLoading(float _number)
    {
        loadingScreen.SetActive(true);
        loader.value = 0;
        fakeLerpValue = _number;
        doLerp = true;
    }

    public void stopFakeLoader()
    {

        StartCoroutine(stopFakeLoadingCoRot());
    }

    IEnumerator stopFakeLoadingCoRot()
    {
        yield return new WaitForSeconds(1f);
        doLerp = false;
        loader.value = 0;
        loadingScreen.SetActive(false);
        fakeLerpValue = 0;
    }
}
