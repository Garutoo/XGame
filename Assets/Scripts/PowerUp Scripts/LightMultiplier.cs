using UnityEngine;

public class LightMultiplier : MonoBehaviour
{
    GameObject theLight;
    public UnityEngine.Experimental.Rendering.LWRP.Light2D globalLight;
    void Start()
    {
        theLight = this.gameObject;
        globalLight = theLight.GetComponent<UnityEngine.Experimental.Rendering.LWRP.Light2D>();
    }
}
