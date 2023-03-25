using MonsterExterminator.UI;
using MonsterExterminator.Weapons;
using UnityEngine;

namespace MonsterExterminator.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private JoyStick moveStick;
        [SerializeField] private JoyStick aimStick;
        [SerializeField] CharacterController characterController;
        [SerializeField] Animator animator;
        [SerializeField] Inventory inventory;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float turnSpeed = 8f;
        [SerializeField] private float animTurnSpeed = 5f;

        private Vector2 moveInput, aimInput;
        private Camera mainCamera;
        private float animatorTurnSpeed;
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");
        private static readonly int RightSpeed = Animator.StringToHash("rightSpeed");
        private static readonly int TurnSpeed = Animator.StringToHash("turnSpeed");
        private static readonly int SwitchWeapon = Animator.StringToHash("switchWeapon");
        private static readonly int Attacking = Animator.StringToHash("attacking");

        void Start()
        {
            mainCamera = Camera.main;
            moveStick.OnStickInputValueChanged += MoveStick_OnStickInputValueChanged;
            aimStick.OnStickInputValueChanged += AimStick_OnStickInputValueChanged;
            aimStick.OnTaped += StartSwitchWeapon;
        }

        private void OnDestroy()
        {
            moveStick.OnStickInputValueChanged -= MoveStick_OnStickInputValueChanged;
            aimStick.OnStickInputValueChanged -= AimStick_OnStickInputValueChanged;
            aimStick.OnTaped -= StartSwitchWeapon;
        }

        private void StartSwitchWeapon()
        {
            animator.SetTrigger(SwitchWeapon);
        }
            
        public void AnimatorSwitchWeapon()
        {
            inventory.NextWeapon();
        }

        private void MoveStick_OnStickInputValueChanged(Vector2 value)
        {
            moveInput = value;
        }

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
            float currentTurnSpeed = 0;

            if (aimDirection.magnitude != 0)
            {
                Quaternion prevRot = transform.rotation;

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDirection, Vector3.up), turnSpeed * Time.deltaTime);

                Quaternion currentRot = transform.rotation;
                float dir = Vector3.Dot(aimDirection, transform.right) > 0 ? 1 : -1;
                float rotationDelta = Quaternion.Angle(prevRot, currentRot) * dir;
                currentTurnSpeed = rotationDelta / Time.deltaTime;
            }

            animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, animTurnSpeed * Time.deltaTime);

            if (Mathf.Abs(animatorTurnSpeed) < 0.01f)
                animatorTurnSpeed = 0;

            animator.SetFloat(TurnSpeed, animatorTurnSpeed);
        }
    }
}