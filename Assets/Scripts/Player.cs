using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _shieldVisual;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;


    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn manager is NULL.");
        }
    }




    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
        //check for speedboost active move at 8.5f
        //after 5 seconds go back to default speed

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.85f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisual.SetActive(false);
            return;
        }
      
        _lives--;
        
        if(_lives < 1)
        {
            //Communicate with spawn manager
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void ActivateShield()
    {
        _isShieldActive = true;
        _shieldVisual.SetActive(true);
    }

    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine()); 
    }

    public void ActivateSpeedBoost()
    {
        _isSpeedBoostActive = true;
        _speed = 8.5f;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        while(_isSpeedBoostActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _speed = 3.5f;
            _isSpeedBoostActive = false;
        }
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        while(_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
            
        }
    }
   

}
