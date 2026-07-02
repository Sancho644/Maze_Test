using UnityEngine;

namespace Enemy
{
    public interface IEnemyMovement
    {
        public void MoveTo(Vector3 position);
        public void Stop();
    }
}