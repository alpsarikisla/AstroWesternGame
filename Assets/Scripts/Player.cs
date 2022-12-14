using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D player;//Karakterimiz
    [SerializeField]//Unity i?erisinden kontrol edilebilir oluyor
    private float movementSpeed;//HareketH?z?
    private Animator karakterAnimasyon;
    private bool sagabak;
    private bool attack;
    private bool slide;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    void Start()
    {
        sagabak = true;
        //Karakterimizi Rigidbody olarak yani fiziksel bir nesne olarak tan?mlad?k
        player = GetComponent<Rigidbody2D>();
        karakterAnimasyon = GetComponent<Animator>();
    }
    private void Update()
    {
        HandleInput();
    }
    void FixedUpdate()
    {
        //Karakterimizin yatay eksende hareket edebilece?ini tan?mlad?k
        float yon = Input.GetAxis("Horizontal");//Yatay eksende sa?a git komutu verilirse(D'ye bas?l?rsa) 1 Sola git komutu verilerise (A'ya Bas?l?rsa) -1 getirir
        isGrounded = IsGrounded();

        HareketEt(yon);
        Don(yon);
        HandleAttack();
        HandleLayers();
        ResetValues();
    }

    private void HareketEt(float netarafa)
    {
        if(player.velocity.y < 0)
        {
            karakterAnimasyon.SetBool("land", true);
        }
        if (!karakterAnimasyon.GetBool("slide") && !karakterAnimasyon.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
        {
            player.velocity = new Vector2(netarafa * movementSpeed, player.velocity.y);
            //Hareket h?z? ?arpan? ile yatay eksende hareket sa?lan?yor
        }
        if (isGrounded && jump)
        {
            isGrounded = false;
            player.AddForce(new Vector2(0, jumpForce));
            karakterAnimasyon.SetTrigger("jump");
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
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
        jump = false;
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
                        karakterAnimasyon.ResetTrigger("jump");
                        karakterAnimasyon.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void HandleLayers()
    {
        if (!isGrounded)
        {
            karakterAnimasyon.SetLayerWeight(1, 1);
        }
        else
        {
            karakterAnimasyon.SetLayerWeight(1, 0);
        }
    }
}
