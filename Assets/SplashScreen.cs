using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]
    GameObject success;

    [SerializeField]
    GameObject fail;

    public GameObject Success { get => success; set => success = value; }
    public GameObject Fail { get => fail; set => fail = value; }

    public void ShowSuccess()
    {
        gameObject.SetActive(true);
        Success.SetActive(true);
    }

    public void ShowFailure()
    {
        gameObject.SetActive(true);
        Fail.SetActive(true);
    }
}
