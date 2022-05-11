using System.Collections;
using UnityEngine;
using Cinemachine;

//per chiamarlo da logica player: CinemachineShake.Instance.ShakeCamera(5f,.1f) TODO modificare al caso nostro

public class CinemachineShake : MonoBehaviour
{

    public static CinemachineShake Instance { get; private set; }

    [SerializeField] private float amplitude = 10f;
    [SerializeField] private float frequency = .01f;
    public NoiseSettings noiseProfile;

    private CinemachineFreeLook cineFreeLook;
    
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;
    private bool isShacking = false;

    private void Awake()
    {
        Instance = this;
        cineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        EditNoise(intensity, frequency);

        startingIntensity = intensity;
        shakeTimer = time;
        isShacking = true;
    }

    public void ShakeCamera(float intensity)
    {
        EditNoise(intensity, frequency);
        startingIntensity = intensity;
        
        isShacking = true;
    }

    public void ShakeCamera()
    {
        EditNoise(amplitude, frequency);
        startingIntensity = amplitude;

        isShacking = true;
    }

    public void StopShaking()
    {
        shakeTimer = 0;
        isShacking = false;
    }

    private void EditNoise(float amp, float freq)
    {
               
        for(int i=0; i<3; i++)
        {
            cineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amp;
            cineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = freq;
            cineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = noiseProfile;
        }
    }

    private void EditNoise(float amp)
    {

        for (int i = 0; i < 3; i++)
        {
            cineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amp;
            cineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
            cineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = noiseProfile;
        }
    }

    private void EditNoise()
    {
        float amp = amplitude, freq = frequency;

        for (int i = 0; i < 3; i++)
        {
            cineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amp;
            cineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = freq;
            cineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = noiseProfile;
        }
    }
    

    private void Update()
    {
        if (shakeTimer > 0 || isShacking == true)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f || isShacking == false)
            {
                //Time out
                EditNoise(Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal)));
                //CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cineFreeLook.GetComponent<CinemachineBasicMultiChannelPerlin>();

                //cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
            }
        }
    }

}