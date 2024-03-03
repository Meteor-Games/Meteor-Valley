using System;
using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class WorldController: NetworkBehaviour
{
    private TextMeshProUGUI timeText;
    public TextMeshProUGUI playerCountText;

    [SerializeField]
    private NetworkVariable<Vector3Int> _time = new NetworkVariable<Vector3Int>();
    [SerializeField]
    public NetworkVariable<int> _estacao = new NetworkVariable<int>();
    [SerializeField]
    public NetworkVariable<float> _daylightIntensityModifier = new NetworkVariable<float>();
    [SerializeField]
    public int _timeCycleMinutes = 0;


    [SerializeField]
    public Light2D GlobalLight;

    //public override void OnNetworkSpawn()
    //{
    //    base.OnNetworkSpawn();
    //    //_time.OnValueChanged += OnTimeChangedClientRpc;
    //    //_estacao.OnValueChanged += SetEstacaoClientRpc;
    //    //_daylightIntensityModifier.OnValueChanged += SetDaylightIntensityModifierClientRpc;
    //}

    void Start()
    {
        GlobalLight.intensity = 1f;
        timeText = GameObject.Find("TimeView").GetComponent<TextMeshProUGUI>();
        resetHour();
        
        UpdateTimeDisplay();
        if(IsServer || IsHost)
        {
            StartCoroutine(CountdownRoutine());
        }
            
    }

    private void UpdateServer()
    {
        UpdateGlobalLight();
        UpdateCountPlayers();
    }

    private void UpdateClient()
    {
        
    }

    private void updateTemperature()
    {
        var _temperatureModifier = 1f;
        switch (_estacao.Value)
        {
            case 1: // Primavera
                _temperatureModifier = 1f;
                GlobalLight.color = Color.Lerp(Color.white, Color.yellow, _temperatureModifier);
                break;
            case 2: // Verão
                _temperatureModifier = 1.2f;
                GlobalLight.color = Color.Lerp(Color.white, Color.red, _temperatureModifier);
                break;
            case 3: // Outono
                _temperatureModifier = 1.2f;
                GlobalLight.color = Color.Lerp(Color.white, new Color(1f, 0.5f, 0f), _temperatureModifier); // Laranja
                break;
            case 4: // Inverno
                _temperatureModifier = 0.8f;
                GlobalLight.color = Color.Lerp(Color.white, Color.blue, _temperatureModifier);
                break;
        }
    }

    private void Update()
    {
        if(IsServer || IsHost)
        {
            UpdateServer();
        }
        else
        {
            UpdateClient();
        }
        UpdateTimeDisplay();
        updateTemperature();
        GlobalLight.intensity = _daylightIntensityModifier.Value;
    }

    public string GetHour()
    {
        return string.Format("{0:00}:{1:00}", _time.Value[0], _time.Value[1]);
    }

    // Rotina para incrementar o tempo a cada segundo
    IEnumerator CountdownRoutine()
    {
        float secondsPerDay = _timeCycleMinutes * 60; // Segundos em um dia
        float secondsPerTick = secondsPerDay / 24000; // Segundos por "tick" (um tick é a menor unidade de tempo no jogo Minecraft)
        
        while (true)
        {
            var time = new int[] { _time.Value[0], _time.Value[1], _time.Value[2] };
            yield return new WaitForSeconds(secondsPerTick);

            time[2] += 1;
            if (time[2] == 60)
            {
                time[2] = 0;
                time[1]++;
                if (time[1] == 60)
                {
                    time[1] = 0;
                    time[0]++;
                    if (time[0] == 24)
                    {
                        resetHour();
                    }
                }
            }
            var nt = new Vector3Int();
            nt.Set(time[0], time[1], time[2]);
            SetTimeServerRpc(nt);
        }
    }   

    // Atualiza o texto para exibir o tempo no formato "7:00"
    void UpdateTimeDisplay()
    {
        timeText.text = GetHour();
    }

    void UpdateCountPlayers()
    {

        // Atualiza o texto com a quantidade de jogadores online somente se estiver conectado ou tiver criado um servidor
        if ((NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer))
        {
            playerCountText.text = "Players: " + NetworkManager.Singleton.ConnectedClientsList.Count;
        }
        else
        {
            Destroy(playerCountText);
        }

    }

    // Atualiza a luz global com base no tempo do dia e em fatores sazonais
    void UpdateGlobalLight()
    {
        if (_time.Value[0] < 12)
        {
            SetDaylightIntensityModifierServerRpc((_time.Value[0] * 0.0833f) + (_time.Value[1] * 0.001f)); // Aumenta a intensidade da luz até meio-dia
        }
        else
        {
            SetDaylightIntensityModifierServerRpc(1f - ((_time.Value[0] - 12) * 0.0833f) - (_time.Value[1] * 0.001f));
        }
    }

    public void resetHour()
    {
        _time.Value.Set(0, 0, 0);
        //this.SetTimeServerRpc(_time.Value);
    }

    [ServerRpc]
    private void SetTimeServerRpc(Vector3Int time)
    {
        _time.Value = time;
    }

    [ServerRpc]
    private void SetEstacaoServerRpc(int estacao)
    {
        _estacao.Value = estacao;
    }


    [ServerRpc]
    private void SetDaylightIntensityModifierServerRpc(float daylightIntensityModifier)
    {
        _daylightIntensityModifier.Value = daylightIntensityModifier;
    }


    [ClientRpc]
    private void SetEstacaoClientRpc(int previousValue, int estacao)
    {
        _estacao.Value = estacao;
    }

    [ClientRpc]
    private void SetDaylightIntensityModifierClientRpc(float previousValue, float daylightIntensityModifier)
    {
        _daylightIntensityModifier.Value = daylightIntensityModifier;
    }

    [ClientRpc]
    public void OnTimeChangedClientRpc(Vector3Int previousValue, Vector3Int newValue)
    {
        _time.Value = newValue;
    }
}
