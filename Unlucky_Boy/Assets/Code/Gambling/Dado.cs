using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dado : MonoBehaviour
{
    [SerializeField] float _torqueMinimun = .5f;
    [SerializeField] float _torqueMaximun = 3f;
    [SerializeField] float _throwStrength = 1f;
    private Rigidbody _rB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Obtenemos el rigidbody del objeto
        _rB = GetComponent<Rigidbody>();
    }
    public void TirarDados()
    {
        Debug.Log("GAAAMBLING");
        //Aplicamos una fuerza hacia arriba con el float de _trhowStrength
        _rB.AddForce(Vector3.up * _throwStrength, ForceMode.Impulse);
        //Le aplicamos una rotación hacia adelante, arriba y un poco a la derecha con un random range
        _rB.AddTorque(transform.forward * Random.Range(_torqueMinimun, _torqueMaximun) + transform.up * Random.Range(_torqueMinimun, _torqueMaximun) + transform.right * Random.Range(_torqueMinimun, _torqueMaximun));
        StartCoroutine(WaitForStop());
    }
    IEnumerator WaitForStop()
    {
        //Esperamos hasta el siguiente FixedUpdate
        yield return new WaitForFixedUpdate();
        //Mientras el objeto siga girando esperará a que empiece el siguiente FixedUpdate
        while(_rB.angularVelocity.sqrMagnitude > 0.5)
        {
            yield return new WaitForFixedUpdate();
        }
        //Comprobar numero
        CheckSize.Instance.ComprobarNumero();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TirarDados();
        }   
    }
}
