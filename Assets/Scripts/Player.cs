using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D player;//Karakterimiz
    [SerializeField]//Unity içerisinden kontrol edilebilir oluyor
    private float movementSpeed;//HareketHýzý
    private Animator karakterAnimasyon;
    private bool sagabak;
    private bool attack;
    private bool slide;

    void Start()
    {
        sagabak = true;
        //Karakterimizi Rigidbody olarak yani fiziksel bir nesne olarak tanýmladýk
        player = GetComponent<Rigidbody2D>();
        karakterAnimasyon = GetComponent<Animator>();
    }
    private void Update()
    {
        HandleInput();
    }
    void FixedUpdate()
    {
        //Karakterimizin yatay eksende hareket edebileceðini tanýmladýk
        float yon = Input.GetAxis("Horizontal");//Yatay eksende saða git komutu verilirse(D'ye basýlýrsa) 1 Sola git komutu verilerise (A'ya Basýlýrsa) -1 getirir
        HareketEt(yon);
        Don(yon);
        HandleAttack();
        ResetValues();
    }

    private void HareketEt(float netarafa)
    {
        if (!karakterAnimasyon.GetBool("slide") && !karakterAnimasyon.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            player.velocity = new Vector2(netarafa * movementSpeed, player.velocity.y);
            //Hareket hýzý çarpaný ile yatay eksende hareket saðlanýyor
        }
        if (slide && !karakterAnimasyon.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            karakterAnimasyon.SetBool("slide", true);
        }
        else if(!karakterAnimasyon.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            karakterAnimasyon.SetBool("slide", false);
        }
        karakterAnimasyon.SetFloat("speed", Mathf.Abs(netarafa));
    }

    private void HandleAttack()
    {
        if (attack && !karakterAnimasyon.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide = true;
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
        slide = false;
    }

}
