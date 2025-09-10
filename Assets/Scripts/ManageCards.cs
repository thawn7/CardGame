using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageCards : MonoBehaviour
{
    public GameObject card;
    private bool firstCardSelected, secondCardSelected;
    private GameObject card1, card2;
    private string rowForCard1, rowForCard2;
    bool timerHasElapsed, timerHasStarted;
    float timer;
    int nbMatch = 0;

    // Start is called before the first frame update
    void Start()
    {
        DisplayCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerHasStarted)
        {
            timer += Time.deltaTime;
            print(timer);
            if(timer >= 1)
            {
                timerHasElapsed = true;
                timerHasStarted = false;
                if(card1.tag == card2.tag)
                {
                    Destroy(card1);
                    Destroy(card2);
                    nbMatch++;
                    if(nbMatch == 10)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
                else
                {
                    card1.GetComponent<Tile>().HideCard();
                    card2.GetComponent<Tile>().HideCard();  
                }
                firstCardSelected = false;
                secondCardSelected = false;
                card1 = null;
                card2 = null;
                rowForCard1 = "";
                rowForCard2 = "";
                timer = 0;
            }
        }
    }

    public void DisplayCards()
    {
        //Instantiate(card, new Vector3(0,0,0),Quaternion.identity);
        //AddACard(0);
        int[] shuffledArray = createShuffledArray();
        int[] shuffledArray2 = createShuffledArray();
        for(int i = 0; i<10;  i++)
        {
            //AddACard(i);
            AddACard(0,i, shuffledArray[i]);
            AddACard(1,i, shuffledArray2[i]);
        }
    }

    void AddACard(int row, int rank, int value)
    {
        float cardOriginalScale = card.transform.localScale.x;
        float scaleFactor = (500 * cardOriginalScale) / 100.0f;
        //GameObject c = (GameObject)Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject cen = GameObject.Find("centerOfScreen");
        //Vector3 newPosition = new Vector3(cen.transform.position.x + ((rank - 10 / 2) * 3), cen.transform.position.y, cen.transform.position.z);
        //Vector3 newPosition = new Vector3(cen.transform.position.x + ((rank - 10 / 2) * scaleFactor), cen.transform.position.y, cen.transform.position.z);
        float yScaleFactor = (725 * cardOriginalScale) / 100.0f;
        Vector3 newPosition = new Vector3(cen.transform.position.x + ((rank - 10 / 2) * scaleFactor), cen.transform.position.y + ((row-2/2)*yScaleFactor), cen.transform.position.z);

        //GameObject c = (GameObject)Instantiate(card, new Vector3(rank*3.0f, 0, 0), Quaternion.identity);
        GameObject c = (GameObject)Instantiate(card, newPosition, Quaternion.identity);
        c.tag = "" + (value+1); // converts rank to string
        c.name = "" + row +"_" + value;
        string nameOfCard = "";
        string cardNumber = "";
        if (value == 0)
        {
            cardNumber = "ace";
        }
        else
        {
            cardNumber = "" + (value + 1);
        }
        nameOfCard = cardNumber + "_of_hearts";
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nameOfCard));
        print("S1:" + s1);
        //GameObject.Find("" + value).GetComponent<Tile>().SetOriginalSprite(s1);
        GameObject.Find("" + row + "_" + value).GetComponent<Tile>().SetOriginalSprite(s1);
    }

    public int[] createShuffledArray()
    {
        int[] newArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int tmp;
        for (int t = 0; t < 10; t++)
        {
            tmp = newArray[t];
            int r = Random.Range(t, 10);
            newArray[t] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public void cardSelected(GameObject card)
    {
        if(!firstCardSelected)
        {
            firstCardSelected = true;
            card1 = card;
            card1.GetComponent<Tile>().RevealCard();
        }
        else if(firstCardSelected&& !secondCardSelected)
        {
            string row = card.name.Substring(0, 1);
            rowForCard2 = row;
            if (rowForCard2 != rowForCard1)
            {
                secondCardSelected = true;
                card2 = card;
                card2.GetComponent<Tile>().RevealCard();
                CheckCards();
            }

        }
    }

    public void CheckCards()
    {
        RunTimer();
    }

    public void RunTimer()
    {
        timerHasElapsed = false;
        timerHasStarted = true;
    }
}

