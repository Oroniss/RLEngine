﻿using NUnit.Framework;
using RLNET;
using System.Collections.Generic;
using RLEngine.UserInterface;

namespace RLEngine
{
	[TestFixture]
	public class UserInputTests
	{
		[Test]
		public void TestAddKeyboardInput()
		{
			UserInputHandler.ClearAllInput();
			UserInputHandler.AddKeyboardInput(RLKey.C);
			Assert.AreEqual("C", UserInputHandler.GetNextKey());
			UserInputHandler.AddKeyboardInput(RLKey.Number9);
			Assert.AreEqual("9", UserInputHandler.GetNextKey());
			UserInputHandler.ExtraKeys = true;
			UserInputHandler.AddKeyboardInput(RLKey.Quote);
			Assert.AreEqual("DOWN_RIGHT", UserInputHandler.GetNextKey());
			UserInputHandler.ExtraKeys = false;
			UserInputHandler.AddKeyboardInput(RLKey.Quote);
			UserInputHandler.AddKeyboardInput(RLKey.Space);
			Assert.AreEqual("SPACE", UserInputHandler.GetNextKey());
			UserInputHandler.ClearAllInput();

			UserInputHandler.AddKeyboardInput("C");
			Assert.AreEqual("C", UserInputHandler.GetNextKey());
			UserInputHandler.AddKeyboardInput("Hello");
			Assert.AreEqual("Hello", UserInputHandler.GetNextKey());
			UserInputHandler.ClearAllInput();

			var input = new List<string> { "A", "C", "DOWN", "1" };
			UserInputHandler.AddKeyboardInput(input);

			Assert.AreEqual("A", UserInputHandler.GetNextKey());
			Assert.AreEqual("C", UserInputHandler.GetNextKey());
			Assert.AreEqual("DOWN", UserInputHandler.GetNextKey());
			Assert.AreEqual("1", UserInputHandler.GetNextKey());
			UserInputHandler.ClearAllInput();
		}

		[Test]
		public void TestGetDirection()
		{
			UserInputHandler.ClearAllInput();

			var input = new List<string> {"DOWN"};

			UserInputHandler.AddKeyboardInput(input);
			var direction = UserInputHandler.GetDirection();
			Assert.AreEqual(0, direction.X);
			Assert.AreEqual(1, direction.Y);
			input = new List<string> { "UP_LEFT" };

			UserInputHandler.AddKeyboardInput(input);
			direction = UserInputHandler.GetDirection();
			Assert.AreEqual(-1, direction.X);
			Assert.AreEqual(-1, direction.Y);

			input = new List<string> {"C", "RIGHT" };

			UserInputHandler.AddKeyboardInput(input);
			direction = UserInputHandler.GetDirection();
			Assert.AreEqual(1, direction.X);
			Assert.AreEqual(0, direction.Y);

			input = new List<string> {"SPACE" };

			UserInputHandler.AddKeyboardInput(input);
			direction = UserInputHandler.GetDirection();
			Assert.AreEqual(0, direction.X);
			Assert.AreEqual(0, direction.Y);

			input = new List<string> {"SPACE", "RIGHT" };

			UserInputHandler.AddKeyboardInput(input);
			direction = UserInputHandler.GetDirection("", false);
			Assert.AreEqual(1, direction.X);
			Assert.AreEqual(0, direction.Y);

			input = new List<string> {"ESCAPE" };

			UserInputHandler.AddKeyboardInput(input);
			Assert.IsNull(UserInputHandler.GetDirection());
		}

		[Test]
		public void TestGetText()
		{
			var input = new List<string> { "F", "R", "E", "D", "ENTER"};

			UserInputHandler.AddKeyboardInput(input);
			Assert.AreEqual("Fred", UserInputHandler.GetText(""));
			UserInputHandler.ClearAllInput();
			
			input = new List<string> { "A", "L", "E", "A", "S", "H", "A", "SPACE", 
				"S", "I", "L", "V", "E", "R", "S", "T", "A", "R", "ENTER" };

			UserInputHandler.AddKeyboardInput(input);
			Assert.AreEqual("Aleasha Silverstar", UserInputHandler.GetText(""));
			UserInputHandler.ClearAllInput();

			input = new List<string> { "A", "L", "E", "A", "S", "A", "BACKSPACE", "H", "A", "SPACE", 
				"S", "I", "L", "V", "E", "R", "S", "T", "A", "R", "ENTER" };

			UserInputHandler.AddKeyboardInput(input);
			Assert.AreEqual("Aleasha Silverstar", UserInputHandler.GetText(""));
			UserInputHandler.ClearAllInput();

			input = new List<string> { "A", "L", "E", "A", "S", "ESCAPE" };

			UserInputHandler.AddKeyboardInput(input);
			Assert.IsNull(UserInputHandler.GetText(""));
			UserInputHandler.ClearAllInput();

			input = new List<string> { "A", "L", "E", "A", "S", "H", "A", "SPACE", 
				"A", "L", "E", "A", "S", "H", "A", "SPACE", 
				"A", "L", "E", "A", "S", "H", "A", "SPACE", 
				"A", "L", "E", "A", "S", "H", "A", "SPACE", 
				"A", "L", "E", "A", "S", "H", "A", "SPACE", 
				"S", "I", "L", "BACKSPACE", "BACKSPACE", "T", "Z", "ENTER" };

			UserInputHandler.AddKeyboardInput(input);
			Assert.AreEqual("Aleasha Aleasha Aleasha Aleasha Aleashtz", UserInputHandler.GetText(""));
			UserInputHandler.ClearAllInput();

			input = new List<string> { "A", "L", "E", "A", "S", "H", "A", "SPACE", "COMMA",
				"S", "I", "L", "V", "E", "R", "S", "T", "A", "R", "ENTER" };

			UserInputHandler.AddKeyboardInput(input);
			Assert.AreEqual("Aleasha Silverstar", UserInputHandler.GetText(""));
			UserInputHandler.ClearAllInput();
		}

		[Test]
		public void TestSelectFromMenu()
		{
			
		}
	}
}
