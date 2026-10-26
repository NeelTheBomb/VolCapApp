using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlaybackFeature : MonoBehaviour
{
    public GameObject videoObject;
    public Slider slider;

    private VideoPlayer vp;
    private bool isDragging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (videoObject != null)
            vp = videoObject.GetComponent<VideoPlayer>();

        if (slider != null)
        {
            slider.minValue = 0f;
            slider.maxValue = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vp == null || slider == null)
            return;

        if (!isDragging && vp.length > 0)
        {
            slider.SetValueWithoutNotify((float)(vp.time / vp.length));
        
        }
    }

    public void BeginDrag()
    {
        isDragging = true;
    }

    public void EndDrag()
    {
        if (vp == null || slider == null || vp.length <= 0)
            return;
        
        double targetTime = slider.value * vp.length;
        vp.time = targetTime;
        
        isDragging = false;
    }

    public void OnSliderValueChanged(float currentvalue)
    {
        if (vp == null || !isDragging || vp.length <= 0)
            return;

        double targetTime = currentvalue * vp.length;
        vp.time = targetTime;
    }
}
