using UniRx;
using UnityEngine;

namespace Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        
        public ReactiveCommand OnScreenOpenStart { get; } = new ReactiveCommand();
        public ReactiveCommand OnScreenOpenEnd { get; } = new ReactiveCommand();
        public ReactiveCommand OnScreenCloseStart { get; } = new ReactiveCommand();
        public ReactiveCommand OnScreenCloseEnd { get; } = new ReactiveCommand();

        public virtual void CloseScreen()
        {
            OnScreenCloseStart.Execute();
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            OnScreenCloseEnd.Execute();
        }

        public virtual void OpenScreen()
        {
            OnScreenOpenStart.Execute();
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            OnScreenOpenEnd.Execute();
        }
    }
}