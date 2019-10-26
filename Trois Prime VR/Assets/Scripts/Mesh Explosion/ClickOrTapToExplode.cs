using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class ClickOrTapToExplode : MonoBehaviour {
	
	
#if UNITY_EDITOR || (!UNITY_EDITOR && !(UNITY_IPHONE || UNITY_ANDROID))
	void OnMouseDown() {
		StartExplosion();
	}
#endif
	private Vector3 _pos;
	private GameObject _prefab;
	void Start(){
         Scene scene = SceneManager.GetActiveScene();
		 if(scene.name=="VR-4")
		 {
		_pos = this.transform.position;
		_prefab = this.gameObject;
		}

	}
	void Update() {
         Scene scene = SceneManager.GetActiveScene();
		 if(scene.name=="VR-4")
		 {
		this.gameObject.transform.LookAt(GameObject.Find("Player").transform);
		this.gameObject.transform.Translate(transform.forward*Time.deltaTime*1f);
		 }
		foreach (var i in Input.touches) {
			if (i.phase != TouchPhase.Began) {
				continue;
			}
			
			// It's kinda wasteful to do this raycast repeatedly for every ClickToExplode in the
			// scene, but since this component is just for testing I don't think it's worth the
			// bother to figure out some shared static solution.
			RaycastHit hit;
			if (!Physics.Raycast(Camera.main.ScreenPointToRay(i.position), out hit)) {
				continue;
			}
			if (hit.collider != GetComponent<Collider>()) {
				continue;
			}
			
			StartExplosion();
			return;
		}
	}
	
	void StartExplosion() {
		BroadcastMessage("Explode");
		Instantiate(_prefab,_pos,Quaternion.identity);
		GameObject.Destroy(gameObject);
	}
	
}
