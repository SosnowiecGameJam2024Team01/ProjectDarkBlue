using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chant : MonoBehaviour
{
    [SerializeField] AudioSource dio;
	[SerializeField] AudioSource afr;

	private void Start()
	{
		StartCoroutine(ChantOne());
		StartCoroutine(ChantTwo());
	}

	IEnumerator ChantOne()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(20,40));
			dio.Play();
		}
	}

	IEnumerator ChantTwo()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(20, 40));
			afr.Play();
		}
	}
}
