using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnfrozenTestTask.Source.UI.Windows
{
    public enum SelectingActionType
    {
        Attack,
        Skip
    }

    public class SelectActionWindow : MonoBehaviour
    {
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _skipButton;

        private bool _isInit;
        private bool _isSelectAction;

        public SelectingActionType ActionType { get; private set; }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            if (_isInit) return;

            _isInit = true;

            _attackButton.onClick.AddListener(() => SelectAction(SelectingActionType.Attack));
            _skipButton.onClick.AddListener(() => SelectAction(SelectingActionType.Skip));
        }

        public void Show()
        {
            Init();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public IEnumerator SelectActionCoroutine()
        {
            _isSelectAction = false;
            yield return new WaitUntil(() => _isSelectAction);
        }

        private void SelectAction(SelectingActionType actionType)
        {
            ActionType = actionType;
            _isSelectAction = true;
        }
    }
}