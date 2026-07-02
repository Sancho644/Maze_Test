using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class PatrolPath  : MonoBehaviour
    {
        [SerializeField] private Transform[] points;

        public IReadOnlyList<Transform> Points => points;
        public int Count => points.Length;
        
        public Transform GetPoint(int index)
        {
            return points[index];
        }
    }
}