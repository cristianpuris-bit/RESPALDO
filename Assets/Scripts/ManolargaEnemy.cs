using UnityEngine;



public class ManolargaEnemy : MonoBehaviour
{
    public enum EnemyAttackType
    {
        None,
        Meele,
        Range
    }
    public enum EnemyState
    {
        None,
        Idle,
        Chase,
        Flee,
        Attack,
        
    }
    public EnemyAttackType attackType;
    public EnemyState state;
    
    public float Speed = 5f;
    GameObject target;
    public float radiusattack = 1f;
    public float radiusMovement = 2.5f;
    public float RadiusFlee = 5f;
    public float Health = 250f;

    public float currentTime = 0f;

    public bool IsAbleToAttack = true;
    public float MaxTime = 2f;
    public float damage = 5f;
    public float ResetTime = 0f;

    public bool IsAbleToUseSpecialAbility = true;
    public float SpecialabilityReset = 0f;
    public float MaxAbilityTime = 4f;
    public float SpecialAbilityDamage = 20f;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }


    void Update()
    {
       
       EnemyStateChanger();

    }

    void EnemyStateChanger()
    {
        if (target == null) return;
        Vector3 targetPos = target.transform.position;
        Vector3 myPos = transform.position;

        switch (state)
        {
            case EnemyState.None:
                {
                    state = EnemyState.Idle;
                    print("esto no deberia pasar");
                    break;
                }
            case EnemyState.Idle:
                {
                    //print(Vector3.Distance(targetPos, myPos));

                    if (Vector3.Distance(targetPos, myPos) <= RadiusFlee)
                        {
                            state = EnemyState.Flee;
                            Debug.Log("escapando");
                            break;
                        }

                    if (Vector3.Distance(targetPos, myPos) <= radiusMovement)
                        {
                            state = EnemyState.Chase;
                            Debug.Log("siguiendo");
                            break;
                        }
                    break;
                }
                

            case EnemyState.Chase:
                {
                    print("hi");
                    Vector3 direction = (targetPos - myPos).normalized;
                    transform.position += direction * Speed * Time.deltaTime;


                    if (Vector3.Distance(targetPos, myPos) > radiusMovement) state = EnemyState.Flee;

                    if (Vector3.Distance(targetPos, myPos) < radiusattack) state = EnemyState.Attack;
                }
                break;

            case EnemyState.Flee:
                {
                    if (Vector3.Distance(targetPos, myPos) < RadiusFlee)
                    {
                        Vector3 direction = (targetPos - myPos).normalized;
                        transform.position -= direction * Speed * Time.deltaTime;
                    }

                    if (Vector3.Distance(targetPos, myPos) > RadiusFlee)
                    {
                        print("volviendo a idle");
                        state = EnemyState.Idle;
                    }

                    if (Vector3.Distance(targetPos, myPos) < radiusMovement)
                    {
                        state = EnemyState.Chase;
                    }

                    if (Vector3.Distance(targetPos, myPos) < RadiusFlee && Vector3.Distance(targetPos, myPos) > radiusMovement)
                    {
                        state = EnemyState.Attack;
                    }
                }
                break;

            case EnemyState.Attack:
                {
                    if (Vector3.Distance(targetPos, myPos) < radiusattack)
                    {
                        if (IsAbleToAttack)
                        {

                            Debug.Log("Atacando");
                            target.GetComponent<Player>().Health -= damage;
                            IsAbleToAttack = false;
                        }
                        currentTime += Time.deltaTime;
                        if (currentTime >= MaxTime)
                        {
                            IsAbleToAttack = true;

                            currentTime = 0;
                        }
                    }


                    SpecialabilityReset += Time.deltaTime;
                    if (SpecialabilityReset >= MaxAbilityTime)
                    {

                        IsAbleToUseSpecialAbility = true;

                        SpecialabilityReset = 0;
                    }

                    if (IsAbleToUseSpecialAbility == true)
                    {
                        SpecialabilityReset += Time.deltaTime;
                        if (SpecialabilityReset >= MaxAbilityTime)
                        {

                            IsAbleToUseSpecialAbility = true;

                            SpecialabilityReset = 0;
                        }

                        target.GetComponent<Player>().Health -= SpecialAbilityDamage;
                        IsAbleToUseSpecialAbility = false;
                        Debug.Log("toma toma te pego piu piu");


                    }

                    if (Vector3.Distance(targetPos, myPos) > radiusattack)
                        state = EnemyState.Flee;
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

    public void CheckMainState()
    {
        switch (attackType)
        {
            case EnemyAttackType.None:
                break;
            case EnemyAttackType.Meele:


                MeeleType();

                break;
            case EnemyAttackType.Range:

                RangeType();
                break;
        }
    }

    public void MeeleType()
    {

    }
    public void RangeType()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusattack);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radiusMovement);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadiusFlee);
    }
}