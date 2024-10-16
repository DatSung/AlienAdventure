using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
	private RectTransform bar;

	// Start is called before the first frame update
	void Start()
	{
		bar = GetComponent<RectTransform>();
		SetSize(HealthController.totalHealth);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public float Damage(float damage)
	{
		if ((HealthController.totalHealth -= damage) >= 0f)
		{
			HealthController.totalHealth -= damage;
		}
		else
		{
			HealthController.totalHealth = 0f;
		}

		SetSize(HealthController.totalHealth);

		return HealthController.totalHealth;
	}

	public void SetSize(float size)
	{
		bar.localScale = new Vector3(size, 1f);
	}
}
