using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : NetworkBehaviour
{
    public float timeCycleMinutes = 30; // Duração do ciclo do dia em minutos
    
    private TextMeshProUGUI timeText;

    public TextMeshProUGUI playerCountText;

    [Range(1, 4)]
    public int estacao = 1;

    public Light2D GlobalLight;

    public int[] time = new int[] { 0, 0, 0 };
    private float daylightIntensityModifier = 1f; // Modificador de intensidade da luz durante o dia
    private float temperatureModifier = 1f; // Modificador de cor da luz com base na temperatura (verão = quente, inverno = frio)

    // Start is called before the first frame update
    void Start()
    {
        timeText = GameObject.Find("TimeView").GetComponent<TextMeshProUGUI>();
        UpdateTimeDisplay();
        StartCoroutine(CountdownRoutine());
    }

    public void resetHour()
    {
        time[0] = time[1] = time[2] = 0;
    }

    public string GetHour()
    {
        return string.Format("{0:00}:{1:00}", time[0], time[1]);
    }

    // Rotina para incrementar o tempo a cada segundo
    IEnumerator CountdownRoutine()
    {
        float secondsPerDay = timeCycleMinutes * 60; // Segundos em um dia
        float secondsPerTick = secondsPerDay / 24000; // Segundos por "tick" (um tick é a menor unidade de tempo no jogo Minecraft)
        while (true)
        {
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
            UpdateTimeDisplay();
            UpdateGlobalLight();
        }
    }

    // Atualiza o texto para exibir o tempo no formato "7:00"
    void UpdateTimeDisplay()
    {
        timeText.text = GetHour();
    }

    void updateCountPlayers()
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
        // Ajusta a intensidade da luz com base na hora do dia
        if (time[0] < 12) // Manhã
        {
            daylightIntensityModifier = (time[0] * 0.0833f) + (time[1] * 0.001f); // Aumenta a intensidade da luz até meio-dia
        }
        else // Tarde e Noite
        {
            daylightIntensityModifier = 1f - ((time[0] - 12) * 0.0833f) - (time[1] * 0.001f); // Diminui a intensidade da luz após o meio-dia
        }

        // Ajusta a cor da luz com base na estação do ano
        // Aqui você pode implementar lógica para ajustar a temperatura da luz com base na estação do ano
        // Por exemplo, cores mais quentes para o verão e cores mais frias para o inverno
        // Vou definir um exemplo simples aqui:

        // Estamos considerando que o ano é dividido em 4 estações (3 meses cada)
        switch (estacao)
        {
            case 1: // Primavera
                //GlobalLight.color = Color.white * temperatureModifier; // Multiplica a cor atual pela temperatura modificada
                GlobalLight.color = Color.Lerp(Color.white, Color.yellow, temperatureModifier);
                break;
            case 2: // Verão
                temperatureModifier = 1.2f; // Aumenta a temperatura da cor (quente)
                GlobalLight.color = Color.Lerp(Color.white, Color.red, temperatureModifier);
                break;
            case 3: // Outono
                temperatureModifier = 1f; // Cor padrão (neutra)
                GlobalLight.color = Color.Lerp(Color.white, new Color(1f, 0.5f, 0f), temperatureModifier); // Laranja
                break;
            case 4: // Inverno
                temperatureModifier = 0.8f; // Reduz a temperatura da cor (fria)
                GlobalLight.color = Color.Lerp(Color.white, Color.blue, temperatureModifier);
                break;
        }

        // Aplica os modificadores à luz global
        GlobalLight.intensity = daylightIntensityModifier;
        updateCountPlayers();
    }
}
