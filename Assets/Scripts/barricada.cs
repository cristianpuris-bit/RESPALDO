using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sistema de barricadas para Lima Z.
/// Controla la colocación, reparación, desgaste y destrucción de barricadas
/// construidas por el jugador para ralentizar a los zombies.
/// </summary>
public class BarricadeManager : MonoBehaviour
{
    // ---------- Tipos de barricada ----------
    public enum TipoBarricada
    {
        Madera,
        Metal,
        Reforzada
    }

    [System.Serializable]
    public class Barricada
    {
        public TipoBarricada tipo;
        public GameObject objeto;
        public float vidaMaxima;
        public float vidaActual;
        public bool destruida;
    }

    [Header("Recursos del jugador")]
    public int chatarra = 50;
    public int metal = 20;

    [Header("Barricadas colocadas")]
    public List<Barricada> barricadasActivas = new List<Barricada>();

    [Header("Prefabs de barricada")]
    public GameObject prefabMadera;
    public GameObject prefabMetal;
    public GameObject prefabReforzada;

    [Header("Configuración de desgaste")]
    public float intervaloDesgaste = 2f;   // cada cuántos segundos se desgasta una barricada
    public float desgastePorIntervalo = 5f;

    void Start()
    {
        // Inicia la corrutina que desgasta las barricadas con el tiempo
        StartCoroutine(DesgasteBarricadasRoutine());
    }

    // =========================================================
    // 1. SWITCH: obtener costo y vida según el tipo de barricada
    // =========================================================
    private (int costoChatarra, int costoMetal, float vida) ObtenerDatosBarricada(TipoBarricada tipo)
    {
        switch (tipo)
        {
            case TipoBarricada.Madera:
                return (costoChatarra: 10, costoMetal: 0, vida: 50f);

            case TipoBarricada.Metal:
                return (costoChatarra: 5, costoMetal: 15, vida: 120f);

            case TipoBarricada.Reforzada:
                return (costoChatarra: 15, costoMetal: 25, vida: 220f);

            default:
                Debug.LogWarning("Tipo de barricada no reconocido, usando valores de Madera por defecto.");
                return (10, 0, 50f);
        }
    }

    // =========================================================
    // 2. IF: validar recursos antes de construir una barricada
    // =========================================================
    public bool TryColocarBarricada(TipoBarricada tipo, Vector3 posicion)
    {
        var datos = ObtenerDatosBarricada(tipo);

        if (chatarra < datos.costoChatarra || metal < datos.costoMetal)
        {
            Debug.Log("No hay suficientes recursos para construir esta barricada.");
            return false;
        }

        // Descuenta recursos
        chatarra -= datos.costoChatarra;
        metal -= datos.costoMetal;

        GameObject prefab = ObtenerPrefab(tipo);
        GameObject instancia = Instantiate(prefab, posicion, Quaternion.identity);

        Barricada nueva = new Barricada
        {
            tipo = tipo,
            objeto = instancia,
            vidaMaxima = datos.vida,
            vidaActual = datos.vida,
            destruida = false
        };

        barricadasActivas.Add(nueva);
        Debug.Log($"Barricada de tipo {tipo} colocada. Vida: {nueva.vidaActual}");
        return true;
    }

    private GameObject ObtenerPrefab(TipoBarricada tipo)
    {
        switch (tipo)
        {
            case TipoBarricada.Madera: return prefabMadera;
            case TipoBarricada.Metal: return prefabMetal;
            case TipoBarricada.Reforzada: return prefabReforzada;
            default: return prefabMadera;
        }
    }

    // =========================================================
    // 3. FOREACH: reparar todas las barricadas activas
    // =========================================================
    public void RepararTodasLasBarricadas(float cantidad)
    {
        foreach (Barricada b in barricadasActivas)
        {
            if (b.destruida) continue; // no se repara lo ya destruido

            b.vidaActual += cantidad;

            if (b.vidaActual > b.vidaMaxima)
            {
                b.vidaActual = b.vidaMaxima;
            }
        }

        Debug.Log("Todas las barricadas activas fueron reparadas.");
    }

    // =========================================================
    // 4. FOR (recorrido inverso): eliminar barricadas destruidas
    // =========================================================
    public void LimpiarBarricadasDestruidas()
    {
        for (int i = barricadasActivas.Count - 1; i >= 0; i--)
        {
            if (barricadasActivas[i].destruida)
            {
                if (barricadasActivas[i].objeto != null)
                {
                    Destroy(barricadasActivas[i].objeto);
                }
                barricadasActivas.RemoveAt(i);
            }
        }
    }

    // =========================================================
    // 5. DO-WHILE: simular los golpes de un zombie contra una barricada
    //    hasta destruirla o hasta que el zombie se rinda (límite de intentos)
    // =========================================================
    public void ZombieAtacaBarricada(Barricada b, float danioPorGolpe, int intentosMaximos)
    {
        if (b == null || b.destruida)
        {
            return;
        }

        int intentos = 0;

        do
        {
            b.vidaActual -= danioPorGolpe;
            intentos++;

            Debug.Log($"Zombie golpea la barricada. Vida restante: {Mathf.Max(b.vidaActual, 0)}");

            if (b.vidaActual <= 0)
            {
                b.vidaActual = 0;
                b.destruida = true;
                Debug.Log("¡La barricada fue destruida!");
            }

        } while (!b.destruida && intentos < intentosMaximos);

        if (!b.destruida)
        {
            Debug.Log("El zombie se cansó de golpear la barricada y se retiró.");
        }
    }

    // =========================================================
    // 6. WHILE (dentro de una corrutina): desgaste pasivo con el tiempo
    // =========================================================
    private IEnumerator DesgasteBarricadasRoutine()
    {
        while (true) // se ejecuta durante toda la partida
        {
            yield return new WaitForSeconds(intervaloDesgaste);

            foreach (Barricada b in barricadasActivas)
            {
                if (b.destruida) continue;

                b.vidaActual -= desgastePorIntervalo;

                if (b.vidaActual <= 0)
                {
                    b.vidaActual = 0;
                    b.destruida = true;
                }
            }

            LimpiarBarricadasDestruidas();
        }
    }

    // =========================================================
    // Utilidad: contar barricadas activas por tipo (FOREACH + IF)
    // =========================================================
    public int ContarBarricadasPorTipo(TipoBarricada tipo)
    {
        int contador = 0;

        foreach (Barricada b in barricadasActivas)
        {
            if (!b.destruida && b.tipo == tipo)
            {
                contador++;
            }
        }

        return contador;
    }
}