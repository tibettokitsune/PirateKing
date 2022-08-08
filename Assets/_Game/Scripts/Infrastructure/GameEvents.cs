using UniRx;

namespace _Game.Scripts.Infrastructure
{
    public class GameEvents
    {
        public ReactiveCommand OnMenuLoaded { get; } = new ReactiveCommand();
        public ReactiveCommand OnTutorialSceneLoaded { get; } = new ReactiveCommand();
    }
}