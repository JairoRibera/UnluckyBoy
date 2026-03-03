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
        _rB = GetComponent<Rigidbody>();
    }
    public void TirarDados()
    {
        Debug.Log("GAAAMBLING");
        _rB.AddForce(Vector3.up * _throwStrength, ForceMode.Impulse);

        _rB.AddTorque(transform.forward * Random.Range(_torqueMinimun, _torqueMaximun) + transform.up * Random.Range(_torqueMinimun, _torqueMaximun) + transform.right * Random.Range(_torqueMinimun, _torqueMaximun));
        StartCoroutine(WaitForStop());
    }
    IEnumerator WaitForStop()
    {
        yield return new WaitForFixedUpdate();
        while(_rB.angularVelocity.sqrMagnitude > 0.1)
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
