using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D player;//Karakterimiz

    [SerializeField]//Unity i�erisinden kontrol edilebilir oluyor
    private float movementSpeed;//HareketH�z�
    private Animator karakterAnimasyon;

    private bool sagabak;

    void Start()
    {
        sagabak = true;
        //Karakterimizi Rigidbody olarak yani fiziksel bir nesne olarak tan�mlad�k
        player = GetComponent<Rigidbody2D>();
        karakterAnimasyon = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Karakterimizin yatay eksende hareket edebilece�ini tan�mlad�k
        float horizontal = Input.GetAxis("Horizontal");
        HareketEt(horizontal);
        Don(horizontal);
    }

    private void HareketEt(float horizontal)
    {
        player.velocity = new Vector2(horizontal * movementSpeed ,player.velocity.y);
        //Hareket h�z� �arpan� ile yatay eksende hareket sa�lan�yor

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
