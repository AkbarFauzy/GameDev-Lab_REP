using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuCharacterSelect : GameState
{
    public UIMenuCharacterSelect(GameManager gameManager, PauseMenu pauseUI):base(gameManager, pauseUI) { 
    }

    public override IEnumerator Start()
    {
        PauseUI.PartyBoxSelected();
        yield break;
    }

    public override IEnumerator UIRight()
    {
        PauseUI.PartyBoxDeselected();
        PauseUI.CharacterIndex += 1;
        if (PauseUI.CharacterIndex >= GameManager.activeCharacter.Length) {
            PauseUI.CharacterIndex = 0;
        }
        PauseUI.PartyBoxSelected();
        yield break;
    }

    public override IEnumerator UILeft()
    {
        PauseUI.PartyBoxDeselected();
        PauseUI.CharacterIndex -= 1;
        if (PauseUI.CharacterIndex == -1) {
            PauseUI.CharacterIndex = GameManager.activeCharacter.Length - 1;
        }
        PauseUI.PartyBoxSelected();
        yield break;
    }

    public override IEnumerator UISubmit()
    {
        GameManager.SetState(new UICharacterDetailsState(GameManager, PauseUI), true);
        yield break;
    }
}
