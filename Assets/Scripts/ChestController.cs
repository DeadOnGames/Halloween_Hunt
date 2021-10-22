using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestController : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite openSprite, closedSprite;

    [SerializeField]
    private bool isOpen;

    private int treatID;
    private int trickID;
    private Item item;

    public AudioClip treatClip;
    public AudioClip trickClip;

    //Tidy this up later after the game jam
    public Item item_1; //CandyCorn
    public Item item_2; //Candy
    public Item item_3; //Lollipop
    public Item item_4; //Ghost
    public Item item_5; //Devil
    public Item item_6; //PumpkinMan

    private Animator anim;

    Item[] treats = new Item[3];
    Item[] tricks = new Item[3];
    Item[] allItems = new Item[6];

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void interact()
    {
        if (isOpen)
        {
            stopInteract();
        } else
        {
            isOpen = true;
            

            loadArrays();

            PlayerController player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>();
            item = pickTrickOrTreat();
            player.setCandyCount(item.pointsValue);
            playChestAnimation(item.id);
            player.PlaySound(getTrickOrTreatClip(item));

            spriteRenderer.sprite = openSprite;
        }
    }
    
    public void stopInteract()
    {
        isOpen = true;
        spriteRenderer.sprite = openSprite;
    }

    public Item pickTrickOrTreat()
    {

        bool Boolean = (UnityEngine.Random.value > 0.5f);
        if (Boolean == true)
        {
            //Treat
            treatID = (UnityEngine.Random.Range(0, (treats.Length)-1));
            return treats[treatID];

        } else
        {
            //Trick
            trickID = (UnityEngine.Random.Range(0, (tricks.Length)-1));
            return tricks[trickID];
        }
    }

    public void loadArrays()
    {
        //Initialise arrays with items - replace with a more cleaner function after game jam
        treats[0] = item_1;
        treats[1] = item_2;
        treats[2] = item_3;

        tricks[0] = item_4;
        tricks[1] = item_5;
        tricks[2] = item_6;

        allItems[0] = item_1;
        allItems[1] = item_2;
        allItems[2] = item_3;
        allItems[3] = item_4;
        allItems[4] = item_5;
        allItems[5] = item_6;
    }

    public int findItemIndex(Item currentItem)
    {
        foreach (Item i in allItems){
            if (currentItem.name.Equals(i.name))
            {
                int index = Array.IndexOf(allItems, i);
                Debug.Log("Index found: " + index);
                return index;
            }

        }
        Debug.Log("Index not found");
        return -1;
    }

    public void playChestAnimation(int id)
    {
        switch (id)
        {
            case 1:
                anim.SetTrigger("CandyCornTrigger");
                break;
            case 2:
                anim.SetTrigger("SweetyTrigger");
                break;
            case 3:
                anim.SetTrigger("DevilTrigger");
                break;
            case 4:
                anim.SetTrigger("GhostTrigger");
                break;
            case 5:
                anim.SetTrigger("LollipopTrigger");
                break;
            case 6:
                anim.SetTrigger("PumpkinManTrigger");
                break;

        }
    }

    public AudioClip getTrickOrTreatClip(Item item)
    {
        if (item.isTreat)
        {
            return treatClip;
        } else
        {
            return trickClip;
        }
    }
}
