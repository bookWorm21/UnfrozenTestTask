using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnfrozenTestTask.Source.UI.Windows;
using UnityEngine;

namespace UnfrozenTestTask.Source
{
    public class StepVisualizer : MonoBehaviour
    {
        [SerializeField] private Transform _centerPoint;
        [SerializeField] private float _offsetXFromCenter;
        [SerializeField] private float _timeForMoveCenter;
        [SerializeField] private float _timeForReturn;
        [SerializeField] private SelectActionWindow _selectActionWindow;

        private List<CombatAgent> AllAgents;

        public void Init(List<CombatAgent> agents)
        {
            AllAgents = agents;
        }

        public IEnumerator StepProcess(CombatAgent source)
        {
            var opponent = FindOpponent(source);

            if (opponent == null) yield break;

            var opponentLastPosition = opponent.transform.position;
            var opponentNewPosition = _centerPoint.position +
                                      new Vector3(opponent.IsPlayerAgent ? -_offsetXFromCenter : _offsetXFromCenter, 0,
                                          0);

            var sourceLastPosition = source.transform.position;
            var sourceNewPosition = _centerPoint.position +
                                    new Vector3(source.IsPlayerAgent ? -_offsetXFromCenter : _offsetXFromCenter, 0, 0);

            var actionType = SelectingActionType.Attack;
            if (source.IsPlayerAgent)
            {
                _selectActionWindow.Show();
                _selectActionWindow.Reset();
                yield return _selectActionWindow.SelectActionCoroutine();
                _selectActionWindow.Hide();
                actionType = _selectActionWindow.ActionType;
            }

            if (actionType == SelectingActionType.Skip)
            {
                yield break;
            }

            var elapsedTime = 0.0f;
            while (elapsedTime < _timeForMoveCenter)
            {
                source.transform.position = Vector3.Lerp
                    (sourceLastPosition, sourceNewPosition, elapsedTime / _timeForMoveCenter);
                opponent.transform.position = Vector3.Lerp
                    (opponentLastPosition, opponentNewPosition, elapsedTime / _timeForMoveCenter);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            source.transform.position = sourceNewPosition;
            opponent.transform.position = opponentNewPosition;


            yield return source.Attack(opponent);

            elapsedTime = 0;
            while (elapsedTime <= _timeForReturn)
            {
                source.transform.position = Vector3.Lerp
                    (sourceNewPosition, sourceLastPosition, elapsedTime / _timeForReturn);

                opponent.transform.position = Vector3.Lerp
                    (opponentNewPosition, opponentLastPosition, elapsedTime / _timeForReturn);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            source.transform.position = sourceLastPosition;
            opponent.transform.position = opponentLastPosition;
        }

        private CombatAgent FindOpponent(CombatAgent source)
        {
            return source.IsPlayerAgent ? 
                AllAgents.FirstOrDefault(ag => ag.IsAlive && !ag.IsPlayerAgent) 
                : AllAgents.FirstOrDefault(ag => ag.IsAlive && ag.IsPlayerAgent);
        }
    }
}