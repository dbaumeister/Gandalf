using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField]
    string name = "Npc";

    [SerializeField]
    GameObject bubble;

    public string Name { get => name; }

    float hideTime = 0;

    void Update()
    {
        if(Time.time > hideTime)
        {
            HideSpeechBubble();
        }
    }

    public void ShowSpeechBubble()
    {
        bubble.SetActive(true);
        hideTime = Time.time + 2;
    }

    public void HideSpeechBubble()
    {
        bubble.SetActive(false);
    }
}
