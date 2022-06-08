using System.Collections.Generic;

namespace UnfrozenTestTask.Source.Core
{
    public class CombatData
    {
        private List<CombatAgent> _playerAgents;
        private List<CombatAgent> _enemyAgents;

        public IEnumerable<CombatAgent> PlayerAgents => _playerAgents;
        public IEnumerable<CombatAgent> EnemyAgents => _enemyAgents;

        public CombatData(List<CombatAgent> playerAgents, List<CombatAgent> enemyAgents)
        {
            _playerAgents = playerAgents;
            _enemyAgents = enemyAgents;
        }
    }
}