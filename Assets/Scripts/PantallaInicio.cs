using UnityEngine;
using UnityEngine.SceneManagement;


public class PantallaInicio : MonoBehaviour
{
    public string SiguienteEscena;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SiguienteEscena);
        }
    }
}
