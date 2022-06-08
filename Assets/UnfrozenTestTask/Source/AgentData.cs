using UnityEngine;

namespace UnfrozenTestTask.Source
{
    [CreateAssetMenu(menuName = "agent data", fileName = "new agent data")]
    public class AgentData : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _health;

        public int Damage => _damage;
    
        public int Health => _health;
    }
}