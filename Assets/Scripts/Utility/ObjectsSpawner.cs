using UnityEngine;

public static class ObjectsSpawner
{
    public static void InRandomCircle
        (MonoBehaviour objectToSpawn,int numberToSpawn, float circleRange, Vector3 centerPosition)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            var randomPosition = Random.insideUnitSphere*circleRange;
            randomPosition = new Vector3(randomPosition.x,randomPosition.y,0);
            randomPosition += centerPosition;
            GameObject.Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }
}