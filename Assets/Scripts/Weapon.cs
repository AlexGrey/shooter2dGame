using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    public float fireRate = 0;
    public float damage = 10f;
    public LayerMask whatToHit;

    bool canShoot = true;
    float timeToFire = 0;
    Transform firePoint;
    private int countShoot = 0;

	void Awake() {
        firePoint = transform.FindChild("FirePoint");

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
            if (Input.GetButton("Fire1") && canShoot) {
                Shoot();
            }
        } else {
            if (Input.GetButton("Fire1") && Time.time > timeToFire && canShoot) {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
        if (Input.GetButtonUp("Fire1")) {
            canShoot = true;
        }
	}

    void Shoot() {
		Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, mousePosition, Color.green);
        if (hit.collider != null) {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);

            if (canShoot) {
                if (hit.transform.GetComponent<Block>().GetSize() == 1) {
                    hit.transform.GetComponent<Block>().hitPoint = hit.point;
                    hit.transform.GetComponent<Block>().Divide();
                    canShoot = false;
                } else if (hit.transform.GetComponent<Block>().GetSize() == 4) {
                    hit.transform.GetComponent<Block>().hitPoint = hit.point;
                    hit.transform.GetComponent<Block>().Divide();
                    canShoot = false;
                } else if (hit.transform.GetComponent<Block>().GetSize() == 16) {
                    hit.transform.GetComponent<Block>().hitPoint = hit.point;
                    hit.transform.GetComponent<Block>().Divide();
                    canShoot = false;
                }
                Debug.Log(hit.point);
                //Debug.Log(hit.transform.position);

                //canShoot = false;
                Debug.Log(hit.transform +  "new collider");
            }
        }
    }
}
