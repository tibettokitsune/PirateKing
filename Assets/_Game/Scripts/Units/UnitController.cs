using Game.Units.UnitStates;
using GoodCat.Fsm;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Units
{
    public class UnitController : IFixedTickable, ITickable
    {
        public CompositeDisposable Disposable { get; } = new CompositeDisposable();
        private ReactiveCommand OnFixedUpdate { get; } = new ReactiveCommand();
        private ReactiveCommand OnUpdate { get; } = new ReactiveCommand();
        
        private ReactiveCommand<UnitView> OnViewUpdate { get; } = new ReactiveCommand<UnitView>();
        public ReactiveCommand OnTargetUpdate { get; } = new ReactiveCommand();
        private UnitData _unitData;
        private UnitView _unitView;
        private readonly CharacterController _characterController;
        private readonly DiContainer _container;
        private AnimatorObserver _animatorObserver;
        
        [SerializeField, ReadOnly] private Vector2 _movementVector;
        [SerializeField, ReadOnly] private bool _evade;
        [SerializeField, ReadOnly] private bool _crouch;
        [SerializeField, ReadOnly] private bool _moveBoost;
        [SerializeField, ReadOnly] private bool _jump;

        [SerializeField, ReadOnly] private bool _isAttack;
        [SerializeField, ReadOnly] private Vector2 _attackDirection;


        private UnitDamageController _unitDamageController = new UnitDamageController();
        private UnitController _fightTarget;

        private FSM _movementFsm;
        private FSM _attackFsm;
        private UnitController(DiContainer container,UnitData data, CharacterController controller)
        {
            _container = container;
            _unitData = data;
            _characterController = _container.InstantiatePrefabForComponent<CharacterController>(controller);
            InitializeMovement();

            InitializeAttack();
        }

        private void InitializeAttack()
        {
            _attackFsm = new FSM();

            var idle = new StateSimple("Idle", null, null, null);
            var attack = new StateSimple("Attack", () =>
            {
                _unitView.AttackAnimation(_attackDirection);
                _unitDamageController.AttackState(true);
            }, null, () =>
            {
                _unitDamageController.AttackState(false);
            });

            _attackFsm.StatesCollection.Add(idle);
            _attackFsm.StatesCollection.Add(attack);

            _attackFsm.StatesCollection.SetStartState(idle);

            _attackFsm.StatesCollection.Transitions.From(idle).To(attack).Set(() => _isAttack && !_animatorObserver.IsAnimationPlay);
            _attackFsm.StatesCollection.Transitions.From(attack).To(idle).Set(() => !_animatorObserver.IsAnimationPlay);

            _attackFsm.Initialize();

            OnUpdate.Subscribe(_ => _attackFsm.Update()).AddTo(Disposable);
        }

        private void InitializeMovement()
        {
            _movementFsm = new FSM();

            #region AddingMovementState
                var movementState = new MovementState(_characterController, _unitData);
                
                OnTargetUpdate.Subscribe(_ =>
                {
                    movementState.UpdateTargets(this, _fightTarget);
                }).AddTo(Disposable);
                OnViewUpdate.Subscribe(_ =>
                {
                    movementState.UpdateView(_unitView);
                }).AddTo(Disposable);
                _movementFsm.StatesCollection.Add(movementState);

                _movementFsm.StatesCollection.SetStartState(movementState);

                OnFixedUpdate.Subscribe(_ =>
                {
                    movementState.UpdateMovementData(_movementVector);
                    
                }).AddTo(Disposable);
            #endregion
            
            #region AddingBoosMovementState
                var boostMovementState = new BoostMovementState(_characterController, _unitData);
                
                OnTargetUpdate.Subscribe(_ =>
                {
                    boostMovementState.UpdateTargets(this, _fightTarget);
                }).AddTo(Disposable);
                OnViewUpdate.Subscribe(_ =>
                {
                    boostMovementState.UpdateView(_unitView);
                }).AddTo(Disposable);
                _movementFsm.StatesCollection.Add(boostMovementState);
                OnFixedUpdate.Subscribe(_ =>
                {
                    boostMovementState.UpdateMovementData(_movementVector);
                }).AddTo(Disposable);
            #endregion
            
            #region AddingCrouchState
                var crouchState = new CrouchState(_characterController, _unitData);
                    
                OnTargetUpdate.Subscribe(_ =>
                {
                    crouchState.UpdateTargets(this, _fightTarget);
                }).AddTo(Disposable);
                OnViewUpdate.Subscribe(_ =>
                {
                    crouchState.UpdateView(_unitView);
                }).AddTo(Disposable);
                _movementFsm.StatesCollection.Add(crouchState);
                OnFixedUpdate.Subscribe(_ =>
                {
                    crouchState.UpdateMovementData(_movementVector);
                }).AddTo(Disposable);
            #endregion
            
            #region AddingJumpState
                var jumpState = new JumpState(_characterController, _unitData);
                    
                OnTargetUpdate.Subscribe(_ =>
                {
                    jumpState.UpdateTargets(this, _fightTarget);
                }).AddTo(Disposable);
                OnViewUpdate.Subscribe(_ =>
                {
                    jumpState.UpdateView(_unitView);
                }).AddTo(Disposable);
                _movementFsm.StatesCollection.Add(jumpState);
                OnFixedUpdate.Subscribe(_ =>
                {
                    jumpState.UpdateMovementData(_movementVector);
                }).AddTo(Disposable);
            #endregion
            
            #region AddingEvadeState
                var evadeState = new EvadeState(_characterController, _unitData);
                    
                OnTargetUpdate.Subscribe(_ =>
                {
                    evadeState.UpdateTargets(this, _fightTarget);
                }).AddTo(Disposable);
                OnViewUpdate.Subscribe(_ =>
                {
                    evadeState.UpdateView(_unitView, _animatorObserver);
                }).AddTo(Disposable);
                _movementFsm.StatesCollection.Add(evadeState);
                OnFixedUpdate.Subscribe(_ =>
                {
                    evadeState.UpdateMovementData(_movementVector);
                }).AddTo(Disposable);
            #endregion
            
            _movementFsm.StatesCollection.Transitions.From(movementState).To(boostMovementState).Set(() => _moveBoost);
            _movementFsm.StatesCollection.Transitions.From(boostMovementState).To(movementState).Set(() => !_moveBoost);
            _movementFsm.StatesCollection.Transitions.From(movementState).To(crouchState).Set(() => _crouch);
            _movementFsm.StatesCollection.Transitions.From(crouchState).To(movementState).Set(() => !_crouch);
            
            _movementFsm.StatesCollection.Transitions.From(movementState).To(jumpState)
                .Set(() =>_jump && IsGrounded());
            _movementFsm.StatesCollection.Transitions.From(boostMovementState).To(jumpState)
                .Set(() => _jump && IsGrounded());
            _movementFsm.StatesCollection.Transitions.From(jumpState).To(movementState)
                .Set(() => IsGrounded() && jumpState.IsReadyToSwitch());

            _movementFsm.StatesCollection.Transitions.From(movementState).To(evadeState).Set(() => _evade);
            _movementFsm.StatesCollection.Transitions.From(boostMovementState).To(evadeState).Set(() => _evade);
            _movementFsm.StatesCollection.Transitions.From(crouchState).To(evadeState).Set(() => _evade);

            _movementFsm.StatesCollection.Transitions.From(evadeState).To(movementState).Set(() => evadeState.IsReadyToSwitch());
            
            
            OnFixedUpdate.Subscribe(_ =>
            {
                _movementFsm.Update();
            }).AddTo(Disposable);
            _movementFsm.Initialize();
        }

        public void ChangeStartPosition(Vector3 pos)
        {
            _characterController.transform.position = pos;
            _characterController.enabled = true;
        }

        public void CreateView(UnitView view)
        {
            _unitView = _container.InstantiatePrefabForComponent<UnitView>(view, _characterController.transform);
            _animatorObserver = _unitView.AnimatorObserver;
            _unitDamageController.Setup(_unitView);
            OnViewUpdate.Execute(_unitView);
        }

        public Transform GetTransformTarget() => _characterController.transform;

        public void UpdateMovementData(Vector3 movement, float isJump, float isEvade, float isMovementBoost, float isCrouch)
        {
            _movementVector = new Vector3(movement.x, movement.y, isJump);
            _evade = isEvade > 0;
            _crouch = isCrouch > 0;
            _moveBoost = isMovementBoost > 0;
            _jump = isJump > 0;
        }
        
        public void UpdateAttackData(Vector2 direction, bool isAttack)
        {
            _attackDirection = direction;
            _isAttack = isAttack;
        }

        public void FixedTick()
        {
            OnFixedUpdate.Execute();
        }

        public void UpdateFightTarget(UnitController target)
        {
            _fightTarget = target;
            OnTargetUpdate.Execute();
        }

        private bool IsGrounded()
        {
            //character controller layer mask ignoring
            int layerMask = 1 << 6;
            layerMask = ~layerMask;
            RaycastHit hit;
            if (Physics.Raycast(_characterController.transform.position,
                    Vector3.down, out hit, 0.01f,
                    layerMask))
                return true;
            return false;
        }

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

        public void Tick()
        {
            OnUpdate.Execute();
        }
    }
}