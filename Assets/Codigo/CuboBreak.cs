using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboBreak : MonoBehaviour
{
    public float VidaBloque = 10f;
    public float explosionForce = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BolaTanque"))
        {
            try
            {
                float ballMass = collision.gameObject.GetComponent<Rigidbody>().mass;
                float ballSpeed = collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
                float fuerzaImpacto = (ballMass / 10 *  ballSpeed / 10);

                VidaBloque -= fuerzaImpacto;

                if (VidaBloque <= 0)
                {
                    // Dividir la caja original en 8 cubos más pequeños de igual tamaño
                    Vector3 originalScale = gameObject.transform.localScale;
                    Vector3 originalPosition = gameObject.transform.position;

                    float newCubeScale = originalScale.x / 2f;

                    for (int x = -1; x < 2; x++)
                    {
                        for (int y = -1; y < 2; y++)
                        {
                            for (int z = -1; z < 2; z++)
                            {
                                // Omitir el cubo original
                                if (x == 0 && y == 0 && z == 0)
                                {
                                    continue;
                                }

                                // Crear el nuevo cubo
                                GameObject smallCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                smallCube.transform.position = originalPosition + new Vector3(x * newCubeScale, y * newCubeScale, z * newCubeScale);
                                smallCube.transform.localScale = Vector3.one * newCubeScale;
                                smallCube.AddComponent<DestroyAfterTime>();
                                smallCube.AddComponent<BloqueRoto>();

                                // Agregar rigidbody y aplicar una fuerza de explosión
                                Rigidbody rb = smallCube.AddComponent<Rigidbody>();
                                rb.mass = 1f;
                                Vector3 forceDirection = (smallCube.transform.position - originalPosition).normalized * explosionForce;
                                rb.AddForce(forceDirection, ForceMode.Impulse);
                            }
                        }
                    }

                    // Destruir el cubo original
                    Destroy(gameObject);
                }
            }
            catch (System.Exception ex)
            {
                string Error = ex.Message;
                throw ex;
            }
        }
    }
}