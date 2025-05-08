using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private static HealthUI instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� �Ѿ ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
        }
    }
}
