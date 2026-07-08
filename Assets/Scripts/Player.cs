


using UnityEngine;

public class Player : MonoBehaviour
{
    public float HorizontalMovement;
    public float VerticalMovement;
    public float Speed;
    public float Health;
    public float VidaMaxima;
    public Armas currentWeapon;


    public Animator anim;
    public bool moving;



    void Start()
    {
        anim.SetBool("Morir", false);

    }


    void Update()
    {
        MovementPlayer();
        Morir();
   




    }
    public void MovementPlayer()
    {
        Debug.Log("player try to move");


        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        Vector3 dir = new Vector3(x, y, 0);
        dir.Normalize();
        float i = transform.position.y;
        float e = transform.position.x;

        transform.position += dir * Speed * Time.deltaTime;

        if (dir.magnitude > 0.1f || dir.magnitude < -0.1f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        if (moving)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);

        }

        anim.SetBool("Moving", moving);
       
        if (currentWeapon.Hand == Armas.TiposDeArma.Melee)
        {
            if(Input.GetMouseButtonDown(0))
            {
                
                anim.SetTrigger("Atacar");
            }
        }
    }

    public void Morir()
    {    
        if (Health <= 0)
        {
            anim.SetBool("Morir",true);
            Debug.Log("Player is dead");
            Destroy(gameObject, 1.1f);
        }
    }
    
}