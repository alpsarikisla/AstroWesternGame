using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D player;//Karakterimiz

    [SerializeField]//Unity içerisinden kontrol edilebilir oluyor
    private float movementSpeed;//HareketHızı
    private Animator karakterAnimasyon;

    private bool sagabak;

    void Start()
    {
        sagabak = true;
        //Karakterimizi Rigidbody olarak yani fiziksel bir nesne olarak tanımladık
        player = GetComponent<Rigidbody2D>();
        karakterAnimasyon = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Karakterimizin yatay eksende hareket edebileceğini tanımladık
        float horizontal = Input.GetAxis("Horizontal");
        HareketEt(horizontal);
        Don(horizontal);
    }

    private void HareketEt(float horizontal)
    {
        player.velocity = new Vector2(horizontal * movementSpeed ,player.velocity.y);
        //Hareket hızı çarpanı ile yatay eksende hareket sağlanıyor

        karakterAnimasyon.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void Don(float horizontal)
    {
        if (horizontal > 0 && !sagabak || horizontal < 0 && sagabak)
        {
            sagabak = !sagabak;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
