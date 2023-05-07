using UnityEngine;

public class Ca�on: MonoBehaviour
{
    public float fuerza = 1000f;
    public float sensibilidadX = 5f;
    public float sensibilidadY = 5f;
    public float smoothTime = 0.1f;
    public GameObject bola;
    public Transform ca�on;
    public Transform punta;
    private float rotX, rotY = 0f;
    private float limitex = 0, limiteY, contadorX, contadorY;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoTanque();
        DisparoTanque();
        limitex++;
    }

    private void MovimientoTanque()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

      

        rotX += mouseX * sensibilidadX;
        rotY -= mouseY * sensibilidadY;
        rotY = Mathf.Clamp(rotY, -14f, 10f); // limita la rotaci�n en el eje Y a -10 grados y 20 grados
        //Debug.Log(limitex+ " X :" + mouseX + " | Y :" + mouseY);
        //Debug.Log(limitex + " TRANSOFRM :" + transform.rotation+ " | CA�ON :" + ca�on.rotation);
        //rotX = Mathf.Clamp(rotX, -45f, 45f);
        //rotY = Mathf.Clamp(rotY, -30f, 30f);

        Quaternion targetRotation = Quaternion.Euler(rotY, rotX, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothTime);

        Quaternion targetCa�onRotation = Quaternion.Euler(rotY, rotX, 0f);
        ca�on.rotation = Quaternion.Lerp(ca�on.rotation, targetCa�onRotation, smoothTime);
    }

    private void DisparoTanque()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject InstanciaBola = Instantiate(bola, punta.transform.position, punta.rotation);
            Rigidbody rbNuevaBola = InstanciaBola.GetComponent<Rigidbody>();
            rbNuevaBola.useGravity = true;
            rbNuevaBola.AddForce(punta.forward * fuerza, ForceMode.Impulse);
            Destroy(InstanciaBola, 3f); // la bola desaparecer� despu�s de 3 segundos
        }
    }
}
