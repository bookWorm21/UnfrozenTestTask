using System.Collections.Generic;
using UnfrozenTestTask.Source.Core;
using UnityEngine;

namespace UnfrozenTestTask.Source
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private List<AgentData> _playerAgents;
        [SerializeField] private List<AgentData> _enemyAgents;
        [SerializeField] private CombatManager _combatManager;
    
        private void Start()
        {
            _combatManager.StartFight(_playerAgents, _enemyAgents);
        }
    }
}