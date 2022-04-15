using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private float maxSpeed;

    private Rigidbody2D m_playerRigidbody;
    private Animator m_playerAnimator;

    public event UnityAction OnScored;
    public event UnityAction OnDead;

    private bool m_bIsDead;
    
    private void Awake()
    {
        m_playerRigidbody = GetComponent<Rigidbody2D>();
        m_playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !m_bIsDead)
        {
            Jump();
        }

        AnimationUpdate();
    }

    private void Jump()
    {
        m_playerRigidbody.velocity = Vector2.zero;
        m_playerRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        if (m_playerRigidbody.velocity.y > maxSpeed)
        {
            m_playerRigidbody.velocity = new Vector2(0f, maxSpeed);
        }
        else if (m_playerRigidbody.velocity.y < -maxSpeed)
        {
            m_playerRigidbody.velocity = new Vector3(0f, -maxSpeed);
        }
    }

    private void AnimationUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            m_playerAnimator?.SetTrigger("OnDash");
        }

        if (m_bIsDead)
        {
            m_playerAnimator?.SetBool("IsDead", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("ScoreZone")) return;
        OnScored?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Obstacle") && !other.gameObject.CompareTag("Floor")) return;
        OnDead?.Invoke();
        m_bIsDead = true;
        // 죽었으면 collider 비활성화하고 Torque -30만큼 힘줘서 돌려버리기
        m_playerRigidbody.AddTorque(-90f, ForceMode2D.Impulse);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
