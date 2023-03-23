using UnityEngine;

namespace MonsterExterminator
{
    [ExecuteAlways]
    public class CameraArm : MonoBehaviour
    {
        [SerializeField] private float armLenght;
        [SerializeField] private Transform child;

        private void Update()
        {
            child.position = transform.position - child.forward * armLenght;
        }
    }
}