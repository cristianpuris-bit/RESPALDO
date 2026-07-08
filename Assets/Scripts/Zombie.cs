using UnityEngine;


public class Zombie : MonoBehaviour
{
    public enum EnemyState
    {
        None,
        Idle,
        Chase,
        Attack,

    }
    public EnemyState State;
    public float Speed = 5f;
    public GameObject target;
    public float radiusattack = 1f;
    public float radiusMovement = 5f;
    public float Health = 100f;
    public BarradeVida BarraVida;
    public bool IsAbleToAttack = true;
    public float MaxTime = 2f;
    public float damage = 5f;
    public float ResetTime = 0f;
    
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

        void Update()
    {
        if (target == null) return;
        Vector3 targetPos = target.transform.position;
        Vector3 myPos = transform.position;
        Vector3 direction = (targetPos - myPos).normalized;
        switch (State)
        {
            case EnemyState.None:
                State = EnemyState.Idle;
                break;

            case EnemyState.Idle:
                {
                    if (Vector3.Distance(targetPos, myPos) < radiusMovement)
                        State = EnemyState.Chase;
                }
                break;

            case EnemyState.Chase:
                {
                    
                    transform.position += direction * Speed * Time.deltaTime;


                    if (Vector3.Distance(targetPos, myPos) > radiusMovement)
                        State = EnemyState.Idle;

                    if (Vector3.Distance(targetPos, myPos) < radiusattack)
                        State = EnemyState.Attack;
                }
                break;
            case EnemyState.Attack:
                {
                    if (IsAbleToAttack)
                    {

                        Debug.Log("ven maldito");
                        target.GetComponent<Player>().Health -= damage;
                        IsAbleToAttack = false;
                        //BarraVida.CambiarVida(target.GetComponent<Player>().Health, target.GetComponent<Player>().VidaMaxima);
                    }
                    ResetTime += Time.deltaTime;
                    if (ResetTime >= MaxTime)
                    {
                        IsAbleToAttack = true;

                        ResetTime = 0;
                    }


                    if (Vector3.Distance(targetPos, myPos) > radiusattack)
                        State = EnemyState.Chase;
                }
                break;

            default:
                break;
        }
       
    }

    public void TakeDamage(float Cantidad)
    {
        Health -= Cantidad;
        if (Health <= 0)
        {  
            Destroy(gameObject);
        }
    }
}
