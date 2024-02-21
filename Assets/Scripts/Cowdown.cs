using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float timeInSeconds = 0; // Tempo inicial em segundos (0 para iniciar no início do dia)
    public float timeCycleMinutes = 30; // Duração do ciclo do dia em minutos
    private TextMeshProUGUI timeText;

    private int[] time = new int[] { 0, 0, 0 };


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
        }
    }

    // Atualiza o texto para exibir o tempo no formato "7:00"
    void UpdateTimeDisplay()
    {
        
        timeText.text = GetHour();
    }
}
