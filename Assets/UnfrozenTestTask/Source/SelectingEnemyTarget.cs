using System.Collections;
using UnityEngine;

namespace UnfrozenTestTask.Source
{
    public class SelectingEnemyTarget : MonoBehaviour
    {
        [SerializeField] private LayerMask _combatAgentLayer;
        
        private bool _isSelecting;
        private Camera _mainCamera;

        public CombatAgent SelectedAgent { get; private set; }

        private void Start()
        {
            _mainCamera = Camera.main;
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var result = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Input.mousePosition), 1000f, _combatAgentLayer);
                if (result.collider != null && result.collider.TryGetComponent(out CombatAgent agent))
                {
                    if (agent.IsPlayerAgent || !agent.IsAlive) return;

                    SelectedAgent = agent;
                    _isSelecting = true;
                }
            }
        }

        public IEnumerator SelectTarget()
        {
            SelectedAgent = null;
            _isSelecting = false;
            enabled = true;
            yield return new WaitUntil(() => _isSelecting);
            enabled = false;
        }
    }
}