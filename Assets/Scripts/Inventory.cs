using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Attributes))]
[RequireComponent(typeof(Weapon))]
public class Inventory : MonoBehaviour
{
    Attributes attributes;

    Attributes defaultAttributes;

    IList<Item> items;

    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        attributes = GetComponent<Attributes>();
        weapon = GetComponent<Weapon>();
        defaultAttributes = attributes.GetCopy();
        items = new List<Item>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Item item = collision.collider.GetComponent<Item>();
        if (item)
        {
            if (item.CompareTag("Projectile"))
            {
                ModifyProjectile mod = item.GetComponent<ModifyProjectile>();
                weapon.projectilePrefab = mod.projectile;
            }

            ModifyLife modifier = item.GetComponent<ModifyLife>();
            if(modifier != null)
            {
                GameObject.FindGameObjectWithTag("GroupValues").GetComponent<GroupValues>().addHearts(modifier.lifeModificator);

            }
            items.Add(item);
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
