using System;
using Spine.Unity;
using UnityEngine;

namespace UnfrozenTestTask.Source
{
    public class CombatAgent : MonoBehaviour
    {
        public const string IdleAnimationName = "idle";
        public const string RunAnimationName = "run";
        public const string AttackAnimationName = "attack";
        
        [SerializeField] private SkeletonAnimation _skeletonAnimation;

        private int _maxHealth;
        private int _currentHealth;
        private int _damage;

        public bool IsPlayerAgent { get; private set; }
        public bool IsAlive { get; private set; } = true;

        public event Action Died;

        public void Init(AgentData agentData, bool isPlayerAgent, bool isMirroring = false)
        {
            _maxHealth = _currentHealth = agentData.Health;
            _damage = agentData.Damage;

            IsPlayerAgent = isPlayerAgent;
            _skeletonAnimation.state.SetAnimation(0, IdleAnimationName, true);
            
            _skeletonAnimation.loop = true;
            _skeletonAnimation.skeleton.scaleX = isMirroring ? -1 : 1 * _skeletonAnimation.skeleton.scaleX;
        }

        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                IsAlive = false;
                gameObject.SetActive(false);
                Died?.Invoke();
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}