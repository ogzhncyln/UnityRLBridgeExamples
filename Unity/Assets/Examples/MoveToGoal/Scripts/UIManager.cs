using RLBridge;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private bool status=false;
    public Button start_button;
    public Text clients_text, agents_test;
    public Bridge bridge;

    void Start()
    {
        start_button.onClick.AddListener(ButtonClicked);
    }

    
    void Update()
    {
        clients_text.text = $"Num Clients: {bridge.num_clients}/{bridge.maxClients}";
        agents_test.text = $"Num Agents: {bridge.agents.Length}";

        if (!status)
        {
            start_button.gameObject.SetActive(bridge.num_clients > 0);
        }

    }

    public void ButtonClicked()
    {
        if (!status)
        {
            bridge.BeginEnv();
            status = true;
            start_button.gameObject.SetActive(false);
        }
    }
}
