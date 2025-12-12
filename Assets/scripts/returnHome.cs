using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToHome : MonoBehaviour
{
    public void GoHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
}