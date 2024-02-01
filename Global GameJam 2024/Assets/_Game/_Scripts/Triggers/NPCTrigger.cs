using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCTrigger : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [Header("Configurações:")]
    [SerializeField] private NPC currentNpc;

    private enum NPC
    {
        Cat,
        Fish,
        Lizard
    }
    #endregion

    #region Funções Próprias
    public void ActivateMiniGame()
    {
        switch (currentNpc)
        {
            case NPC.Cat:
                // Ir para a fase do minigame da cozinha
                SceneManager.LoadScene("CatMiniGame");
                break;

            case NPC.Fish:
                // Começar o minigame do peixe
                break;

            case NPC.Lizard:
                // Ir para a fase do minigame de música
                SceneManager.LoadScene("FishMiniGame");
                break;
        }
    }
    #endregion
}
