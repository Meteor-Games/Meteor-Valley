using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IventoryControll : MonoBehaviour
{
    public GameObject[] slots;

    void Start()
    {
        // Encontre todos os objetos com a tag "inventory_slot"
        slots = GameObject.FindGameObjectsWithTag("inventory_slot");

        // Ordenar os objetos pelo nome
        slots = slots.OrderBy(slot => slot.name).ToArray();

        // Adicionar texto com o número da posição dentro de cada slot
        for (int i = 0; i < slots.Length; i++)
        {
            // Crie um objeto de texto
            GameObject textObject = new GameObject("Text");
            // Adicione-o como filho do slot atual
            textObject.transform.SetParent(slots[i].transform);
            // Adicione o componente Text à GameObject
            Text textComponent = textObject.AddComponent<Text>();
            // Configure o texto para exibir o número da posição
            textComponent.text = (i + 1).ToString();
            // Defina a fonte e o tamanho do texto
            textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            textComponent.fontSize = 12; // Altere o tamanho do texto para 12 (pequeno)
                                         // Posicione o texto na parte inferior direita do slot
            textComponent.alignment = TextAnchor.LowerRight;
            // Defina a cor do texto
            textComponent.color = Color.black;
            // Ajuste a posição do texto dentro do slot
            RectTransform rectTransform = textObject.GetComponent<RectTransform>();
            rectTransform.pivot = new Vector2(1, 0); // Ancoragem no canto inferior direito
            rectTransform.anchorMin = new Vector2(1, 0);
            rectTransform.anchorMax = new Vector2(1, 0);
            rectTransform.anchoredPosition = new Vector2(-5, 5); // Ajuste a posição para dentro do slot
        }
    }


    void Update()
    {
        // Você pode adicionar lógica de atualização aqui, se necessário
    }
}
