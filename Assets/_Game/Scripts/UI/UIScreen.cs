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
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public virtual void OpenScreen()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}