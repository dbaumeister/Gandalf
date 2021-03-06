﻿using System.Collections;
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

    int maxHearts;

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
        GroupValues values = GameObject.FindGameObjectWithTag("GroupValues").GetComponent<GroupValues>();

        ModifyLife modifier = collision.collider.GetComponent<ModifyLife>();
        bool wasFull = false;
        if (modifier)
        {
            //pickung up the item does not increase the number of hearts higher than allowed
            if (values.getHearts() < values.getMaxHearts())
            {
                GameObject.FindGameObjectWithTag("GroupValues").GetComponent<GroupValues>().addHearts(Mathf.Min(modifier.lifeModificator, values.getMaxHearts() - values.getHearts()));
            }
            else
            {
                wasFull = true;
            }
        }

        Item item = collision.collider.GetComponent<Item>();
        if (item)
        {
            if (item.CompareTag("Projectile"))
            {
                ModifyProjectile mod = item.GetComponent<ModifyProjectile>();
                weapon.projectilePrefab = mod.projectile;
            }
            items.Add(item);
        }


        CoffeeCollectible coffee = collision.collider.GetComponent<CoffeeCollectible>();
        if (coffee)
        {
            coffee.StartingTime = Time.time;
        }

        BoozeCollectible booze = collision.collider.GetComponent<BoozeCollectible>();
        if (booze)
        {
            booze.StartingTime = Time.time;
        }

        MagicalBambooCollectable bamboo = collision.collider.GetComponent<MagicalBambooCollectable>();
        if (bamboo)
        {
            bamboo.PickUpTime = Time.time;
            bamboo.NextTime = bamboo.PickUpTime + bamboo.TimeStep;
        }

        if (item || modifier || coffee || booze)
        {
            if(modifier && wasFull)
            {
                // Do not destroy the game object if our hearts are full
            }
            else
            {

                if (item.Sound)
                {
                    AudioManager.PlaySound(item.Sound);
                }

                Destroy(collision.collider.gameObject);
            }
        }
    }

    private void Update()
    {
        attributes.Overwrite(defaultAttributes);
        foreach(Item item in items)
        {
            attributes = item.Apply(attributes);
        }
    }
}
