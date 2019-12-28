using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Appearance : MonoBehaviour
{
    [SerializeField]
    Sprite up;

    [SerializeField]
    Sprite down;

    [SerializeField]
    Sprite[] right;

    [SerializeField]
    Sprite att_up;

    [SerializeField]
    Sprite att_down;

    [SerializeField]
    Sprite[] att_right;

    [SerializeField]
    Sprite hurtSprite;

    int curHor, curVert;
    float nextSpriteHor, nextSpriteVert;
    float offHor, offVert;

    public bool attacking;
    bool hurt;
    float hurtTime;

    [SerializeField]
    AudioClip ouch;

    // Start is called before the first frame update
    void Start()
    {
        curHor = 0;
        curVert = 0;
        nextSpriteVert = 0f;
        nextSpriteHor = 0f;
        offVert = 0.4f;
        offHor = 0.2f;

        hurtTime = float.PositiveInfinity;
    }

    void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void Hurt()
    {
        AudioManager.PlaySound(ouch);
        hurt = true;
        hurtTime = Time.time + 2f;

    }


    public void Change(Vector2 lookDirection)
    {
        transform.localScale = new Vector3(1, 1, 1);
        if (hurt)
        {
            SetSprite(hurtSprite);
            if (Time.time > hurtTime)
            {
                hurt = false;
            }
        }
        else if (Mathf.Abs(lookDirection.x) > Mathf.Abs(lookDirection.y))
        {
            if(lookDirection.x > 0)
            {
                if(Time.time > nextSpriteHor)
                {
                    nextSpriteHor = Time.time + offHor;
                    curHor = (curHor + 1) % 3;
                    if (attacking){
                        SetSprite(att_right[curHor]);
                    }
                    else
                    {
                        SetSprite(right[curHor]);
                    }
                    
                }
                
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                if (Time.time > nextSpriteHor)
                {
                    nextSpriteHor = Time.time + offHor;
                    curHor = (curHor + 1) % 3;
                    if (attacking)
                    {
                        SetSprite(att_right[curHor]);
                    }
                    else
                    {
                        SetSprite(right[curHor]);
                    }
                }
            }
        }
        else
        {
            if (lookDirection.y > 0)
            {
                
                if (Time.time > nextSpriteVert)
                {
                    nextSpriteVert = Time.time + offVert;
                    curVert = (curVert + 1) % 2;                                       
                }
                transform.localScale = curVert == 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
                if (attacking)
                {
                    SetSprite(att_up);
                }
                else
                {
                    SetSprite(up);
                }
            }
            else
            {
                if (Time.time > nextSpriteVert)
                {
                    nextSpriteVert = Time.time + offVert;
                    curVert = (curVert + 1) % 2;
                }
                transform.localScale = curVert == 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
                if (attacking)
                {
                    SetSprite(att_down);
                }
                else
                {
                    SetSprite(down);
                }
            }
        }
    }
}
