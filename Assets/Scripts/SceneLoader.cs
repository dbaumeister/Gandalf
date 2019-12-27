using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField]
    GameObject loadingScreen;

    string sceneName = "Level";

    // Start is called before the first frame update
    void Start()
    {
        LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UnloadScene(sceneName);
            LoadScene(sceneName);
        }
    }

    void UnloadScene(string name)
    {
        loadingScreen.SetActive(true);
        Scene scene = SceneManager.GetSceneByName(name);
        AsyncOperation operation = SceneManager.UnloadSceneAsync(scene);
        operation.completed += SceneUnloadCompleted;
    }

    private void SceneUnloadCompleted(AsyncOperation obj)
    {
        // Nothing
    }

    void LoadScene(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        operation.completed += SceneLoadCompleted;
    }

    private void SceneLoadCompleted(AsyncOperation obj)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
        loadingScreen.SetActive(false);
    }
}
