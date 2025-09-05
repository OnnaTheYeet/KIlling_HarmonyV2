using UnityEngine;
using UnityEngine.UI;

public class GammaControlDirectionalLight : MonoBehaviour
{
    public Scrollbar gammaScrollbar;  
    public Light directionalLight;     

    void Start()
    {
        gammaScrollbar.value = 0.5f;

        gammaScrollbar.onValueChanged.AddListener(OnGammaChanged);
    }

    void OnGammaChanged(float value)
    {
        float gamma = Mathf.Lerp(0.1f, 3.0f, value); 

        if (directionalLight != null)
        {
            directionalLight.intensity = gamma; 
        }

        if (directionalLight != null)
        {
            directionalLight.color = new Color(gamma, gamma, gamma);
        }
    }

    void OnDestroy()
    {
        gammaScrollbar.onValueChanged.RemoveListener(OnGammaChanged);
    }
}
