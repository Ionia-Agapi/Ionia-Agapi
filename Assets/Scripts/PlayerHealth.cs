using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] hearts; // ��Ʈ ������Ʈ �迭
    private int health;

    void Start()
    {
        health = hearts.Length;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spike"))  // �±� ��
        {
            Debug.Log("���ÿ� ����!");
            TakeDamage(1);  // ������ ó��
        }
    }



    void TakeDamage(int amount)
    {
        if (health <= 0) return;

        health -= amount;

        if (health >= 0 && health < hearts.Length)
        {
            hearts[health].SetActive(false); // ��Ʈ ��Ȱ��ȭ
        }

        if (health <= 0)
        {
            Debug.Log("���� ����");
            // ���� ���� ó��
        }
    }
}
