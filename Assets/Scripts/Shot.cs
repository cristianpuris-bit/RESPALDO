using UnityEngine;

public class Shot : MonoBehaviour
{
    public float speed = 15;
    public float TimeAlive = 3;

    void Start()
    {
        Destroy(gameObject, TimeAlive);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Zombie enemigo = collision.GetComponent<Zombie>();
            

            if (enemigo != null)
            {
                enemigo.TakeDamage(25);
                Destroy(gameObject);
            }
        }
        if (collision.tag == "Enemy2")
        {
            EnemyAmalgama enemigo = collision.GetComponent<EnemyAmalgama>();


            if (enemigo != null)
            {
                enemigo.TakeDamage(25);
                Destroy(gameObject);
            }
        }
        if (collision.tag == "Enemy3")
        {
            ManolargaEnemy enemigo = collision.GetComponent<ManolargaEnemy>();


            if (enemigo != null)
            {
                enemigo.TakeDamage(25);
                Destroy(gameObject);
            }
        }
    }
}