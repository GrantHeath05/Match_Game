using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    [Header("Grid Setup")]
    public GameObject cardPrefab;
    public Transform gridParent;
    public int rows;
    public int cols;

    [Header("Win Screen UI")]
    public GameObject winScreen;
    public TMP_Text winText;

    private List<string> cardValues = new List<string>();
    private List<card> revealedCards = new List<card>();
    private bool canReveal = true;

    private int guessCount = 0;
    private int totalPairs;
    private int matchedPairs = 0;

    void Start()
    {
        rows = PlayerPrefs.GetInt("rows", 4);
        cols = PlayerPrefs.GetInt("cols", 4);
        totalPairs = (rows * cols) / 2;

        CreateGrid(rows, cols);

        winScreen.SetActive(false);
    }

    void CreateGrid(int rows, int cols)
    {
        GridLayoutGroup grid = gridParent.GetComponent<GridLayoutGroup>();
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = cols;

        int totalCards = rows * cols;
        GenerateCardValues(totalCards);

        for (int i = 0; i < totalCards; i++)
        {
            GameObject cardGO = Instantiate(cardPrefab, gridParent);
            card cardScript = cardGO.GetComponent<card>();
            cardScript.cardSetup(cardValues[i], this);
        }
    }

    void GenerateCardValues(int totalCards)
    {
        int pairCount = totalCards / 2;
        string[] baseValues = { "circle", "star", "square", "diamond", "triangle" };

        cardValues.Clear();

        for (int i = 0; i < pairCount; i++)
        {
            string value = baseValues[i % baseValues.Length];
            cardValues.Add(value);
            cardValues.Add(value);
        }

        for (int i = 0; i < cardValues.Count; i++)
        {
            int rand = Random.Range(0, cardValues.Count);
            (cardValues[i], cardValues[rand]) = (cardValues[rand], cardValues[i]);
        }
    }

    public bool CanReveal()
    {
        return canReveal && revealedCards.Count < 2;
    }

    public void CardRevealed(card revealedCard)
    {
        revealedCards.Add(revealedCard);

        if (revealedCards.Count == 2)
        {
            canReveal = false;
            guessCount++;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);

        if (revealedCards[0].GetCardValue() == revealedCards[1].GetCardValue())
        {
            revealedCards[0].RemoveCard();
            revealedCards[1].RemoveCard();
            matchedPairs++;

            if (matchedPairs == totalPairs)
            {
                ShowWinScreen();
            }
        }
        else
        {
            revealedCards[0].FlipDown();
            revealedCards[1].FlipDown();
        }

        revealedCards.Clear();
        canReveal = true;
    }

    private void ShowWinScreen()
    {
        winText.text = $"You won in {guessCount} guesses!";
        winScreen.SetActive(true);
    }

    public void ReturnToHome()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}