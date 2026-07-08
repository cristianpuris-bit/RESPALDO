using UnityEngine;

public class SpawnRate : MonoBehaviour
{
    public enum EnemyType
    {
        None,
        Spawn,
        Zombie,
        Amalgama,
        ManoLarga
    }
    public EnemyType EnemySpawn;

    public GameObject ZombiePreFab;
    public GameObject ZombiePreFab2;
    public GameObject AmalgamaPreFab;
    public GameObject ManoLargaPreFab;
    public float ZombieTimer;
    public float AmalgamaTimer;
    public float ManoLargaTimer;
    public float range;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        Vector3 fullLenghtDir = randomDir * Random.Range(0f, range);

        switch (EnemySpawn)
        {
            case EnemyType.None:
                EnemySpawn = EnemyType.Spawn;
                break;
            case EnemyType.Spawn:
                {
                    ZombieTimer += Time.deltaTime;
                    if (ZombieTimer > 1)
                    {
                        EnemySpawn = EnemyType.Zombie;
                        ZombieTimer = 0f;
                    }
                    AmalgamaTimer += Time.deltaTime;
                    if (AmalgamaTimer > 5)
                    {
                        EnemySpawn = EnemyType.Amalgama;
                        AmalgamaTimer = 0f;
                    }
                    ManoLargaTimer += Time.deltaTime;
                    if (ManoLargaTimer > 3)
                    {
                        EnemySpawn = EnemyType.ManoLarga;
                        ManoLargaTimer = 0f;
                    }

                }
                break;
            case EnemyType.Zombie:
                {
                    
                    GameObject enemy = Instantiate(ZombiePreFab, transform.position, Quaternion.identity);
                    enemy.transform.position += fullLenghtDir;
                    GameObject enemy2 = Instantiate(ZombiePreFab2, transform.position, Quaternion.identity);
                    enemy2.transform.position += fullLenghtDir;
                    EnemySpawn = EnemyType.Spawn;
                }
                break;
            case EnemyType.Amalgama:
                {
                    
                    GameObject enemy = Instantiate(AmalgamaPreFab, transform.position, Quaternion.identity);
                    enemy.transform.position += fullLenghtDir;
                    EnemySpawn = EnemyType.Spawn;

                }
                break;
            case EnemyType.ManoLarga:
                {
                    
                    GameObject enemy = Instantiate(ManoLargaPreFab, transform.position, Quaternion.identity);
                    enemy.transform.position += fullLenghtDir;
                    EnemySpawn = EnemyType.Spawn;

                }
                break;
            default:
                {
                    break;
                }
        }
        
       
    }



}
