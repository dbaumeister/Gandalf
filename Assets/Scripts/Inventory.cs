using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Attributes))]
public class Inventory : MonoBehaviour
{
    Attributes attributes;

    Attributes defaultAttributes;

    IList<Item> items;

    // Start is called before the first frame update
    void Start()
    {
        attributes = GetComponent<Attributes>();
        defaultAttributes = attributes.GetCopy();
        items = new List<Item>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Item item = collision.collider.GetComponent<Item>();
        if (item)
        {
            items.Add(item);
            if (item.CompareTag("Projectile"))
            {
                ProjectilePrefab prefab = 
            }
            Destroy(collision.collider.gameObject);
        }
    }

    private void Update()
    {
        attributes.Overwrite(defaultAttributes);
        foreach(Item item in items)
        {
            item.Apply(attributes);
        }
    }
}
