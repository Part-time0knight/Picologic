using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddController : MonoBehaviour, IAdd
{
    public void ShowAdd(ShowEnd showEnd)
    {
        gameObject.SetActive(true);
        StartCoroutine(EndAdd(showEnd));
    }
    private IEnumerator EndAdd(ShowEnd showEnd)
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        showEnd();
    }
}
