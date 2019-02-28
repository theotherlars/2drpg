using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foreground;
    private Image background;
    [SerializeField]
    [Tooltip("The update speed in seconds")]
    private float updateSpeed = 0.2f;
    
    //private FloatReference enemyHP;

    private void Awake()
    {
        GetComponentInParent<EnemyController>().OnHealthPercentChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float percent)
    {
        StartCoroutine(ChangeHealthBar(percent));
    }

    private IEnumerator ChangeHealthBar(float percent)
    {
        float preChangePercent = foreground.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            foreground.fillAmount = Mathf.Lerp(preChangePercent, percent, elapsed / updateSpeed);
            yield return null;
        }
        foreground.fillAmount = percent;
        if (percent == 0) { this.gameObject.SetActive(false);}
    }

    /*
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }*/
}
