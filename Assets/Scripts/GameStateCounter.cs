using AudioContent;
using EnemyContent;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameStateCounter : MonoBehaviour
{
    [Header("Post Processing Volume")] 
    [SerializeField] private Volume _postProcessVolume;
    [SerializeField] private GameObject _runInfo;
    [SerializeField] private Color _adjustColor;

    private Client _client;
    private CoffeeMachine _coffeeMachine;
    private Bloom _bloom;
    private ColorAdjustments _colorAdjust;
    private ChromaticAberration _chromaticAberration;

    public bool StateRun { get; private set; } = false;
    
    private void OnDisable()
    {
        _client.Transformed -= RunStateActivate;
        _coffeeMachine.CupCoffeeSetted -= SuspenseStateActivate;
        _client.Transforming -= TransformingStateActivate;
    }

    public void Init(Client client, CoffeeMachine coffeeMachine)
    {
        if (_postProcessVolume != null && _postProcessVolume.profile != null)
        {
            _postProcessVolume.profile.TryGet<Bloom>(out _bloom);
            _postProcessVolume.profile.TryGet<ColorAdjustments>(out _colorAdjust);
            _postProcessVolume.profile.TryGet<ChromaticAberration>(out _chromaticAberration);
        }

        _client = client;
        _coffeeMachine = coffeeMachine;
        _coffeeMachine.CupCoffeeSetted += SuspenseStateActivate;
        _client.Transformed += RunStateActivate;
        _client.Transforming += TransformingStateActivate;
    }

    private void SuspenseStateActivate()
    {
        AudioController.Instance.MysticMusic();
    }

    private void TransformingStateActivate()
    {
        if (_colorAdjust != null)
            _colorAdjust.colorFilter.value = _adjustColor;

        if (_bloom != null)
            _bloom.tint.overrideState = true;

        if (_chromaticAberration != null)
            _chromaticAberration.intensity.value = 1f;
        
        StateRun = true;
    }

    private void RunStateActivate()
    {
        _runInfo.SetActive(true);
        AudioController.Instance.RunMusic();
    }
}