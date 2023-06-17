using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _rotateSpeed = 13.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spanManager;
    // Update is called once per frame
    private void Start()
    {
        _spanManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spanManager.StartSpawning();
            Destroy(this.gameObject, 0.20f);
            
        }
    }

    

}
