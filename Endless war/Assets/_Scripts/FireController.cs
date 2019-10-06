using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private GameController gc;
    private Rigidbody2D rBody;
    public float speed;
    private AudioSource explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
        rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = transform.right * speed;
        explosionSound = gc.audioSources[(int)SoundClip.EXPLOSION];

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);
            gc.Score += 100;
            explosionSound.volume = 0.3f;
            explosionSound.Play();

        }
    }
}
