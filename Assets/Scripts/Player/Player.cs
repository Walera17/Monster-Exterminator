using MonsterExterminator.UI;
using UnityEngine;

namespace MonsterExterminator.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private JoyStick moveStick;
        [SerializeField] private JoyStick aimStick;
        [SerializeField] CharacterController characterController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private float moveSpeed = 20f;
        [SerializeField] private float turnSpeed = 30f;

        private Vector2 moveInput, aimInput;
        private Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main;
            moveStick.OnStickInputValueChanged += MoveStick_OnStickInputValueChanged;
            aimStick.OnStickInputValueChanged += AimStick_OnStickInputValueChanged;
        }

        private void OnDestroy()
        {
            moveStick.OnStickInputValueChanged -= MoveStick_OnStickInputValueChanged;
            aimStick.OnStickInputValueChanged -= AimStick_OnStickInputValueChanged;
        }

        private void MoveStick_OnStickInputValueChanged(Vector2 value)
        {
            moveInput = value;
        }

        private void AimStick_OnStickInputValueChanged(Vector2 value)
        {
            aimInput = value;
        }

        Vector3 StickInputToWorldDirection(Vector2 inputValue)
        {
            Vector3 rightDir = mainCamera.transform.right;                              // направление вправо персонажа
            Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);                        // направление вперед персонажа
            return rightDir * inputValue.x + upDir * inputValue.y;                      // мировое направление движения персонажа
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
            if (aimDirection.magnitude != 0)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDirection, Vector3.up), turnSpeed * Time.deltaTime);
        }
    }
}