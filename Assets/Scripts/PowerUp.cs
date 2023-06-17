using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _powerupSpeed = 3f;
  
    [SerializeField] //0 = TripleShot 1 = Speed 2 = Shields
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _powerupSpeed * Time.deltaTime);
        
        if(transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if(player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.ActivateTripleShot();
                        break;
                    case 1:
                        player.ActivateSpeedBoost();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
                
                Destroy(this.gameObject);
            }
        }
    }
}
