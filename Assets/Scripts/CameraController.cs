using UnityEngine;

namespace MonsterExterminator
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform followTransform;
        [SerializeField] private float turnSpeed = 2f;

        private void LateUpdate()
        {
            transform.position = followTransform.position;
        }

        public void AddYawInput(float amt)
        {
            transform.Rotate(Vector3.up, amt * Time.deltaTime * turnSpeed);
        }
    }
}