using UnityEngine;

public class GameClear : MonoBehaviour
{
    void Start()
    {
        GameObject ui = GameObject.Find("GameClearUI");
        if (ui != null)
        {
            ui.SetActive(true);
            Debug.Log("������ GameClear UI ����");
        }
        else
        {
            Debug.LogWarning("GameClearUI ������Ʈ�� ã�� ����");
        }
    }
}
