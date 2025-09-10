using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool tileRevealed = false;
    public Sprite originalSprite;
    public Sprite hiddenSprite;

    // Start is called before the first frame update
    void Start()
    {
        HideCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        print("you pressed on tile");
        /*        if(tileRevealed)
                {
                    HideCard();
                }
                else
                {
                    RevealCard();
                }*/
        GameObject.Find("gameManager").GetComponent<ManageCards>().cardSelected(gameObject);

    }

    public void HideCard()
    {
        GetComponent<SpriteRenderer>().sprite = hiddenSprite;
        tileRevealed = false;
    }

    public void RevealCard()
    {
        GetComponent<SpriteRenderer>().sprite = originalSprite;
        tileRevealed = true;
    }

    public void SetOriginalSprite(Sprite newSprite)
    {
        originalSprite = newSprite;
    }
}
