using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]
    GameObject success;

    [SerializeField]
    GameObject fail;

    [SerializeField]
    AudioClip[] sounds;

    public GameObject Success { get => success; set => success = value; }
    public GameObject Fail { get => fail; set => fail = value; }

    public void ShowSuccess()
    {

        gameObject.SetActive(true);
        Success.SetActive(true);
        AudioManager.PlaySound(sounds[0]);
    }

    public void ShowFailure()
    {
        gameObject.SetActive(true);
        Fail.SetActive(true);
        AudioManager.PlaySound(sounds[1]);
    }
}
