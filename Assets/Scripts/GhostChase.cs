using UnityEngine;

public class GhostChase : MonoBehaviour
{
    [Header("참조")]
    public Transform player;
    public SpriteRenderer sr;

    public ParticleSystem auraBlack;
    public ParticleSystem auraBlue;
    public ParticleSystem ghostSmoke;

    [Header("등장 및 추적 조건")]
    public float appearHeight = 28f;
    public float chaseMinY = 24f;
    public float moveSpeed = 5f;

    [Header("페이드아웃 조건")]
    public float fadeOutSpeed = 0.5f;
    public float smokeDelay = 0.7f;
    public float requiredHeightToEscape = 43.0f;

    [HideInInspector] public bool safeZoneReached = false;

    private Rigidbody2D rb;
    private bool hasAppeared = false;
    private bool smokePlayed = false;
    private float fadeTimer = 0f;
    private Color originalColor;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (sr != null)
            originalColor = sr.color;
    }

    void Start()
    {
        if (sr != null)
        {
            sr.enabled = false;
            sr.color = originalColor;
        }
        
        if (auraBlack != null) auraBlack.Stop();
        if (auraBlue != null) auraBlue.Stop();
        if (ghostSmoke != null) ghostSmoke.Stop();

        rb.linearVelocity = Vector2.zero;
    }

    void FixedUpdate()
    {
        if (!hasAppeared && player.position.y > appearHeight)
        {
            hasAppeared = true;
            
            if (sr != null)
            {
                sr.enabled = true;
                sr.color = originalColor;
            }
            
            if (auraBlack != null) auraBlack.Play();
            if (auraBlue != null) auraBlue.Play();

            Debug.Log("👻 유령 등장! (스프라이트 + aura 파티클 재생)");
        }

        if (!hasAppeared)
            return;
        
        if (safeZoneReached)
        {
            FadeOutAndStop();
            return;
        }
        
        if (player.position.y < chaseMinY)
            return;

        Vector2 dir = ((Vector2)player.position - rb.position).normalized;
        Vector2 targetPos = rb.position + dir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);
    }

    void FadeOutAndStop()
    {
        if (sr == null) return;
        
        fadeTimer += Time.fixedDeltaTime;
        
        if (!smokePlayed && fadeTimer >= smokeDelay)
        {
            smokePlayed = true;
            
            if (auraBlack != null) auraBlack.Stop();
            if (auraBlue != null) auraBlue.Stop();
            
            if (ghostSmoke != null)
            {
                if (!ghostSmoke.gameObject.activeInHierarchy)
                    ghostSmoke.gameObject.SetActive(true);

                ghostSmoke.transform.SetParent(null);
                ghostSmoke.Play();
                
                float totalLifetime = ghostSmoke.main.duration
                                    + ghostSmoke.main.startLifetime.constantMax;
                Destroy(ghostSmoke.gameObject, totalLifetime);
            }

            Debug.Log("🌫 Smoke 파티클 재생 (페이드아웃 " + smokeDelay + "초 후)");
        }

        // ▶ 스프라이트 알파 감소 (항상 진행)
        Color c = sr.color;
        c.a -= fadeOutSpeed * Time.fixedDeltaTime;
        sr.color = c;

        rb.linearVelocity = Vector2.zero;
        Debug.Log("🌫 페이드아웃 중 → alpha: " + c.a);
        
        if (c.a <= 0f)
        {
            sr.enabled = false;
            this.enabled = false;
            Debug.Log("💀 유령 완전 소멸");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SafeZone"))
        {
            Debug.Log("🎯 SafeZone 감지됨, playerY = " + player.position.y);

            if (player.position.y >= requiredHeightToEscape)
            {
                safeZoneReached = true;
                fadeTimer = 0f;
                smokePlayed = false;
                Debug.Log("🟢 높이 충족 → 유령 사라짐 준비 (FadeOut 시작)");
            }
            else
            {
                Debug.Log("⚠ 높이 미달 → 사라지지 않음");
            }
        }
    }
}
