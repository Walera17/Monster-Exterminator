using MonsterExterminator.UI;
using UnityEngine;

namespace MonsterExterminator.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private JoyStick joyStick;
        [SerializeField] CharacterController characterController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private float moveSpeed = 20f;
        [SerializeField] private float turnSpeed = 30f;

        private Vector2 moveInput;
        private Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main;
            joyStick.OnStickInputValueChanged += JoyStick_OnStickInputValueChanged;
        }

        private void OnDestroy()
        {
            joyStick.OnStickInputValueChanged -= JoyStick_OnStickInputValueChanged;
        }

        private void JoyStick_OnStickInputValueChanged(Vector2 value)
        {
            moveInput = value;
        }

        void Update()
        {
            Vector3 rightDir = mainCamera.transform.right; // направление вправо персонажа
            Vector3 upDir = Vector3.Cross(rightDir, Vector3.up); // направление вперед персонажа
            Vector3 moveDirection = rightDir * moveInput.x + upDir * moveInput.y; // направление движения персонажа

            characterController.Move(moveDirection * (moveSpeed * Time.deltaTime));

            if (moveInput.magnitude != 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), turnSpeed * Time.deltaTime);

                if (cameraController != null)
                    cameraController.AddYawInput(moveInput.x);
            }
        }
    }
}