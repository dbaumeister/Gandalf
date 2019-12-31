using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    float nextCall;
    static float DELAY = 5f;

    string lastClip = null;
    float lastClipTime = 0;

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
    
    private void SecretPlaySound(AudioClip clip)
    {
        // only allow one clip every second.
        // and only allow one clip every other second.
        float timeSinceLastClip = Time.time - lastClipTime;
        float allowedTimeSinceLastClip = 0;

        if (lastClip == clip.name)
        {
            allowedTimeSinceLastClip = 2;
        }
        else
        {
            allowedTimeSinceLastClip = 1;
        }

        if (timeSinceLastClip > allowedTimeSinceLastClip)
        {
            // Play the clip
            GameObject o = Instantiate(empty, transform);
            AudioSource source = o.AddComponent<AudioSource>();
            source.PlayOneShot(clip, 0.7f);

            lastClip = clip.name;
            lastClipTime = Time.time;
        }
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
