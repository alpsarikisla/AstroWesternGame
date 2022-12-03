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

    [SerializeField]
    private Transform[] groundPoints;//Karaktere oluşturduğumuz GroundPointleri buraya ekleyeceğiz

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;
   
    private bool isGrounded;

    private bool jump;
    private bool slide;

    [SerializeField]
    private float jumpForce;

    private bool attack;

    void Start()
    {
        sagabak = true;
        //Karakterimizi Rigidbody olarak yani fiziksel bir nesne olarak tanımladık
        player = GetComponent<Rigidbody2D>();
        karakterAnimasyon = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        //Karakterimizin yatay eksende hareket edebileceğini tanımladık
        float yon = Input.GetAxis("Horizontal");//Yatay eksende sağa git komutu verilirse(D'ye basılırsa) 1 Sola git komutu verilerise (A'ya Basılırsa) -1 getirir

        isGrounded = IsGrounded();

        HareketEt(yon);

        Don(yon);

        HandleAttaks();

        ResetValues();
    }

    private void HareketEt(float netarafa)
    {
        if (!karakterAnimasyon.GetBool("slide") && !karakterAnimasyon.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            player.velocity = new Vector2(netarafa * movementSpeed, player.velocity.y);
            //Hareket hızı çarpanı ile yatay eksende hareket sağlanıyor
        }
        if (slide && !karakterAnimasyon.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            karakterAnimasyon.SetBool("slide", true);
        }
        else if (!karakterAnimasyon.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            karakterAnimasyon.SetBool("slide", false);
        }

        karakterAnimasyon.SetFloat("speed", Mathf.Abs(netarafa));

        if (isGrounded && jump)
        {
            isGrounded = false;
            player.AddForce(new Vector2(0, jumpForce));
        }
    }
    private void HandleAttaks()
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
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

    private bool IsGrounded()
    {
        if (player.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void ResetValues()
    {
        attack = false;
        slide = false;
        jump = false;
    }
}
