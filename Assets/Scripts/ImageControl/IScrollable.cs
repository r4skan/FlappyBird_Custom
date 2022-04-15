using UnityEngine;

namespace ImageControl
{
    public interface IScrollable
    {
        void Scroll(Vector2 dir, float speed);
    }
}
