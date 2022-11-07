using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesSpawner : MonoBehaviour
{
    public IReadOnlyList<GameObject> Cubes => _cubes ;
    
    [SerializeField]
    private GameObject _cubePrefab;

    [SerializeField]
    private Vector2 _startSpawnPoint;
    
    [SerializeField]
    private int _rows;

    [SerializeField]
    private int _columns;
    
    [SerializeField]
    private float _sideCubeSize = 0.25f;
    
    [SerializeField]
    private float _offset = 0.05f;

    [SerializeField]
    private float _delayBeforeNextSpawn = 0.01f;

    private  WaitForSeconds _waitBeforeNextSpawn;
    private readonly List<GameObject> _cubes = new();

    private void Start()
    {
        _waitBeforeNextSpawn = new WaitForSeconds(_delayBeforeNextSpawn);
        StartCoroutine(SpawnCubes());;
    }

    private IEnumerator SpawnCubes()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                var localCubeX = j * _sideCubeSize + j * _offset;
                var localCubeY = i * _sideCubeSize + i * _offset;
                var cubePosition = _startSpawnPoint + new Vector2(localCubeX, -localCubeY);
                
                _cubes.Add(Instantiate(_cubePrefab, cubePosition, Quaternion.identity));
                yield return _waitBeforeNextSpawn;
            }
        }
    }
}
