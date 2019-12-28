using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Naming : MonoBehaviour
{
    [SerializeField]
    Sprite sprite0;
    [SerializeField]
    Sprite sprite1;
    [SerializeField]
    Sprite sprite2;
    [SerializeField]
    Sprite sprite3;
    [SerializeField]
    Sprite sprite4;
    [SerializeField]
    Sprite sprite5;
    [SerializeField]
    Sprite sprite6;
    [SerializeField]
    Sprite sprite7;

    [SerializeField]
    public AudioClip[] sounds;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Sprite[] sprites = { sprite0, sprite1, sprite2, sprite3, sprite4, sprite5, sprite6, sprite7 };
         SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        int ran = Random.Range(0, 8);
        sr.sprite = sprites[ran];
        AudioManager.PlaySound(sounds[ran]);
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
