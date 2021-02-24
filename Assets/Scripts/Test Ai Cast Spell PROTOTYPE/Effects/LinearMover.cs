using UnityEngine;

public class LinearMover : IMover
{
    private readonly Vector3 _direction;
    private readonly float _speed;
    private readonly Transform transform;

    public LinearMover(Transform transform,Vector3 direction,float speed)
    {
        this.transform = transform;
        _direction = direction;
        _speed = speed;
    }

    public void Tick()
    {
        transform.position += Time.fixedDeltaTime * _speed * _direction;
    }
}