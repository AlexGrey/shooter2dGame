using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    public float fireRate = 0;
    public float damage = 10f;
    public LayerMask whatToHit;
    public Transform bulletTrailPrefab;
    public Transform muzzleFlashPrefab;
    public float effectSpawnRate = 10;

    bool canShoot = true;
    float timeToFire = 0;
    public float timeToSpawnEffect = 0;
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
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        if (Time.time >= timeToSpawnEffect) {
            StartCoroutine("Effect");
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }

        Debug.DrawLine(firePointPosition, mousePosition, Color.green);
        if (hit.collider != null) {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);

            if (canShoot) {
				if (hit.transform.gameObject.tag.Equals ("Monster")) {
					if (hit.transform.FindChild ("MeleeRange").GetComponent<UfOEnemy> ().health == 0) {
						hit.transform.FindChild ("MeleeRange").GetComponent<UfOEnemy> ().DestroySelf ();
						GameObject.Find ("CharacterRobotBoy").GetComponent<Character> ().countOfKilledMosters++;
						canShoot = false;
					} else if (hit.transform.FindChild ("MeleeRange").GetComponent<UfOEnemy> ().health > 0) {
						hit.transform.FindChild ("MeleeRange").GetComponent<UfOEnemy> ().health -= 1;
					}
					canShoot = false;
				} else if (hit.transform.gameObject.tag.Equals ("Turret")) {
					if (hit.transform.FindChild ("Gun").GetComponent<TurretGun> ().health == 0) {
						hit.transform.FindChild ("Gun").GetComponent<TurretGun> ().DestroySelf ();
						canShoot = false;
					} else if (hit.transform.FindChild ("Gun").GetComponent<TurretGun> ().health > 0) {
						hit.transform.FindChild ("Gun").GetComponent<TurretGun> ().health -= 1;
					}
					canShoot = false;
				} else if (hit.transform.GetComponent<Block> ().GetSize () == 1) {
					hit.transform.GetComponent<Block> ().hitPoint = hit.point;
					hit.transform.GetComponent<Block> ().Divide ();
					canShoot = false;
				} else if (hit.transform.GetComponent<Block> ().GetSize () == 4) {
					hit.transform.GetComponent<Block> ().hitPoint = hit.point;
					hit.transform.GetComponent<Block> ().Divide ();
					canShoot = false;
				} else if (hit.transform.GetComponent<Block> ().GetSize () == 16) {
					hit.transform.GetComponent<Block> ().hitPoint = hit.point;
					hit.transform.GetComponent<Block> ().Divide ();
					canShoot = false;
				} 
            }
        }
    }

    IEnumerator Effect () {
        Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);
        Transform clone = (Transform) Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        yield return 0;
        Destroy(clone.gameObject, 0.02f);
    }
}
