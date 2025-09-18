using UnityEngine;

public class ControlDireccion : MonoBehaviour 
{
	public bool EsJugador1 = true;

	float Giro = 0;
	
	public bool Habilitado = true;
	CarController carController;
		
	//---------------------------------------------------------//
	
	// Use this for initialization
	void Start () 
	{
		carController = GetComponent<CarController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Giro = EsJugador1 ? CustomInputManager.inst.Axis1.x : CustomInputManager.inst.Axis2.x;
		carController.SetGiro(Giro);
	}

	public float GetGiro()
	{
		return Giro;
	}
	
}
