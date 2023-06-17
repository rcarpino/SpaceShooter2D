using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    private Animator _anim;

    // Update is called once per frame

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player is NULL.");
        }

        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }

        
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if(transform.position.y < -6f)
        {
            float RandomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(RandomX, 7.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 1.8f);
        }
        
        if (other.tag == "Laser")
        {

            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddtoScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 1.8f);
        }
        
    }
}
