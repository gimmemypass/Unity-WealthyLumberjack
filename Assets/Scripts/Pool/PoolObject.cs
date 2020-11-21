using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for objects, which have to be added to the pool
public class PoolObject : MonoBehaviour
{
	#region Interface
	public void ReturnToPool()
	{
		gameObject.SetActive(false);
	}
	#endregion
}

