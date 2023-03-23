using MonsterExterminator.UI;
using UnityEngine;

namespace MonsterExterminator.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private JoyStick joyStick;
        [SerializeField] CharacterController characterController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private float moveSpeed = 20;

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
            Vector3 rightDir = mainCamera.transform.right;                                                              // ����������� ������ ���������
            Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);           	                                            // ����������� ������ ���������
            characterController.Move((rightDir * moveInput.x + upDir * moveInput.y) * (Time.deltaTime * moveSpeed));

            if (moveInput.magnitude != 0 && cameraController != null)
                cameraController.AddYawInput(moveInput.x);
        }
    }
}