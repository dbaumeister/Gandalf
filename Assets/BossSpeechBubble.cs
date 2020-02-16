using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpeechBubble : MonoBehaviour
{
    [SerializeField]
    GameObject bubble;

    float hideTime;

    void Update()
    {
        if(Time.time > hideTime)
        {
            bubble.SetActive(false);
        }
    }

    public void ShowSpeechBubble(float seconds)
    {
        hideTime = Time.time + seconds;
        bubble.SetActive(true);
    }
}
