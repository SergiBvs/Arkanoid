using UnityEngine;

public class CorruptionRandomColor : MonoBehaviour {

    public Vector3 Colors;
	public bool needsColor = true;

	// Use this for initialization
	void Start () {

		Colors.x = Random.Range(0, 1f);
		Colors.y = Random.Range(0, 1f);
		Colors.z = Random.Range(0, 1f);

		if (needsColor)
		{
			Color color = new Color(Colors.x, Colors.y, Colors.z);
			this.GetComponent<SpriteRenderer>().color = color;
		}
	}
}
