using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubesController : MonoBehaviour
{
    public static CubesController Instance;

    [Header("Map Size")] 
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;
    
    [Header("SpawnSettings")]
    [SerializeField] private float minTimeRespawn;
    [SerializeField] private float maxTimeRespawn;

    private void Awake()
    {
        Instance = this;
    }

    public void CallRespawn(GameObject cube)
    {
        StartCoroutine(Respawn(cube));
    }
    
    private IEnumerator Respawn(GameObject cube)
    {
        yield return new WaitForSeconds(Random.Range(minTimeRespawn, maxTimeRespawn));

        cube.transform.position = GetRandomPosition();
        
        cube.SetActive(true);
    }

    public Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }
}
