using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float timeInSeconds = 0; // Tempo inicial em segundos (0 para iniciar no início do dia)
    public float timeCycleMinutes = 30; // Duração do ciclo do dia em minutos
    private TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GameObject.Find("TimeView").GetComponent<TextMeshProUGUI>();
        UpdateTimeDisplay();
        StartCoroutine(CountdownRoutine());
    }

    // Rotina para incrementar o tempo a cada segundo
    IEnumerator CountdownRoutine()
    {
        float secondsPerDay = timeCycleMinutes * 60; // Segundos em um dia
        float secondsPerTick = secondsPerDay / 24000; // Segundos por "tick" (um tick é a menor unidade de tempo no jogo Minecraft)
        while (true)
        {
            yield return new WaitForSeconds(secondsPerTick);
            timeInSeconds += 1;
            if (timeInSeconds >= secondsPerDay)
            {
                timeInSeconds = 0;
            }
            UpdateTimeDisplay();
        }
    }

    // Atualiza o texto para exibir o tempo no formato "7:00"
    void UpdateTimeDisplay()
    {
        // Converter o tempo total em segundos para hora e minuto
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);

        // Exibir o tempo formatado
        timeText.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }
}
