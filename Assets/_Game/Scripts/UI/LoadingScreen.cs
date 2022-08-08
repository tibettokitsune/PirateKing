using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class LoadingScreen : UIScreen
    {
        [SerializeField] private Slider bar;
        private Coroutine _loadingBarCoroutine;
        public void Loading(AsyncOperation loading, Action onComplete)
        {
            StopIfWork();
            _loadingBarCoroutine = StartCoroutine(LoadingBarWork(loading, onComplete));
        }

        private void StopIfWork()
        {
            if (_loadingBarCoroutine != null)
                StopCoroutine(_loadingBarCoroutine);
        }

        private IEnumerator LoadingBarWork(AsyncOperation loading, Action onComplete)
        {
            OpenScreen();

            while (!loading.isDone)
            {
                bar.value = loading.progress;
                yield return null;
            }
            
            CloseScreen();
            onComplete.Invoke();
        }
    }
}