/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 10/2018
 * Modificação:
 * ********************************************************************/


using UnityEngine;


/// <summary>
/// /classe de estado mãe para os estados que aceitam input de combate.
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class Combat : PlayerState
{
    /// <summary>
    /// Dado um input, retorna o estado de combate correspondente.
    /// Essa função precisa ser chamada no Update de cada estado que precisa dela.
    /// Pode-se retorna o resultado dela diretamente do update do estado para o gerenciador deestados.
    /// </summary>
    public  override NewState InputHandle(PlayerInput playerInput)
    {
        // Saber combat.
        if (Input.GetButtonDown(playerInput.trigger_R))
        {
            newState.SetNewState(stateManager.SaberCombat(), playerInput.trigger_R, false);
        }
        else if(Input.GetButtonDown(playerInput.trigger_L))
        {
            newState.SetNewState(stateManager.SaberCombat(), playerInput.trigger_L, false);
        }

        // Force combat.
        else if (Input.GetButtonDown(playerInput.button_Y))
        {
            newState.SetNewState(stateManager.ForceCombat(), playerInput.button_Y, false);
        }
        else if (Input.GetButtonDown(playerInput.vertical_R))
        {
            newState.SetNewState(stateManager.ForceCombat(), playerInput.vertical_R, false);
        }
        else if (Input.GetButtonDown(playerInput.button_X))
        {
            newState.SetNewState(stateManager.ForceCombat(), playerInput.button_X, false);
        }
        else if (Input.GetButtonDown(playerInput.down))
        {
            newState.SetNewState(stateManager.ForceCombat(), playerInput.down, false);
        }
        else if (Input.GetButtonDown(playerInput.right))
        {
            newState.SetNewState(stateManager.ForceCombat(), playerInput.right, false);
        }
        else if (Input.GetButtonDown(playerInput.button_B))
        {
            newState.SetNewState(stateManager.ForceCombat(), playerInput.button_B, false);
        }
        else if (Input.GetButtonDown(playerInput.left))
        {
            newState.SetNewState(stateManager.ForceCombat(), playerInput.left, false);
        }
        else if (Input.GetButtonDown(playerInput.button_A))
        {
            newState.SetNewState(stateManager.ForceCombat(), playerInput.button_A, false);
        }

        // Others.
        else if (Input.GetButtonDown(playerInput.bumper_R))
        {
            newState.SetNewState(stateManager.Row(), playerInput.bumper_R, false);
        }
        else if (Input.GetButtonDown(playerInput.bumper_L))
        {
            //newState.SetNewState(stateManager.Defending(), playerInput.bumper_L, true);
        }

        return newState;
    }


    /// <summary>
    /// Aplica dano ao jogador a chamando a função de dano e indo para o estrado correto.
    /// </summary>
    public override bool TakeDamage(float damage)
    {
        if (player.HpDamage(damage))
        {
            newState.SetNewState(stateManager.Dying(), null, true);
        }
        else
        {
            //newState.SetNewState(stateManager.Damage(), null, true);
        }

        return false;
    }
}
