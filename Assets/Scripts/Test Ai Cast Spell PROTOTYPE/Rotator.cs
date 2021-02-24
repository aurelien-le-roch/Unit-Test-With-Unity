using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        var newAngle = transform.eulerAngles.z+(_speed*Time.fixedDeltaTime);
        if (newAngle > 360 || newAngle < -360)
        {
            newAngle = 0;
        }
        transform.eulerAngles=new Vector3(0,0,newAngle);
    }
}
