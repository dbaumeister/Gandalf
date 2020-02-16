using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpeechBubble : MonoBehaviour
{
    [SerializeField]
    GameObject bubble;

    public void ShowSpeechBubble()
    {
        bubble.SetActive(true);
    }

    public void HideSpeechBubble()
    {
        bubble.SetActive(false);
    }
}
