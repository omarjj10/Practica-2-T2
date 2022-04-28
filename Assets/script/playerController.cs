using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float JumpForce = 10;
    public float velocity = 10;
    public GameObject bala;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;
    private static readonly string ANIMATOR_STATE = "Estado";
    private static readonly int animacionSaltar = 2; 
    private static readonly int animacionCorrer = 1;
    private static readonly int animacionQuieto = 0;
    private static readonly int animacionDisparar = 3;
    private static readonly int animacionEnergia = 4;
    private static readonly int RIGHT = 1;
    private static readonly int LEFT = -1;

    public int intento = 0;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
        ChangeAnimation(animacionQuieto);
        if (Input.GetKey(KeyCode.RightArrow)) //si presiona la flecha a la derecha
        {
            Desplazarse(RIGHT);
            
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //si presiona la flecha a la derecha
        {
            Desplazarse(LEFT);
        }
        if (Input.GetKeyUp(KeyCode.Space)) //cada vez que suelto la tecla salta
        {
            _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            ChangeAnimation(animacionSaltar);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Disparar();
            ChangeAnimation(animacionDisparar);
        }
    }
    private void Disparar()
    {
        //codigo para crear elementos
        var x = this.transform.position.x;
        var y = this.transform.position.y;
        var bulletGo=Instantiate(bala,new Vector2(x,y), Quaternion.identity) as GameObject;
        var controller=bulletGo.GetComponent<balachicaController>();
        if (_sr.flipX){
            controller.velocidad *= -1;
        }
    }
    private void Desplazarse(int position)
    {
        _rb.velocity = new Vector2(velocity * position, _rb.velocity.y);
        _sr.flipX = position == LEFT;
        ChangeAnimation(animacionCorrer);
    }
    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger(ANIMATOR_STATE,animation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "enemy")
        {
            Debug.Log("Entrar en colision: "+other.gameObject.name);
            intento++;
            
        }
    }
}
