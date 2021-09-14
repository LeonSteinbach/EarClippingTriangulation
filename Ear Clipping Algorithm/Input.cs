using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ear_Clipping_Algorithm {
	public static class Input {
		#region KeyboardInput
		private static KeyboardState currentKeyboardState, previousKeyboardState;

		public static bool isKeyHold(Keys key) { return currentKeyboardState.IsKeyDown(key); }
		public static bool isKeyPressed(Keys key) { return (currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key)); }
		public static bool isKeyReleased(Keys key) { return (!currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyDown(key)); }
		#endregion

		#region MouseInput
		private static MouseState currentMouseState, previousMouseState;

		public static Point mousePosition() { return currentMouseState.Position; }
		public static bool mouseMotion() { return currentMouseState.Position != previousMouseState.Position; }

		public static bool isLeftHold() { return currentMouseState.LeftButton == ButtonState.Pressed; }
		public static bool isLeftPressed() { return (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released); }
		public static bool isLeftReleased() { return (currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed); }

		public static bool isRightHold() { return currentMouseState.RightButton == ButtonState.Pressed; }
		public static bool isRightPressed() { return (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released); }
		public static bool isRightReleased() { return (currentMouseState.RightButton == ButtonState.Released && previousMouseState.RightButton == ButtonState.Pressed); }

		public static bool isMiddletHold() { return currentMouseState.MiddleButton == ButtonState.Pressed; }
		public static bool isMiddlePressed() { return (currentMouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released); }
		public static bool isMiddleReleased() { return (currentMouseState.MiddleButton == ButtonState.Released && previousMouseState.MiddleButton == ButtonState.Pressed); }

		public static int scrollValue() { return currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue; }
		#endregion

		public static void Update() {
			previousKeyboardState = currentKeyboardState;
			currentKeyboardState = Keyboard.GetState();

			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();
		}
	}
}
