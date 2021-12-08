using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ASyncSceneLoader : MonoBehaviourSingleton<ASyncSceneLoader>
{
    public float loadingProgress { get { return asyncLoad.progress; } }
    public bool sceneIsLoading { get { return !loadIsDone; } }
    [SerializeField] float minLoadSeconds;
    AsyncOperation asyncLoad;
    string sceneLoading;
    bool loadIsDone;

    //Unity Events
    public void StartLoad(string sceneToLoad)
    {
        sceneLoading = sceneToLoad;
        SceneManager.LoadScene("Load Scene");
        StartCoroutine(LoadAsyncScene());
    }

    //Methods
    IEnumerator LoadAsyncScene()
    {
        //The Application loads the Scene in the background as the current Scene runs.
        //asyncLoad = SceneManager.LoadSceneAsync(sceneLoading, LoadSceneMode.Additive);
        //asyncLoad.allowSceneActivation = false;

        //Set timer
        float timer = minLoadSeconds;

        // Wait until the asynchronous scene fully loads
        do
        {
            timer -= Time.deltaTime;
            yield return null;
        } while (/*asyncLoad.progress < 0.9f || */timer > 0);


        //asyncLoad.allowSceneActivation = true;
        //SceneManager.UnloadSceneAsync("Load Scene");
        SceneManager.LoadScene(sceneLoading);
        yield break;
    }
}