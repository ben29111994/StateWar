using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Cube : MonoBehaviour
{
    [Header("Status")]
    public bool isRed;
    public bool isWaveAnimation;
    public CubeType cubeType;

    [Header("Color")]
    public Color c_Red;
    public Color c_Green;
    public Color c_Black;
    public Color C_White;
    public AnimationCurve animCurve;
    public AnimationCurve animCurve2;
    public Material m_Outline;
    public Material m_Cube;

    [Header("References")]
    public GameObject redCubeTrigger;
    public Renderer rend;
    public Transform model;

    public enum CubeType
    {
        Black,
        Green,
    }

    private void Start()
    {

    }

    public void ActiveCube(Vector3 _pos, CubeType _cubeType)
    {
        isWaveAnimation = false;
        isRed = false;
        model.transform.localPosition = Vector3.zero;
        transform.position = _pos;
        gameObject.SetActive(true);
        StopAllCoroutines();
        SetCubeType(_cubeType);
        redCubeTrigger.SetActive(false);
    }

    public void FuncRed(bool _isOn)
    {
        if (cubeType == CubeType.Green) return;

        isRed = _isOn;
        if (isRed)
        {
           // model.transform.localPosition = Vector3.up * 2.0f;
            WaveAnimation();
            SetRed();
        }
        else
        {
         //   model.transform.localPosition = Vector3.zero;   
            SetBlack(false);
        }
    }

    public void SetCubeType(CubeType _cubeType)
    {
        cubeType = _cubeType;

        switch (cubeType)
        {
            case CubeType.Black:
                SetBlack(true);
                break;
            case CubeType.Green:
                SetGreen();
                break;
        }
    }


    private void WaveAnimation()
    {
        if (isWaveAnimation) return;
        isWaveAnimation = true;
        float distance = 2.0f;
        float time = 0.4f;
        model.DOMoveY(distance, time).SetEase(animCurve).OnComplete(() => isWaveAnimation = false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedTrigger"))
        {
            if (isRed) return;
            FuncRed(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RedTrigger"))
        {
            FuncRed(false);
        }
    }

    [NaughtyAttributes.Button]
    public void SetGreen()
    {
        ChangeColorMaterial(c_Green);
        cubeType = CubeType.Green;
    }

   // [NaughtyAttributes.Button]
    public void SetBlack(bool _isColor)
    {
        if (_isColor)
        {
            Color _c = TestController.Instance.TypeDeFaultCube == 0 ? c_Black : C_White;
            ChangeColorMaterial(_c);
        }
        cubeType = CubeType.Black;
        redCubeTrigger.SetActive(false);
    }

    [NaughtyAttributes.Button]
    public void SetRed()
    {
        float a = 0.5f;
        float b = 0.0f;
        ChangeColorMaterial(c_Red,a);
        cubeType = CubeType.Black;
        redCubeTrigger.SetActive(true);
    }

    private void ChangeColorMaterial(Color _targetColor, float _time = 0.0f)
    {
        StartCoroutine(C_ChangeColorMaterial(_targetColor, _time));
    }
    
    private IEnumerator C_ChangeColorMaterial(Color _targetColor, float _time)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();

        if (_time == 0.0f)
        {
            rend.GetPropertyBlock(mpb, 1);
            mpb.SetColor("_Color", _targetColor);
            rend.SetPropertyBlock(mpb, 1);
            yield break;
        }

        Color colorA = TestController.Instance.TypeDeFaultCube == 0 ? c_Black : C_White;
        Color colorB = _targetColor;
        float time = 0.0f;
        while(time < _time)
        {
            time += Time.deltaTime;

            float t = time / _time;
            float tFixed = animCurve2.Evaluate(t);
            rend.GetPropertyBlock(mpb, 1);
            Color colorC = Color.Lerp(colorA, colorB, tFixed);
            mpb.SetColor("_Color", colorC);
            rend.SetPropertyBlock(mpb, 1);

            yield return null;
        }
    }


}