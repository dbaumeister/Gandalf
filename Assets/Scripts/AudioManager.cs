using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    float nextCall;
    static float DELAY = 5f;

    GameObject empty;

    // Start is called before the first frame update
    void Start()
    {
        nextCall = 0f;
        empty = new GameObject();
    }

    public static void PlaySound(AudioClip clip)
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SecretPlaySound(clip);
    }

    public void SecretPlaySound(AudioClip clip)
    {       
        GameObject o = Instantiate(empty, transform);       
        AudioSource source = o.AddComponent<AudioSource>();
        source.PlayOneShot(clip, 0.7f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextCall)
            
        {
            nextCall = Time.time + DELAY;
            for (int i = 0; i < transform.childCount; ++i)
            {
                GameObject o = transform.GetChild(i).gameObject;
                if (!o.GetComponent<AudioSource>().isPlaying)
                {
                    Destroy(o);
                }
            }

        }
          
    }
}
