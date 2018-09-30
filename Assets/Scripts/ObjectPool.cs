using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	public RecycleGameObject prefab;

	private List<RecycleGameObject> instanceList = new List<RecycleGameObject>();

	private RecycleGameObject CreateInstance(Vector3 pos){

		var newObj = GameObject.Instantiate(prefab);
		newObj.transform.position = pos;
		newObj.transform.parent = transform;

		instanceList.Add(newObj);

		return newObj;      
	}

	public RecycleGameObject NextObject(Vector3 pos){
		RecycleGameObject nextObj = null;

		foreach(var obj in instanceList){
			if(obj.gameObject.activeSelf != true){
				nextObj = obj;
				nextObj.transform.position = pos;
			}
		}

        if(nextObj == null)
		    nextObj = CreateInstance(pos);
		
		nextObj.Restart();

		return nextObj;
	}

}
