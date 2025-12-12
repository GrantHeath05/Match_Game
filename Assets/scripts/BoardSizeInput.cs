using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BoardSizeInput : MonoBehaviour
{
    public TMP_InputField rowInput;
    public TMP_InputField colInput;

    public TMP_Text rowFeedback;
    public TMP_Text colFeedback;

    public void OnPress()
    {
        if (!validateInput(rowInput, rowFeedback) || !validateInput(colInput, colFeedback))
        {
            Debug.Log("One or both inputs are bad");
            return;
        }

        int rowValue = int.Parse(rowInput.text.Trim());
        int colValue = int.Parse(colInput.text.Trim());
        int totalCards = rowValue * colValue;

        if (totalCards % 2 != 0)
        {
            rowFeedback.text = "Total cards must be even for matching";
            colFeedback.text = "Try adjusting rows or columns";
            makeTextRed(rowFeedback);
            makeTextRed(colFeedback);
            return;
        }

        if (rowValue > 6 || colValue > 10)
        {
            rowFeedback.text = "Max grid size is 6 x 10";
            colFeedback.text = "Try smaller values";
            makeTextRed(rowFeedback);
            makeTextRed(colFeedback);
            return;
        }


        PlayerPrefs.SetInt("rows", rowValue);
        PlayerPrefs.SetInt("cols", colValue);
        SceneManager.LoadScene("MatchGame");
    }

    private bool validateInput(TMP_InputField input, TMP_Text feedback)
    {
        string value = input.text.Trim();
        makeTextRed(feedback);

        if (string.IsNullOrEmpty(value))
        {
            feedback.text = "Inputfield is blank";
            return false;
        }
        if (!int.TryParse(value, out int number))
        {
            feedback.text = "Input must be a whole positive number";
            return false;
        }
        if (number <= 0)
        {
            feedback.text = "Input must be a whole number";
            return false;
        }
        

        feedback.text = "";
        return true;
    }

    public void makeTextRed(TMP_Text feedback)
    {
        feedback.color = Color.red;
    }
}