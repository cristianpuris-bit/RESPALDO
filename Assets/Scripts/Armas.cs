using UnityEngine;

public class Armas : MonoBehaviour
{
    public enum TiposDeArma
    {
        None,
        Melee,
        OneHand,
        TwoHand,
    }
    public TiposDeArma Hand;
    public float Damage;
    public float Cooldown;
    public float LastShoot; 
    public GameObject BulletPreFab;
    public Transform Puntoataque;
    public float RadioAtaque;
    public LayerMask Enemy;
    public int Daño = 100;
    void Start()
    {
        

    }

    
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position);
        direction.z = 0;
        direction.Normalize();

        switch (Hand)
        {
            case TiposDeArma.None:
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        Hand = TiposDeArma.Melee;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        Hand = TiposDeArma.OneHand;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        Hand = TiposDeArma.TwoHand;
                    }
                }
                break;
            case TiposDeArma.Melee:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Collider2D[] colisiones = Physics2D.OverlapCircleAll(Puntoataque.position, RadioAtaque);
                        foreach (Collider2D colision in colisiones)
                        {
                            if (colision.gameObject.tag == "Enemy")
                            {
                                colision.GetComponent<Zombie>().TakeDamage(5f);
                            }
                            if (colision.gameObject.tag == "Enemy2")
                            {
                                colision.GetComponent<EnemyAmalgama>().TakeDamage(10f);
                            }
                            if (colision.gameObject.tag == "Enemy3")
                            {
                                colision.GetComponent<ManolargaEnemy>().TakeDamage(5f);
                            }
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        Hand = TiposDeArma.OneHand;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        Hand = TiposDeArma.TwoHand;
                    }
                }
                
                break;
            case TiposDeArma.OneHand:
                {
                    Cooldown = 1f;
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Time.time - LastShoot >= Cooldown)
                        {
                            GameObject bullet = Instantiate(BulletPreFab, transform.position, Quaternion.identity);
                            bullet.transform.up = direction;
                            
                            LastShoot = Time.time;
                        }
                       
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        Hand = TiposDeArma.Melee;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        Hand = TiposDeArma.TwoHand;
                    }
                }
                break;
            case TiposDeArma.TwoHand:
                {
                    Cooldown = 0.2f;
                    if (Input.GetMouseButton(0))
                    {
                        if (Time.time - LastShoot >= Cooldown)
                        {
                            GameObject bullet = Instantiate(BulletPreFab, transform.position, Quaternion.identity);
                            bullet.transform.up = direction;
                            LastShoot = Time.time;
                        }
                        
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        Hand = TiposDeArma.Melee;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        Hand = TiposDeArma.OneHand;
                    }


                }
                break;
            default:
                break;
        }
    }
}
