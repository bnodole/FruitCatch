using System;
using System.Collections;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public GameManager gameManager;
    bool isScored;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        isScored = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.collider.CompareTag("CartBase") && !isScored)
        {
            if (this.gameObject.CompareTag("Bomb"))
            { 
                ParticleSystem explosion = this.transform.GetChild(0).GetComponent<ParticleSystem>();
                this.transform.GetChild(0).gameObject.SetActive(true);
                gameManager.sound.PlayOneShot(gameManager.destroySound);
                explosion.Play();
                collision.gameObject.SetActive(false);
                gameManager.Death();
            }
            else
            {
                gameManager.ScoreManager();
                this.GetComponent<Rigidbody>().mass = 0;
                isScored = true;
                Destroy(this.gameObject, 2);
            }
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject, 1);
        }
    }
}
