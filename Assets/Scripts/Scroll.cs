using ImageControl;
using UnityEngine;
using UnityEngine.Events;

public class Scroll : MonoBehaviour
{
    public event UnityAction<string> OnCountUpdate;
        
    private Transform m_deadZone;

    public float scrollSpeed;

    private void Start()
    {
        m_deadZone = GameObject.FindGameObjectWithTag("ScrollDeadZone")?.transform;
    }
        
    private void Update()
    {
        ScrollObj(Vector2.left, scrollSpeed);

        if ((int)transform.position.x != (int)m_deadZone.position.x) return;
        Dead();
    }
    
    public void ScrollObj(Vector2 dir, float speed)
    {
        transform.Translate(dir * (speed * Time.deltaTime));
    }

    private void Dead()
    {
        OnCountUpdate?.Invoke(gameObject.name);
        Destroy(gameObject);
    }
}
