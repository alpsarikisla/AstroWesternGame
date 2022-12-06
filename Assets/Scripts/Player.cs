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
    private bool attack;

    void Start()
    {
        sagabak = true;
        //Karakterimizi Rigidbody olarak yani fiziksel bir nesne olarak tan�mlad�k
        player = GetComponent<Rigidbody2D>();
        karakterAnimasyon = GetComponent<Animator>();
    }
    private void Update()
    {
        HandleInput();
    }
    void FixedUpdate()
    {
        //Karakterimizin yatay eksende hareket edebilece�ini tan�mlad�k
        float yon = Input.GetAxis("Horizontal");//Yatay eksende sa�a git komutu verilirse(D'ye bas�l�rsa) 1 Sola git komutu verilerise (A'ya Bas�l�rsa) -1 getirir
        HareketEt(yon);
        Don(yon);
        HandleAttack();
        ResetValues();
    }

    private void HareketEt(float netarafa)
    {
        if (!karakterAnimasyon.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            player.velocity = new Vector2(netarafa * movementSpeed, player.velocity.y);
            //Hareket h�z� �arpan� ile yatay eksende hareket sa�lan�yor
        }
        karakterAnimasyon.SetFloat("speed", Mathf.Abs(netarafa));
    }

    private void HandleAttack()
    {
        if (attack)
        {
            karakterAnimasyon.SetTrigger("attack");
            player.velocity = Vector2.zero;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }
    }

    private void Don(float netarafa)
    {
        if (netarafa > 0 && !sagabak || netarafa < 0 && sagabak)
        {
            sagabak = !sagabak;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void ResetValues()
    {
        attack = false;
    }

}
