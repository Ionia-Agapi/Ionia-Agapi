using UnityEngine;
using UnityEngine.SceneManagement;

public class Player: MonoBehaviour
{
    private static Player instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject startPoint = GameObject.Find("PlayerStartPoint");
        if (startPoint != null)
        {
            transform.position = startPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("PlayerStartPoint 오브젝트가 이 씬에 없습니다.");
        }
    }
}