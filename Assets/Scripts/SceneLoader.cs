using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UnloadScene("SampleScene");
            LoadScene("SampleScene");
        }
    }

    void UnloadScene(string name)
    {
        Scene scene = SceneManager.GetSceneByName(name);
        AsyncOperation operation = SceneManager.UnloadSceneAsync(scene);
        operation.completed += SceneUnloadCompleted;
    }

    private void SceneUnloadCompleted(AsyncOperation obj)
    {
        Debug.Log("Scene Unloaded");
    }

    void LoadScene(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        operation.completed += SceneLoadCompleted;
    }

    private void SceneLoadCompleted(AsyncOperation obj)
    {
        Debug.Log("Scene Loaded");
    }
}
