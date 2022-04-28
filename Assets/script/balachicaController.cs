using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balachicaController : MonoBehaviour
{
    public float velocidad=10;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,5);
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector2(velocidad, _rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "enemy")
        {
            //Debug.Log("Entrar en colision: "+other.gameObject.name);
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
