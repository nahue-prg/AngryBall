using System.Collections;
using UnityEngine;

namespace Assets.Codigo
{
    public class Bola : MonoBehaviour
    {
        public float ballMass;
        public float ballSpeed;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Cubo"))
            {
                ballMass = GetComponent<Rigidbody>().mass;
                ballSpeed = GetComponent<Rigidbody>().velocity.magnitude;
                // Aplicar la explosión aquí
            }
        }
    }
}