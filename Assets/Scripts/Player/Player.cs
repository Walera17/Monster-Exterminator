using MonsterExterminator.Damage;
using MonsterExterminator.Health;
using MonsterExterminator.UI;
using MonsterExterminator.Weapons;
using UnityEngine;

namespace MonsterExterminator.Player
{
    public class Player : MonoBehaviour, ITeamInterface
    {
        [SerializeField] private JoyStick moveStick;
        [SerializeField] private JoyStick aimStick;
        [SerializeField] CharacterController characterController;
        [SerializeField] MovementComponent movementComponent;
        [SerializeField] Animator animator;
        [SerializeField] Inventory inventory;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float animTurnSpeed = 5f;
        [SerializeField] TeamRelation teamRelation;

        [Header("Health and Damage")]
        [SerializeField] HealthComponent healthComponent;
        [SerializeField] PlayerHealthBar healthBar;

        private Vector2 moveInput, aimInput;
        private Camera mainCamera;
        private float animatorTurnSpeed;
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");
        private static readonly int RightSpeed = Animator.StringToHash("rightSpeed");
        private static readonly int TurnSpeed = Animator.StringToHash("turnSpeed");
        private static readonly int SwitchWeapon = Animator.StringToHash("switchWeapon");
        private static readonly int Attacking = Animator.StringToHash("attacking");
        private static readonly int Death = Animator.StringToHash("death");

        public int GetTeamID() => (int)teamRelation;

        void Start()
        {
            mainCamera = Camera.main;
            moveStick.OnStickInputValueChanged += MoveStick_OnStickInputValueChanged;
            aimStick.OnStickInputValueChanged += AimStick_OnStickInputValueChanged;
            aimStick.OnTaped += StartSwitchWeapon;
            healthComponent.OnHealthChange += HealthComponent_OnHealthChange;
            healthComponent.OnDead += HealthComponent_OnDead;
            healthComponent.BroadcastHealthValueImmediately();
        }

        private void OnDestroy()
        {
            moveStick.OnStickInputValueChanged -= MoveStick_OnStickInputValueChanged;
            aimStick.OnStickInputValueChanged -= AimStick_OnStickInputValueChanged;
            aimStick.OnTaped -= StartSwitchWeapon;
            healthComponent.OnHealthChange -= HealthComponent_OnHealthChange;
            healthComponent.OnDead -= HealthComponent_OnDead;
        }

        private void HealthComponent_OnDead()
        {
            animator.SetLayerWeight(2,1);
            animator.SetTrigger(Death);
        }

        private void HealthComponent_OnHealthChange(float health, float maxHealth, float delta, GameObject instigator)
        {
            healthBar.SetHealthValue(health , maxHealth, delta);
        }

        private void StartSwitchWeapon() => animator.SetTrigger(SwitchWeapon);

        public void AnimatorSwitchWeapon() => inventory.NextWeapon();

        public void AnimatorAttackPoint() => inventory.CurrentWeapon.Attack();

        private void MoveStick_OnStickInputValueChanged(Vector2 value) => moveInput = value;

        private void AimStick_OnStickInputValueChanged(Vector2 value)
        {
            aimInput = value;

            animator.SetBool(Attacking, aimInput.magnitude > 0);
        }

        Vector3 StickInputToWorldDirection(Vector2 inputValue)
        {
            Vector3 rightDir = mainCamera.transform.right; // направление вправо персонажа
            Vector3 upDir = Vector3.Cross(rightDir, Vector3.up); // направление вперед персонажа
            return rightDir * inputValue.x + upDir * inputValue.y; // мировое направление движения персонажа
        }

        void Update()
        {
            PerformMoveAndAim();

            UpdateCamera();
        }

        private void PerformMoveAndAim()
        {
            Vector3 moveDirection = StickInputToWorldDirection(moveInput);
            characterController.Move(moveDirection * (moveSpeed * Time.deltaTime));

            UpdateAim(moveDirection);

            float forward = Vector3.Dot(moveDirection, transform.forward);
            float right = Vector3.Dot(moveDirection, transform.right);

            animator.SetFloat(ForwardSpeed, forward);
            animator.SetFloat(RightSpeed, right);

            if (characterController.transform.position.y > 0.1f)
                characterController.Move(Vector3.down * Time.deltaTime * moveSpeed);
        }

        private void UpdateAim(Vector3 moveDirection)
        {
            Vector3 aimDirection = moveDirection;

            if (aimInput.magnitude != 0)
                aimDirection = StickInputToWorldDirection(aimInput);

            RotateToward(aimDirection);
        }

        private void UpdateCamera()
        {
            if (moveInput.magnitude != 0 && aimInput.magnitude == 0 && cameraController != null)
                cameraController.AddYawInput(moveInput.x);
        }

        private void RotateToward(Vector3 aimDirection)
        {
            float currentTurnSpeed = movementComponent.RotateToward(aimDirection);

            animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, animTurnSpeed * Time.deltaTime);

            if (Mathf.Abs(animatorTurnSpeed) < 0.01f)
                animatorTurnSpeed = 0;

            animator.SetFloat(TurnSpeed, animatorTurnSpeed);
        }
    }
}