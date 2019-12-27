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
    Sprite left;

    [SerializeField]
    Sprite right;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }


    public void Change(Vector2 lookDirection)
    {
        if (Mathf.Abs(lookDirection.x) > Mathf.Abs(lookDirection.y))
        {
            if(lookDirection.x > 0)
            {
                SetSprite(right);
            }
            else
            {
                SetSprite(left);
            }
        }
        else
        {
            if (lookDirection.y > 0)
            {
                SetSprite(up);
            }
            else
            {
                SetSprite(down);
            }
        }
    }
}
