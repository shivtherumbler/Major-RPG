using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Graphics : MonoBehaviour
{
    public int[] ResolutionSetWidth;
    public int[] ResolutionSetHeight;
    public int CurrentResolutionValue;
    public int CurrentQualityValue;
    public bool[] Effects;
    public Toggle[] ToggleEffects;
    public Dropdown ResolutionDropDown;
    public Dropdown QualityDropdown;
    bool fullScreen;
    public PostProcessProfile PPV;
    private void Start()
    {
        Effects = new bool[7];
        
    }
    public void ResolutionChanged(int Value)
    {
        Screen.SetResolution(ResolutionSetWidth[Value], ResolutionSetHeight[Value], fullScreen);
        CurrentResolutionValue = Value;

    }
    public void QualityChanged(int Value)
    {
        QualitySettings.SetQualityLevel(Value);
        CurrentQualityValue = Value;
    }
    public void FullScreenUpdate(bool Toggle)
    {
        Screen.fullScreen = Toggle;
        fullScreen = Toggle;
        Screen.SetResolution(ResolutionSetWidth[CurrentResolutionValue], ResolutionSetHeight[CurrentResolutionValue], fullScreen);
        Effects[0] = Toggle;
    }
    public void BloomChange(bool Toggle)
    {
        PPV.GetSetting<Bloom>().active = Toggle;
        Effects[1] = Toggle;
    }
    public void VignetteChange(bool Toggle)
    {
        PPV.GetSetting<Vignette>().active = Toggle;
        Effects[2] = Toggle;
    }
    public void ChromaticAbberationChange(bool Toggle)
    {
        PPV.GetSetting<ChromaticAberration>().active = Toggle;
        Effects[3] = Toggle;
    }
    public void MotionBlur(bool Toggle)
    {
        PPV.GetSetting<MotionBlur>().active = Toggle;
        Effects[4] = Toggle;
    }
    public void AmbientOcculsionChange(bool Toggle)
    {
        PPV.GetSetting<AmbientOcclusion>().active = Toggle;
        Effects[5] = Toggle;
    }

    public void ColorGrading(bool Toggle)
    {
        PPV.GetSetting<ColorGrading>().active = Toggle;
        Effects[6] = Toggle;
    }
}