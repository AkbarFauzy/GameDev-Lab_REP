using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterDetailsState : GameState
{
    public UICharacterDetailsState(GameManager gameManager, PauseMenu pauseUI):base(gameManager, pauseUI)
    {
    }

    public override IEnumerator Start()
    {
        PauseUI.OpenCharacterDetails(PauseUI.CharacterIndex);
        yield break;
    }

    public override IEnumerator UIRight()
    {
        if (cdCounter == 0)
        {
            cdCounter = 1;
            PauseUI.CharacterIndex += 1;
            if (PauseUI.CharacterIndex >= GameManager.activeCharacter.Length)
            {
                PauseUI.CharacterIndex = 0;
            }
            PauseUI.ChangeCharacterDetails(PauseUI.CharacterIndex);
        }
        yield return new WaitForSeconds(PauseMenu.buttonCD);
        cdCounter = 0;
        yield break;
    }

    public override IEnumerator UILeft()
    {
        if (cdCounter == 0 )
        {
            cdCounter = 1;
            PauseUI.CharacterIndex -= 1;
            if (PauseUI.CharacterIndex < 0)
            {
                PauseUI.CharacterIndex = GameManager.activeCharacter.Length-1;
            }
            PauseUI.ChangeCharacterDetails(PauseUI.CharacterIndex);
        }
        yield return new WaitForSeconds(PauseMenu.buttonCD);
        cdCounter = 0;
        yield break;
    }

    public override IEnumerator UICancel()
    {
        GameManager.SetToPreviousState();
        yield break;
    }

}
