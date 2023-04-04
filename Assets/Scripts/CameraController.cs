using MonsterExterminator.Damage;
using UnityEngine;

namespace MonsterExterminator
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform followTransform;
        [SerializeField] private float turnSpeed = 2f;
        [SerializeField] private Shaker shaker;
        public Shaker Shaker => shaker;

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