using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {



    [SerializeField]
    private int size;
	private Transform astar;
    private Transform chunk;
    private GameObject instantiateObj;
    public Vector2 hitPoint {get; set;}

	// Use this for initialization
	void Start () {
		astar = GameObject.Find ("A*").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int GetSize() {
        return this.size;
    }

    public void Divide() {
        chunk = this.transform.parent.transform;
        int countOfChildChunk = this.transform.parent.childCount;
        //Debug.Log("current chunk has:" + countOfChildChunk + " childs");
        if (size == 16) {
            GameObject obj = Resources.Load("Prefabs/BlockSize4") as GameObject;
            Destroy(this.gameObject);
            for (int i = -1; i < 2; i ++) {
                for (int j = -1; j < 2; j ++) {
                    if (i != 0) {
                        if (j != 0) {
                            instantiateObj = Instantiate(obj);
                            instantiateObj.transform.parent = chunk;
                            instantiateObj.transform.localPosition = new Vector3(i, j, 0);
                        }
                    }
                }
            }
        }
        if (size == 4) {
            GameObject obj = Resources.Load("Prefabs/BlockSize1") as GameObject;

            float min = this.transform.localPosition.x;
            float max = this.transform.localPosition.y;

            Debug.Log("min:" + min);
            Debug.Log("max:" + max);

            for (float i = -1; i < 2; i ++) {
                for (float j = -1; j < 2; j ++) {
                    if (i != 0) {
                        if (j != 0) {
                            instantiateObj = Instantiate(obj);
                            instantiateObj.transform.parent = chunk;

                            float finalX = (float)System.Math.Round(i /2 + min, 1);
                            float finalY = (float)System.Math.Round(j /2 + max, 1);
                            Debug.Log (finalX + ":" + finalY);
                            instantiateObj.transform.localPosition = new Vector3(finalX, finalY, 0);
                        }
                    }
                }
            }
            Destroy(this.gameObject);
        }

        if (size == 1) {
            Destroy(this.gameObject);
			astar.gameObject.GetComponent<AstarPath> ().Scan ();
        }
    }

    private void FindCollideBlock (Vector2 point) {
        foreach (Transform child in chunk.transform) {
            float shortTransformX = (float)System.Math.Round(child.transform.position.x - 1f, 1);
            float shortPointX = (float)System.Math.Round(point.x, 1);
            /*Debug.Log(shortTransformX - 1f);
            Debug.Log(shortPointX);*/
            //Debug.Log(child + ", position: " + child.transform.position);
            /*if (shortPointX.Equals(shortTransformX - 1)) {
                Debug.Log(child + " - ok");
            }*/
        }
    }


}
