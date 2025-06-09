using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class PlantingGrassGrid: MonoBehaviour
{
    public GameObject GrassObjectPrefab1; // Cubo o lo que uses para visualizar
    public GameObject GrassObjectPrefab2;
    public Terrain terrain;             // El terreno sobre el cual se va a plantar
    public Transform jugadorOCamara;
    public float distanciaActivacionC = 20f; // Distancia a la que se activa el objeto
    private int contPlants = 0; // Contador de plantas para verificar el total plantado


    private void InstanciarPlantaConContenedor(Vector3 position, GameObject prefab)
    {
        // Crear el contenedor vacío
        GameObject contenedor = new GameObject("Contenedor_" + prefab.name);
        contenedor.transform.position = position;

        // Instanciar la planta como hijo directo del contenedor y en su posición final
        GameObject visual = Instantiate(prefab, position, Quaternion.identity, contenedor.transform);

        // Eliminar físicas si las tuviera
        DestroyImmediate(visual.GetComponent<Rigidbody>());
        DestroyImmediate(visual.GetComponent<Collider>());

        // Añadir script al contenedor (no al visual)
        var activador = contenedor.AddComponent<ActivadorPorProximidad>();
        activador.referencia = jugadorOCamara;
        activador.objetoVisual = visual;
        activador.distanciaActivacion = distanciaActivacionC;
        activador.chequearCadaFrame = true;

        contPlants++; // Contador global
    }


    public void GenerarPlantasZona(float inicioZ, float finZ, float pasoZ, float inicioX, float finX, float pasoX)
    {
        for (float ileraZ = inicioZ; ileraZ < finZ; ileraZ += pasoZ)
        {
            for (float ileraX = inicioX; ileraX < finX; ileraX += pasoX)
            {
                float randomValue = Random.Range(0, 3.5f);
                float randomX = Random.Range(-0.5f, 0.5f);
                float randomZ = Random.Range(-0.5f, 0.5f);

                float worldX = ileraX + randomX;
                float worldZ = ileraZ + randomZ;
                float worldY = terrain.SampleHeight(new Vector3(worldX, 0, worldZ));

                Vector3 basePosition = new Vector3(worldX, worldY + 0.1234f, worldZ);
                InstanciarPlantaConContenedor(basePosition, GrassObjectPrefab1);

                // Plantas adicionales
                int cantidadExtras = (randomValue < 1f) ? 1 :
                                     (randomValue < 2f) ? 2 :
                                     (randomValue < 3f) ? 3 : 0;

                for (int i = 0; i < cantidadExtras; i++)
                {
                    float extraX = Random.Range(inicioX, finX);
                    worldY = terrain.SampleHeight(new Vector3(extraX, 0, worldZ)); // Recalcular Y para la nueva posición
                    Vector3 extraPos = new Vector3(extraX, worldY + 0.1234f, worldZ);
                    InstanciarPlantaConContenedor(extraPos, GrassObjectPrefab2);
                }
            }
        }
    }


    void Start()
    {
        GenerarPlantasZona(1f, 499f, 1f, 1f, 20f, 1f);
        GenerarPlantasZona(1f, 499f, 1f, 103f, 107f, 1f);
        GenerarPlantasZona(1f, 499f, 1f, 200f, 204f, 1f);
        GenerarPlantasZona(1f, 499f, 1f, 297f, 301f, 1f);
        GenerarPlantasZona(1f, 499f, 1f, 397f, 401f, 1f);
        GenerarPlantasZona(1f, 499f, 1f, 481f, 500f, 1f);

        GenerarPlantasZona(1f, 19f, 1f, 1f, 499f, 1f);
        GenerarPlantasZona(248.5f, 252.5f, 1f, 1f, 499f, 1f);
        GenerarPlantasZona(482f, 492f, 1f, 1f, 499f, 1f);

        Debug.Log("Total de plantas de hierba colocadas: " + contPlants);

        //int contPlants = 0; // Contador de plantas para verificar el total plantado
        //for (float ileraZ = 1f; ileraZ < 499f; ileraZ = ileraZ + 1f)

        //{

        //    for (float ileraX = 1f; ileraX < 20f; ileraX = ileraX + 1f)
        //    {
        //        float randomValue = Random.Range(0, 3.5f); // Generar un valor aleatorio entre 0 y 3
        //        float randomValueX = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1
        //        float randomValueZ = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1

        //        float worldX = ileraX + randomValueX; // Coordenada X del cubo de prueba
        //        float worldZ = ileraZ + randomValueZ; // Coordenada Z del cubo de prueba
        //        float worldY = terrain.SampleHeight(new Vector3(worldX, 0, worldZ)); // Altura del terreno en esa posición

        //        Vector3 position = new Vector3(worldX, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //        GameObject grass1 = Instantiate(GrassObjectPrefab1, position, Quaternion.identity); // Instanciar el cubo de prueba

        //        DestroyImmediate(grass1.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //        DestroyImmediate(grass1.GetComponent<Collider>());

        //        // Añadir script de activación por proximidad
        //        var script = grass1.AddComponent<ActivadorPorProximidad>();
        //        script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //        script.distanciaActivacion = distanciaActivacionC;
        //        script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        contPlants++; // Incrementar el contador de plantas

        //        if (randomValue < 1f) // Si el valor aleatorio es menor que 1, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(1f, 20f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            // Añadir script de activación por proximidad
        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 2f) // Si el valor aleatorio es menor que 2, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(1f, 20f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            float randomPos2 = Random.Range(1f, 20f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(randomPos2, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 3f)
        //        {
        //            float randomPos1 = Random.Range(1f, 20f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());
        //            float randomPos2 = Random.Range(1f, 20f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(randomPos2, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());
        //            float randomPos3 = Random.Range(1f, 20f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition3 = new Vector3(randomPos3, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass4 = Instantiate(GrassObjectPrefab2, randomPosition3, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass4.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass4.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script3 = grass4.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        contPlants++; // Incrementar el contador de plantas
        //    }

        //    for (float ileraX = 103f; ileraX < 107f; ileraX = ileraX + 1f)
        //    {
        //        float randomValue = Random.Range(0, 3.5f); // Generar un valor aleatorio entre 0 y 3
        //        float randomValueX = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1
        //        float randomValueZ = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1

        //        float worldX = ileraX + randomValueX; // Coordenada X del cubo de prueba
        //        float worldZ = ileraZ + randomValueZ; // Coordenada Z del cubo de prueba
        //        float worldY = terrain.SampleHeight(new Vector3(worldX, 0, worldZ)); // Altura del terreno en esa posición

        //        Vector3 position = new Vector3(worldX, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //        GameObject grass1 = Instantiate(GrassObjectPrefab1, position, Quaternion.identity); // Instanciar el cubo de prueba

        //        DestroyImmediate(grass1.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //        DestroyImmediate(grass1.GetComponent<Collider>());

        //        // Añadir script de activación por proximidad
        //        var script = grass1.AddComponent<ActivadorPorProximidad>();
        //        script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //        script.distanciaActivacion = distanciaActivacionC;
        //        script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        if (randomValue < 1f) // Si el valor aleatorio es menor que 1, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(103f, 107f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        }

        //        if (randomValue < 2f) // Si el valor aleatorio es menor que 2, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(103f, 107f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            float randomPos2 = Random.Range(103f, 107f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(randomPos2, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 3f)
        //        {
        //            float randomPos1 = Random.Range(103f, 107f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());
        //            float randomPos2 = Random.Range(103f, 107f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(randomPos2, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());
        //            float randomPos3 = Random.Range(103f, 107f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition3 = new Vector3(randomPos3, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass4 = Instantiate(GrassObjectPrefab2, randomPosition3, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass4.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass4.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script3 = grass4.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        contPlants++; // Incrementar el contador de plantas
        //    }

        //    for (float ileraX = 200f; ileraX < 204; ileraX = ileraX + 1f)
        //    {
        //        float randomValue = Random.Range(0, 3.5f); // Generar un valor aleatorio entre 0 y 3
        //        float randomValueX = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1
        //        float randomValueZ = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1

        //        float worldX = ileraX + randomValueX; // Coordenada X del cubo de prueba
        //        float worldZ = ileraZ + randomValueZ; // Coordenada Z del cubo de prueba
        //        float worldY = terrain.SampleHeight(new Vector3(worldX, 0, worldZ)); // Altura del terreno en esa posición

        //        Vector3 position = new Vector3(worldX, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //        GameObject grass1 = Instantiate(GrassObjectPrefab1, position, Quaternion.identity); // Instanciar el cubo de prueba

        //        DestroyImmediate(grass1.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //        DestroyImmediate(grass1.GetComponent<Collider>());

        //        // Añadir script de activación por proximidad
        //        var script = grass1.AddComponent<ActivadorPorProximidad>();
        //        script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //        script.distanciaActivacion = distanciaActivacionC;
        //        script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        if (randomValue < 1f) // Si el valor aleatorio es menor que 1, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(200f, 204); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        }

        //        if (randomValue < 2f) // Si el valor aleatorio es menor que 2, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(200f, 204); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            float randomPos2 = Random.Range(200f, 204); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(randomPos2, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 3f)
        //        {
        //            float randomPos1 = Random.Range(200f, 204); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());
        //            float randomPos2 = Random.Range(200f, 204); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(randomPos2, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());
        //            float randomPos3 = Random.Range(200f, 204); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition3 = new Vector3(randomPos3, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass4 = Instantiate(GrassObjectPrefab2, randomPosition3, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass4.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass4.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script3 = grass4.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        contPlants++; // Incrementar el contador de plantas
        //    }

        //    for (float ileraX = 297f; ileraX < 301f; ileraX = ileraX + 1f)
        //    {
        //        float randomValue = Random.Range(0, 3.5f); // Generar un valor aleatorio entre 0 y 3
        //        float randomValueX = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1
        //        float randomValueZ = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1

        //        float worldX = ileraX + randomValueX; // Coordenada X del cubo de prueba
        //        float worldZ = ileraZ + randomValueZ; // Coordenada Z del cubo de prueba
        //        float worldY = terrain.SampleHeight(new Vector3(worldX, 0, worldZ)); // Altura del terreno en esa posición

        //        Vector3 position = new Vector3(worldX, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //        GameObject grass1 = Instantiate(GrassObjectPrefab1, position, Quaternion.identity); // Instanciar el cubo de prueba

        //        DestroyImmediate(grass1.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //        DestroyImmediate(grass1.GetComponent<Collider>());

        //        // Añadir script de activación por proximidad
        //        var script = grass1.AddComponent<ActivadorPorProximidad>();
        //        script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //        script.distanciaActivacion = distanciaActivacionC;
        //        script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        if (randomValue < 1f) // Si el valor aleatorio es menor que 1, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(297f, 301f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        }

        //        if (randomValue < 2f) // Si el valor aleatorio es menor que 2, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(297f, 301f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            float randomPos2 = Random.Range(297f, 301f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(randomPos2, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 3f)
        //        {
        //            float randomPos1 = Random.Range(297f, 301f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());
        //            float randomPos2 = Random.Range(297f, 301f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(randomPos2, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());
        //            float randomPos3 = Random.Range(297f, 301f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition3 = new Vector3(randomPos3, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass4 = Instantiate(GrassObjectPrefab2, randomPosition3, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass4.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass4.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script3 = grass4.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        contPlants++; // Incrementar el contador de plantas
        //    }

        //    for (float ileraX = 397f; ileraX < 401f; ileraX = ileraX + 1f)
        //    {
        //        float randomValue = Random.Range(0, 3.5f); // Generar un valor aleatorio entre 0 y 3
        //        float randomValueX = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1
        //        float randomValueZ = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1

        //        float worldX = ileraX + randomValueX; // Coordenada X del cubo de prueba
        //        float worldZ = ileraZ + randomValueZ; // Coordenada Z del cubo de prueba
        //        float worldY = terrain.SampleHeight(new Vector3(worldX, 0, worldZ)); // Altura del terreno en esa posición

        //        Vector3 position = new Vector3(worldX, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //        GameObject grass1 = Instantiate(GrassObjectPrefab1, position, Quaternion.identity); // Instanciar el cubo de prueba

        //        DestroyImmediate(grass1.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //        DestroyImmediate(grass1.GetComponent<Collider>());

        //        // Añadir script de activación por proximidad
        //        var script = grass1.AddComponent<ActivadorPorProximidad>();
        //        script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //        script.distanciaActivacion = distanciaActivacionC;
        //        script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        if (randomValue < 1f) // Si el valor aleatorio es menor que 1, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(397f, 401f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            // Añadir script de activación por proximidad
        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 2f) // Si el valor aleatorio es menor que 2, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(397f, 401f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            float randomPos2 = Random.Range(397f, 401f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(randomPos2, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 3f)
        //        {
        //            float randomPos1 = Random.Range(397f, 401f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());
        //            float randomPos2 = Random.Range(397f, 401f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(randomPos2, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());
        //            float randomPos3 = Random.Range(397f, 401f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition3 = new Vector3(randomPos3, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass4 = Instantiate(GrassObjectPrefab2, randomPosition3, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass4.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass4.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script3 = grass4.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        }
        //        contPlants++;
        //    }

        //    for (float ileraX = 481f; ileraX < 500f; ileraX = ileraX + 1f)
        //    {
        //        float randomValue = Random.Range(0, 3.5f); // Generar un valor aleatorio entre 0 y 3
        //        float randomValueX = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1
        //        float randomValueZ = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1

        //        float worldX = ileraX + randomValueX; // Coordenada X del cubo de prueba
        //        float worldZ = ileraZ + randomValueZ; // Coordenada Z del cubo de prueba
        //        float worldY = terrain.SampleHeight(new Vector3(worldX, 0, worldZ)); // Altura del terreno en esa posición

        //        Vector3 position = new Vector3(worldX, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //        GameObject grass1 = Instantiate(GrassObjectPrefab1, position, Quaternion.identity); // Instanciar el cubo de prueba

        //        DestroyImmediate(grass1.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //        DestroyImmediate(grass1.GetComponent<Collider>());

        //        // Añadir script de activación por proximidad
        //        var script = grass1.AddComponent<ActivadorPorProximidad>();
        //        script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //        script.distanciaActivacion = distanciaActivacionC;
        //        script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        if (randomValue < 1f) // Si el valor aleatorio es menor que 1, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(481f, 500f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            // Añadir script de activación por proximidad
        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 2f) // Si el valor aleatorio es menor que 2, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(481f, 500f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            float randomPos2 = Random.Range(481f, 500f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(randomPos2, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 3f)
        //        {
        //            float randomPos1 = Random.Range(481f, 500f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(randomPos1, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());
        //            float randomPos2 = Random.Range(481f, 500f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(randomPos2, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());
        //            float randomPos3 = Random.Range(481f, 500f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition3 = new Vector3(randomPos3, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //            GameObject grass4 = Instantiate(GrassObjectPrefab2, randomPosition3, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass4.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass4.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script3 = grass4.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }
        //        contPlants++;
        //    }    
        //}

        //for (float ileraX = 1f; ileraX < 499f; ileraX = ileraX + 1f)
        //{
        //    for (float ileraZ = 1f; ileraZ < 19f; ileraZ = ileraZ + 1f)
        //    {
        //        float randomValue = Random.Range(0, 3.5f); // Generar un valor aleatorio entre 0 y 3
        //        float randomValueX = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1
        //        float randomValueZ = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1

        //        float worldX = ileraX + randomValueX; // Coordenada X del cubo de prueba
        //        float worldZ = ileraZ + randomValueZ; // Coordenada Z del cubo de prueba
        //        float worldY = terrain.SampleHeight(new Vector3(worldX, 0, worldZ)); // Altura del terreno en esa posición

        //        Vector3 position = new Vector3(worldX, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //        GameObject grass1 = Instantiate(GrassObjectPrefab1, position, Quaternion.identity); // Instanciar el cubo de prueba

        //        DestroyImmediate(grass1.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //        DestroyImmediate(grass1.GetComponent<Collider>());

        //        // Añadir script de activación por proximidad
        //        var script = grass1.AddComponent<ActivadorPorProximidad>();
        //        script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //        script.distanciaActivacion = distanciaActivacionC;
        //        script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        if (randomValue < 1f) // Si el valor aleatorio es menor que 1, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(1f, 19f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(worldX, worldY + 0.1234f, randomPos1); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            // Añadir script de activación por proximidad
        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 2f) // Si el valor aleatorio es menor que 2, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(1f, 19f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(worldX, worldY + 0.1234f, randomPos1); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            float randomPos2 = Random.Range(1f, 19f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(worldX, worldY + 0.1234f, randomPos2); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 3f)
        //        {
        //            float randomPos1 = Random.Range(1f, 19f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(worldX, worldY + 0.1234f, randomPos1); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());
        //            float randomPos2 = Random.Range(1f, 19f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(worldX, worldY + 0.1234f, randomPos2); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());
        //            float randomPos3 = Random.Range(1f, 19f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition3 = new Vector3(worldX, worldY + 0.1234f, randomPos3); // Ajustar la posición del cubo
        //            GameObject grass4 = Instantiate(GrassObjectPrefab2, randomPosition3, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass4.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass4.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script3 = grass4.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }
        //        contPlants++;
        //    }

        //    for (float ileraZ = 248.5f; ileraZ < 252.5f; ileraZ = ileraZ + 1f)
        //    {
        //        float randomValue = Random.Range(0, 3.5f); // Generar un valor aleatorio entre 0 y 3
        //        float randomValueX = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1
        //        float randomValueZ = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1

        //        float worldX = ileraX + randomValueX; // Coordenada X del cubo de prueba
        //        float worldZ = ileraZ + randomValueZ; // Coordenada Z del cubo de prueba
        //        float worldY = terrain.SampleHeight(new Vector3(worldX, 0, worldZ)); // Altura del terreno en esa posición

        //        Vector3 position = new Vector3(worldX, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //        GameObject grass1 = Instantiate(GrassObjectPrefab1, position, Quaternion.identity); // Instanciar el cubo de prueba

        //        DestroyImmediate(grass1.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //        DestroyImmediate(grass1.GetComponent<Collider>());

        //        // Añadir script de activación por proximidad
        //        var script = grass1.AddComponent<ActivadorPorProximidad>();
        //        script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //        script.distanciaActivacion = distanciaActivacionC;
        //        script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        if (randomValue < 1f) // Si el valor aleatorio es menor que 1, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(248.5f, 252.5f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(worldX, worldY + 0.1234f, randomPos1); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            // Añadir script de activación por proximidad
        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 2f) // Si el valor aleatorio es menor que 2, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(248.5f, 252.5f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(worldX, worldY + 0.1234f, randomPos1); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            float randomPos2 = Random.Range(248.5f, 252.5f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(worldX, worldY + 0.1234f, randomPos2); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 3f)
        //        {
        //            float randomPos1 = Random.Range(248.5f, 252.5f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(worldX, worldY + 0.1234f, randomPos1); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());
        //            float randomPos2 = Random.Range(248.5f, 252.5f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(worldX, worldY + 0.1234f, randomPos2); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());
        //            float randomPos3 = Random.Range(248.5f, 252.5f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition3 = new Vector3(worldX, worldY + 0.1234f, randomPos3); // Ajustar la posición del cubo
        //            GameObject grass4 = Instantiate(GrassObjectPrefab2, randomPosition3, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass4.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass4.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script3 = grass4.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }
        //        contPlants++;
        //    }

        //    for (float ileraZ = 482f; ileraZ < 492f; ileraZ = ileraZ + 1f)
        //    {
        //        float randomValue = Random.Range(0, 3.5f); // Generar un valor aleatorio entre 0 y 3
        //        float randomValueX = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1
        //        float randomValueZ = Random.Range(-0.5f, 0.5f); // Generar un valor aleatorio entre 0 y 1

        //        float worldX = ileraX + randomValueX; // Coordenada X del cubo de prueba
        //        float worldZ = ileraZ + randomValueZ; // Coordenada Z del cubo de prueba
        //        float worldY = terrain.SampleHeight(new Vector3(worldX, 0, worldZ)); // Altura del terreno en esa posición

        //        Vector3 position = new Vector3(worldX, worldY + 0.1234f, worldZ); // Ajustar la posición del cubo
        //        GameObject grass1 = Instantiate(GrassObjectPrefab1, position, Quaternion.identity); // Instanciar el cubo de prueba

        //        DestroyImmediate(grass1.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //        DestroyImmediate(grass1.GetComponent<Collider>());

        //        // Añadir script de activación por proximidad
        //        var script = grass1.AddComponent<ActivadorPorProximidad>();
        //        script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //        script.distanciaActivacion = distanciaActivacionC;
        //        script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //        if (randomValue < 1f) // Si el valor aleatorio es menor que 1, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(482f, 492f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(worldX, worldY + 0.1234f, randomPos1); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            // Añadir script de activación por proximidad
        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 2f) // Si el valor aleatorio es menor que 2, plantar el cubo
        //        {
        //            float randomPos1 = Random.Range(482f, 492f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(worldX, worldY + 0.1234f, randomPos1); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());

        //            float randomPos2 = Random.Range(482f, 492f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(worldX, worldY + 0.1234f, randomPos2); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba

        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }

        //        if (randomValue < 3f)
        //        {
        //            float randomPos1 = Random.Range(482f, 492f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition1 = new Vector3(worldX, worldY + 0.1234f, randomPos1); // Ajustar la posición del cubo
        //            GameObject grass2 = Instantiate(GrassObjectPrefab2, randomPosition1, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass2.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass2.GetComponent<Collider>());
        //            float randomPos2 = Random.Range(482f, 492f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition2 = new Vector3(worldX, worldY + 0.1234f, randomPos2); // Ajustar la posición del cubo
        //            GameObject grass3 = Instantiate(GrassObjectPrefab2, randomPosition2, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass3.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass3.GetComponent<Collider>());
        //            float randomPos3 = Random.Range(482f, 492f); // Generar una posición aleatoria para el cubo
        //            Vector3 randomPosition3 = new Vector3(worldX, worldY + 0.1234f, randomPos3); // Ajustar la posición del cubo
        //            GameObject grass4 = Instantiate(GrassObjectPrefab2, randomPosition3, Quaternion.identity); // Instanciar el cubo de prueba
        //            DestroyImmediate(grass4.GetComponent<Rigidbody>()); // Asegurarse de que no tenga físicas
        //            DestroyImmediate(grass4.GetComponent<Collider>());

        //            var script1 = grass2.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script2 = grass3.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente

        //            var script3 = grass4.AddComponent<ActivadorPorProximidad>();
        //            script.referencia = jugadorOCamara; // Asignalo desde tu clase principal
        //            script.distanciaActivacion = distanciaActivacionC;
        //            script.chequearCadaFrame = true; // true si querés que cambie dinámicamente
        //        }
        //        contPlants++;
        //    }
        //}
    }
}