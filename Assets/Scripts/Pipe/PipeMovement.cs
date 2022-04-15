using UnityEngine;

namespace Pipe
{
    public class PipeMovement : MonoBehaviour
    {
        public float moveSpeed;

        private void Start()
        {
            Destroy(gameObject, 3);
        }
        
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime));
        }
    }
}
