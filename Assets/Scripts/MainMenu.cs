using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnPressGameStart()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void OnPressExit()
    {
        Application.Quit();
    }
}
