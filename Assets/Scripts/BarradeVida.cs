using UnityEngine;
using UnityEngine.UI;


public class BarradeVida : MonoBehaviour
{
    public Slider Slider;
    public Player player;

    void Start()
    {
        
    }

    
    void Update()
    {
        CambiarVida(player.Health, player.VidaMaxima);
    }
public void CambiarVida(float VidaActual, float VidaMaxima)
    {
        Slider.value = VidaActual / VidaMaxima;
    }

      

}
