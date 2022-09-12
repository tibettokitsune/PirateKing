using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Units
{
    public class UnitController : IFixedTickable
    {
        public CompositeDisposable Disposable { get; } = new CompositeDisposable();
        private UnitData _unitData;
        private UnitView _unitView;
        private readonly CharacterController _characterController;
        private readonly DiContainer _container;

        [SerializeField, ReadOnly] private Vector3 _movementVector;
        [SerializeField, ReadOnly] private float _evade;

        private UnitController _fightTarget;
        private UnitController(DiContainer container,UnitData data, CharacterController controller)
        {
            _container = container;
            _unitData = data;
            _characterController = _container.InstantiatePrefabForComponent<CharacterController>(controller);
        }

        public void ChangeStartPosition(Vector3 pos)
        {
            _characterController.transform.position = pos;
            _characterController.enabled = true;
        }

        public void CreateView(UnitView view)
        {
            _unitView = _container.InstantiatePrefabForComponent<UnitView>(view, _characterController.transform);
        }

        public Transform GetTransformTarget() => _characterController.transform;
        
        
        public class Factory : PlaceholderFactory<UnitData, CharacterController,UnitController>
        {
            private readonly DiContainer _container;
            public Factory(DiContainer container)
            {
                _container = container;
            }

            public override UnitController Create(UnitData data, CharacterController controller)
            {
                return new UnitController(_container, data, controller);
            }
        }

        public void Movement(float horizontal, float vertical, float isJump, float isEvade)
        {
            _movementVector = new Vector3(horizontal, isJump, vertical);
            _evade = isEvade;
        }

        public void FixedTick()
        {
            var forwardVector = _fightTarget.GetTransformTarget().position - GetTransformTarget().position;
            var rightVector = Quaternion.AngleAxis(90, Vector3.up) * forwardVector;
            _characterController.Move(forwardVector.normalized * _movementVector.z * _unitData.MovementSpeed);
            _characterController.Move(rightVector.normalized * _movementVector.x * _unitData.MovementSpeed);

            _characterController.Move(Physics.gravity);
            
            forwardVector.y = 0;

            var rootRotation = Quaternion.LookRotation(forwardVector);
            _unitView.UpdateRotationData(rootRotation);
            _unitView.UpdateAnimationData(_movementVector.x, _movementVector.z,
                _movementVector.y > 0, _evade > 0 );
        }
        public void UpdateFightTarget(UnitController target) => _fightTarget = target;
    }
}