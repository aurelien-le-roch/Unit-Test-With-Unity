using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    private Vector3 _direction;
    private float _speed;

    public void Launch(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
    }

    private void FixedUpdate()
    {
        transform.position += Time.fixedDeltaTime * _speed * _direction;
    }
}