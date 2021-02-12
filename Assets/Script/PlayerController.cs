using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float PowerupStrength = 15.0f;
    public bool hasPowerup = false;
    public GameObject PowerupIndicator;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float fowardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * fowardInput);

        PowerupIndicator.transform.position = transform.position + new Vector3(0, 4.65f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            PowerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());

            IEnumerator PowerupCountdownRoutine()
            {
                yield return new WaitForSeconds(7);
                hasPowerup = false;
                PowerupIndicator.gameObject.SetActive(false);
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("Collided with: " + collision.gameObject + " with power set to " + hasPowerup );
            enemyRigidbody.AddForce(awayFromPlayer * PowerupStrength, ForceMode.Impulse);
        }
    }
}
