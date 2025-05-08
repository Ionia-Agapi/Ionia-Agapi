using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowerItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("LoadNextScene", 1.5f);
        }
    }
    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadSceneAsync(nextSceneIndex);
    }
}