using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float CameraSpeed;
    public float CameraDistance;
    public float LimiteInferior;
    public float LimiteSuperior;
    public float LimiteIzquierdo;
    public float LimiteDerecho;

    void Start()
    {
     
    }

    
    void Update()
    {
        CameraMovement();
    }
    public void CameraMovement()
    {
        if (target == null)
        {
            return;
        }

        Vector3 direction = (target.transform.position - transform.position);  
        direction.z = 0;
        direction.Normalize();
        if (Vector3.Distance (target.transform.position, transform.position) > 1 )
              transform.position += direction * CameraSpeed * Time.deltaTime;
        if   (transform.position.y < LimiteInferior)
        {
            transform.position = new Vector3 (transform.position.x, LimiteInferior, transform.position.z);
        }
        if (transform.position.y > LimiteSuperior )
        {
            transform.position = new Vector3(transform.position.x, LimiteSuperior, transform.position.z);
        }
        if (transform.position.x > LimiteDerecho)
        {
            transform.position = new Vector3(LimiteDerecho, transform.position.y, transform.position.z);
        }
        if (transform.position.x < LimiteIzquierdo)
        {
            transform.position = new Vector3(LimiteIzquierdo, transform.position.y, transform.position.z);
        }
    }





}
