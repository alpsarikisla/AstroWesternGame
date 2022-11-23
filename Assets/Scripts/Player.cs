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

    void Start()
    {
        sagabak = true;
        //Karakterimizi Rigidbody olarak yani fiziksel bir nesne olarak tanýmladýk
        player = GetComponent<Rigidbody2D>();
        karakterAnimasyon = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Karakterimizin yatay eksende hareket edebileceðini tanýmladýk
        float yon = Input.GetAxis("Horizontal");//Yatay eksende saða git komutu verilirse(D'ye basýlýrsa) 1 Sola git komutu verilerise (A'ya Basýlýrsa) -1 getirir
        HareketEt(yon);
        Don(yon);
    }

    private void HareketEt(float netarafa)
    {
        player.velocity = new Vector2(netarafa * movementSpeed ,player.velocity.y);
        //Hareket hýzý çarpaný ile yatay eksende hareket saðlanýyor

        karakterAnimasyon.SetFloat("speed", Mathf.Abs(netarafa));
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
}
