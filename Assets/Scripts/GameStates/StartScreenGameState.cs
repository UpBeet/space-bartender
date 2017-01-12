using UnityEngine.EventSystems;
using YesAndEngine.GameStateManagement;

namespace SpaceBartender.GameStates {

	// Controller for the start screen game state.
	public class StartScreenGameState : IGameState {

		// Called when the player starts the game.
		public void OnClickStartGame (PointerEventData e) {
			Manager.PushStateAsync ("Gameplay Screen");
		}
	}
}
