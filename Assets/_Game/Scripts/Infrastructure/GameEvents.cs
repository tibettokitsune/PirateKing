using UniRx;

namespace Game.Infrastructure
{
    public class GameEvents
    {
        public ReactiveCommand OnMenuLoaded { get; } = new ReactiveCommand();
        public ReactiveCommand OnTutorialSceneLoaded { get; } = new ReactiveCommand();
    }
}