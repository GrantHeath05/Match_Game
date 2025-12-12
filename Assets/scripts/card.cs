using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public Image valueImage;

    public Sprite backOfCard;
    public Sprite frontOfCardBackground;

    public Sprite circle;
    public Sprite star;
    public Sprite square;
    public Sprite diamond;
    public Sprite triangle;

    private bool isFlipped = false;
    private string cardValue;

    private GridManager gridManager;

    public void cardSetup(string value, GridManager gridManager)
    {
        cardValue = value;
        this.gridManager = gridManager;

        switch (value.ToLower())
        {
            case "circle": valueImage.sprite = circle; break;
            case "star": valueImage.sprite = star; break;
            case "square": valueImage.sprite = square; break;
            case "diamond": valueImage.sprite = diamond; break;
            case "triangle": valueImage.sprite = triangle; break;
            default: break;
        }

        valueImage.enabled = false;
        GetComponent<Image>().sprite = backOfCard;
    }

    public void OnClick()
    {
        if (!isFlipped && gridManager.CanReveal())
        {
            FlipUp();
            gridManager.CardRevealed(this);
        }
    }

    public void FlipUp()
    {
        GetComponent<Image>().sprite = frontOfCardBackground;
        valueImage.enabled = true;
        isFlipped = true;
    }

    public void FlipDown()
    {
        GetComponent<Image>().sprite = backOfCard;
        valueImage.enabled = false;
        isFlipped = false;
    }

    public string GetCardValue()
    {
        return cardValue;
    }

    public void RemoveCard()
    {
        GetComponent<Button>().interactable = false;
        valueImage.enabled = false;
        GetComponent<Image>().enabled = false;
    }
}