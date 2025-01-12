using UnityEngine;

namespace MonoReferencing.Public
{
    public abstract class MonoReference : MonoBehaviour
    {
        public abstract object Data { get; }
    }
    
    public abstract class MonoReference<T> : MonoReference where T : IMonoReferenceData
    {
        [SerializeField] private T _data;
        
        public override object Data => _data;
    }
}