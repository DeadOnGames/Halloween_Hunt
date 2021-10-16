using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Tidy this up later after the game jam
    public Item item_1;
    public Item item_2;
    public Item item_3;
    public Item item_4;
    public Item item_5;
    public Item item_6;

    Item[] treats = new Item[3];
    Item[] tricks = new Item[3];

    public void interact()
    {
        if (isOpen)
        {
            stopInteract();
        } else
        {
            isOpen = true;
            spriteRenderer.sprite = openSprite;
            loadArrays();
            //Debug.Log("Result: " + pickTrickOrTreat().pointsValue);

            //Animation based on result
            //Play corresponding sound

            PlayerController player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>();
            player.setCandyCount(pickTrickOrTreat().pointsValue);
        }
    }
    
    public void stopInteract()
    {
        isOpen = true;
        spriteRenderer.sprite = openSprite;
    }

    public Item pickTrickOrTreat()
    {

        bool Boolean = (Random.value > 0.5f);
        if (Boolean == true)
        {
            //Treat
            treatID = (Random.Range(0, (treats.Length)-1));
            return treats[treatID];

        } else
        {
            //Trick
            trickID = (Random.Range(0, (tricks.Length)-1));
            return tricks[trickID];
        }
    }

    public void loadArrays()
    {
        treats[0] = item_1;
        treats[1] = item_2;
        treats[2] = item_3;
        tricks[0] = item_4;
        tricks[1] = item_5;
        tricks[2] = item_6;

    }


}
