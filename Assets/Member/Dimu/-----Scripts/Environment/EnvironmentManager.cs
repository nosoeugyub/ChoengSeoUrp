using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EnvironmentManager : MonoBehaviour
{
    float cleanliness; //0~100
    [Range(0, 100)]
    [SerializeField] float _cleanliness; //0~100
    [SerializeField] Image cleanGageImage;
    [SerializeField] Material skyBox;

    [SerializeField] Color goodFogColor;//183 239 248
    [SerializeField] Color badFogColor;//58 58 58 

    [SerializeField] float goodFogStartDis;
    [SerializeField] float badFogStartDis;

    [SerializeField] float goodFogEndDis;
    [SerializeField] float badFogEndDis;

    [SerializeField] Color goodSkyColor;//     = new Color(90, 167, 255); //90 167 255
    [SerializeField] Color badSkyColor;//    = new Color(25, 30 ,48);//25 30 48
                                           //
    [SerializeField] Color goodEquatorColor;//        = new Color(158, 241, 255); //158 241 255
    [SerializeField] Color badEquatorColor;//    = new Color(69, 69, 77); //69 69 77
                                          //
    [SerializeField] Color goodGroundColor;//= new Color(90, 167, 255);//90 167 255
    [SerializeField] Color badGroundColor;//= new Color(173, 186, 202); //173 186 202
                                           //
    [SerializeField] Color goodCloudColor;// = new Color(255, 255, 255);//90 167 255
    [SerializeField] Color badCloudColor;//= new Color(155, 155, 155); //173 186 202

    [SerializeField] Color _fogColor; 
    [SerializeField] Color _skyColor;
    [SerializeField] Color _equatorColor;
    [SerializeField] Color _groundColor;
    [SerializeField] Color _cloudColor;

    [SerializeField] Light d1;
    [SerializeField] float goodIntensity_d1;
    [SerializeField] float badIntensity_d1;
    [SerializeField] Light d2;
    [SerializeField] float goodIntensity_d2;
    [SerializeField] float badIntensity_d2;
    [SerializeField] Light d3;
    [SerializeField] float goodIntensity_d3;
    [SerializeField] float badIntensity_d3;

    [SerializeField] VolumeProfile volume;
    //[SerializeField] Image fatiGageImage;
    public float Cleanliness
    {
        get
        {
            return cleanliness;
        }
        set
        {
            cleanliness = value;
            cleanGageImage.fillAmount = cleanliness / 100;
        }
    }
    private void Start()
    {
        //fogColor = RenderSettings.fogColor;
        //Cleanliness = 10;
    }
    private void Update()
    {
        //RenderSettings.fogColor = fogColor;
        //Cleanliness = _cleanliness;
        Cleanliness +=Time.deltaTime*20;

        _fogColor = ((goodFogColor - badFogColor) / 100 * Cleanliness) + badFogColor;
        RenderSettings.fogColor = _fogColor;

        RenderSettings.fogStartDistance = ((goodFogStartDis - badFogStartDis) / 100 * Cleanliness) + badFogStartDis;
        RenderSettings.fogEndDistance = ((goodFogEndDis - badFogEndDis) / 100 * Cleanliness) + badFogEndDis;

        _skyColor = ((goodSkyColor - badSkyColor) / 100 * Cleanliness) + badSkyColor;
        _equatorColor = ((goodEquatorColor - badEquatorColor) / 100 * Cleanliness) + badEquatorColor;
        _groundColor = ((goodGroundColor - badGroundColor) / 100 * Cleanliness) + badGroundColor;
        _cloudColor = ((goodCloudColor - badCloudColor) / 100 * Cleanliness) + badCloudColor;

        d1.intensity = ((goodIntensity_d1 - badIntensity_d1) / 100 * Cleanliness) + badIntensity_d1;
        d2.intensity = ((goodIntensity_d2 - badIntensity_d2) / 100 * Cleanliness) + badIntensity_d2;
        d3.intensity = ((goodIntensity_d3 - badIntensity_d3) / 100 * Cleanliness) + badIntensity_d3;

        skyBox.SetColor("_SkyColor", _skyColor);
        skyBox.SetColor("_EquatorColor", _equatorColor);
        skyBox.SetColor("_GroundColor", _groundColor);
        skyBox.SetColor("_CloudsLightColor", _cloudColor);

        UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments;
    }
    public void SetFog()
    {
        RenderSettings.fogColor = badFogColor;
    }
}
