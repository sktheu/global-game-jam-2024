using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCTrigger : MonoBehaviour
{
    #region Vari�veis Globais
    // Unity Inspector:
    [Header("Configura��es:")]
    [SerializeField] private NPC currentNpc;

    private enum NPC
    {
        Cat,
        Fish,
        Lizard
    }
    #endregion

    #region Fun��es Pr�prias
    public void ActivateMiniGame()
    {
        switch (currentNpc)
        {
            case NPC.Cat:
                // Ir para a fase do minigame da cozinha
                SceneManager.LoadScene("CatMiniGame");
                break;

            case NPC.Fish:
                // Come�ar o minigame do peixe
                break;

            case NPC.Lizard:
                // Ir para a fase do minigame de m�sica
                SceneManager.LoadScene("FishMiniGame");
                break;
        }
    }
    #endregion
}
