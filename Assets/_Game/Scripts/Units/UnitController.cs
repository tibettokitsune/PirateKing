using UnityEngine;
using Zenject;

namespace Game.Units
{
    public class UnitController
    {
        private UnitData _unitData;
        private CharacterController _characterController;

        public UnitController(UnitData data, CharacterController controller)
        {
            _unitData = data;
            _characterController = GameObject.Instantiate(controller);
        }

        public void ChangeStartPosition(Vector3 pos)
        {
            _characterController.transform.position = pos;
            _characterController.enabled = true;
        }
        
        
        public class Factory : PlaceholderFactory<UnitData, CharacterController,UnitController>
        {
            private DiContainer _container;
            public Factory(DiContainer container)
            {
                _container = container;
            }

            public override UnitController Create(UnitData data, CharacterController controller)
            {
                return new UnitController(data, controller);
            }
        }
    }
}