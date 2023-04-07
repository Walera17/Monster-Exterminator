using UnityEngine;

namespace MonsterExterminator.Characters
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float turnSpeed = 8f;

        public float RotateToward(Vector3 aimDirection)
        {
            float currentTurnSpeed = 0;

            if (Mathf.Abs(aimDirection.magnitude) > 0.01f)
            {
                Quaternion prevRot = transform.rotation;

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDirection, Vector3.up), turnSpeed * Time.deltaTime);

                Quaternion currentRot = transform.rotation;
                float dir = Vector3.Dot(aimDirection, transform.right) > 0 ? 1 : -1;
                float rotationDelta = Quaternion.Angle(prevRot, currentRot) * dir;
                currentTurnSpeed = rotationDelta / Time.deltaTime;
            }

            return currentTurnSpeed;
        }
    }
}