using UnityEngine;

/// <summary>
/// Use this component with a game object for demonstration purposes.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(InterpolationObjectController))]
public class TestMotion : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;

    private void FixedUpdate()
    {
        transform.position += Vector3.right * _speed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + _speed * 30 * Time.fixedDeltaTime, transform.rotation.eulerAngles.z);
    }
}
