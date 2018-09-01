using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingText : MonoBehaviour {

    public float speed = 1f;
    public float fadeSpeed = 1f;

    public void Float(string text, Vector3 position)
    {
        GetComponent<Text>().text = text;
        transform.position = position;
        StartCoroutine(floatUp());
    }

    IEnumerator floatUp()
    {
        while(GetComponent<Text>().color.a > 0)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            Color c = GetComponent<Text>().color;
            c.a -= fadeSpeed * Time.deltaTime;
            GetComponent<Text>().color = c;
            yield return null;
        }

        Destroy(gameObject);
    }
}
