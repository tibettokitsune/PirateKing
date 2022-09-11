using UnityEngine;
using Zenject;

namespace Game.Units
{
    public class UnitController
    {
        private UnitData _unitData;
        private UnitView _unitView;
        private readonly CharacterController _characterController;
        private readonly DiContainer _container;

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
    }
}