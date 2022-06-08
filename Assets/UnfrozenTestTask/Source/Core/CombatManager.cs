using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnfrozenTestTask.Source.Core
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> _playerAgentsPoints;
        [SerializeField] private List<Transform> _enemyAgentsPoints;
        [SerializeField] private Transform _agentsContainer;
        [SerializeField] private CombatAgent _agentPrefab;
        [SerializeField] private StepVisualizer _stepVisualizer;

        private List<CombatAgent> _allAgentsOnCombat = new List<CombatAgent>();
        private Queue<CombatAgent> _moveQueue = new Queue<CombatAgent>();

        public CombatData CombatData { get; private set; }

        public void StartFight(List<AgentData> playerAgents, List<AgentData> enemyAgents)
        {
            Clear();
        
            var playerSquad = SpawnSquad(playerAgents, _playerAgentsPoints, true);
            var enemySquad = SpawnSquad(enemyAgents, _enemyAgentsPoints, false, true);
            _stepVisualizer.Init(_allAgentsOnCombat);

            CombatData = new CombatData(playerSquad, enemySquad);

            StartCoroutine(FightCoroutine());
        }

        private IEnumerator FightCoroutine()
        {
            var playerWin = false;
            var endFight = EndFight(out playerWin);
            while (!endFight)
            {
                if (_moveQueue.Count == 0)
                {
                    CreateMoveQueue();
                }

                yield return _stepVisualizer.StepProcess(_moveQueue.Dequeue());
                endFight = EndFight(out playerWin);
            }
        }

        private bool EndFight(out bool isPlayerWin)
        {
            var haveEnemyAgent = CombatData.EnemyAgents.FirstOrDefault(ag => ag.IsAlive) != null;
            var havePlayerAgent = CombatData.PlayerAgents.FirstOrDefault(ag => ag.IsAlive) != null;
            isPlayerWin = !haveEnemyAgent;
            return !(haveEnemyAgent && havePlayerAgent);
        }

        private void CreateMoveQueue()
        {
            var allAgentsCopy = new List<CombatAgent>(_allAgentsOnCombat.Count);
            foreach (var agent in _allAgentsOnCombat)
            {
                if (agent.IsAlive)
                {
                    allAgentsCopy.Add(agent);
                }
            }

            while (allAgentsCopy.Count != 0)
            {
                var randomIndex = Random.Range(0, allAgentsCopy.Count);
                _moveQueue.Enqueue(allAgentsCopy[randomIndex]);
                allAgentsCopy.RemoveAt(randomIndex);
            }
        }

        private List<CombatAgent> SpawnSquad(List<AgentData> agentsData, List<Transform> points, bool isPlayerAgent, bool isMirroring = false)
        {
            var result = new List<CombatAgent>(Mathf.Min(agentsData.Count, points.Count));
            var pointIndex = 0;
            foreach (var agent in agentsData)
            {
                var combatAgent = Instantiate(_agentPrefab, 
                    points[pointIndex].transform.position, 
                    points[pointIndex].transform.rotation, 
                    _agentsContainer);
                combatAgent.Init(agent, isPlayerAgent, isMirroring);
                pointIndex++;

                _allAgentsOnCombat.Add(combatAgent);
                result.Add(combatAgent);

                if (pointIndex >= points.Count)
                {
                    break;
                }
            }

            return result;
        }

        private void Clear()
        {
            foreach (var combatAgent in _allAgentsOnCombat)
            {
                if (combatAgent != null)
                {
                    combatAgent.Destroy();
                }
            }
        
            _allAgentsOnCombat.Clear();
        }
    }
}