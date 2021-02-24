using UnityEngine;

public class LaunchGameObjectSpellEffect : SpellEffect
{
    private readonly float _speed;

    public LaunchGameObjectSpellEffect(float speed)
    {
        _speed = speed;
    }
    
    public override void Apply(Transform transformToLaunch, Vector3 targetPosition)
    {
        var haveMover = transformToLaunch.GetComponent<IHaveMover>();
        if(haveMover==null)
            return;
        
        var linearMover = new LinearMover(transformToLaunch,transformToLaunch.up, _speed);
        haveMover.ChangeMover(linearMover);
    }
}