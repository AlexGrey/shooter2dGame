using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	/// <summary>
	///список всех fore- и background которые будут обдладать эффектом parallax
	/// </summary>
    public Transform[] backgrounds;

	/// <summary>
	///	сглаживание с которым будут двигаться backgrounds
	/// </summary>
	public float smoothing = 1f;

	/// <summary>
	/// пропорции с которыми будут двигаться backgrounds
	/// </summary>
    private float[] parallaxScales;

	/// <summary>
	/// ссылка на camera transform
	/// </summary>
    private Transform cam;

	/// <summary>
	///	позиция камеры в предыдущем кадре
	/// </summary>
    private Vector3 previousCamPos;

	void Awake() {
		cam = Camera.main.transform;
    }

	// Use this for initialization
	void Start () {
		previousCamPos = cam.position;
		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z*-1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backgrounds.Length; i++) {
            // Эффект parallax в противоположность движения камеры умноженный на масштаб
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // установка целевой позиции по x в текущей позиции плюс parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // создание целевой позиции по х (остальные координаты не меняются)
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade между текущей позиции и позиции камеры используя Lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

        // устанавливаем previousCamPos равным позиции камеры
        previousCamPos = cam.position;
	}
}
