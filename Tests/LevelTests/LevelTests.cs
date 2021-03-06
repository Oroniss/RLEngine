﻿// Tidied for version 0.3.

using NUnit.Framework;
using RLEngine.Levels;
using RLEngine.Entities;
using System.Collections.Generic;

namespace RLEngine.Tests
{
	[TestFixture]
	public class LevelTests
	{
		static string defaultDebugMessage = "No Debug Messages";
		static string databasePath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "UnitTestFiles");

		[SetUp]
		public void Setup()
		{
			Levels.LevelDatabase.LevelDatabase.SetTestFilePath(TestContext.CurrentContext.TestDirectory);
			ErrorLogger.SetToTest();
			StaticDatabase.StaticDatabaseConnection.SetToTest(databasePath);
			StaticDatabase.StaticDatabaseConnection.OpenDBConnection();

		}

		[TearDown]
		public void TearDown()
		{
			StaticDatabase.StaticDatabaseConnection.CloseDBConnection();
		}

		[Test]
		public void TestBasicFunctions()
		{
			var level1 = new Level(LevelId.TestLevel1);
			Assert.AreEqual("Small Test Level", level1.LevelName);
			Assert.AreEqual(8, level1.MapWidth);
			Assert.AreEqual(6, level1.MapHeight);

			var level2 = new Level(LevelId.TestLevel2);
			Assert.AreEqual("Large Test Level", level2.LevelName);
			Assert.AreEqual(60, level2.MapWidth);
			Assert.AreEqual(12, level2.MapHeight);
		}

		[Test]
		public void TestUtilityFunctions()
		{
			var level1 = new Level(LevelId.TestLevel1);
			Assert.IsTrue(level1.IsValidMapCoord(4, 4));
			Assert.IsTrue(level1.IsValidMapCoord(0, 0));
			Assert.IsTrue(level1.IsValidMapCoord(7, 5));
			Assert.IsTrue(level1.IsValidMapCoord(0, 5));
			Assert.IsFalse(level1.IsValidMapCoord(-1, 0));
			Assert.IsFalse(level1.IsValidMapCoord(0, 6));
			Assert.IsFalse(level1.IsValidMapCoord(8, 0));

			// TODO: Keep these up to date as more entity types get added in.
			Assert.IsTrue(level1.HasTrait(Trait.BlockMove, 0, 5));
			Assert.IsTrue(level1.HasTrait(Trait.TestTrait2, 7, 5));
			Assert.IsFalse(level1.HasTrait(Trait.TestTrait1, 6, 1));

			var level2 = new Level(LevelId.TestLevel2);
			Assert.IsTrue(level2.IsValidMapCoord(0, 0));
			Assert.IsTrue(level2.IsValidMapCoord(59, 11));
			Assert.IsFalse(level2.IsValidMapCoord(59, -1));
			Assert.IsFalse(level2.IsValidMapCoord(60, 1));
			Assert.IsFalse(level2.IsValidMapCoord(0, 12));
		}

		[Test]
		public void TestRevealedFunctions()
		{
			var level1 = new Level(LevelId.TestLevel1);
			Assert.IsFalse(level1.IsRevealed(0, 0));
			Assert.IsFalse(level1.IsRevealed(7, 5));

			level1.RevealTile(4, 4);

			Assert.IsTrue(level1.IsRevealed(4, 4));

			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			Assert.IsFalse(level1.IsRevealed(12, 15));
			Assert.AreEqual("Checked revealed on invalid tile: 12, 15", ErrorLogger.GetNextTestMessage());
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			level1.RevealTile(12, 15);
			Assert.AreEqual("Tried to reveal an invalid tile: 12, 15", ErrorLogger.GetNextTestMessage());
			Assert.AreEqual(defaultDebugMessage, ErrorLogger.GetNextTestMessage());

			ErrorLogger.ClearTestMessages();
		}

		[Test]
		public void TestGetColors()
		{
			// TODO: Keep up to date as different entity types go in.
			var level1 = new Level(LevelId.TestLevel1);

			Assert.AreEqual("GraySeven", level1.GetBGColor(0, 0));
			Assert.AreEqual("GraySeven", level1.GetBGColor(3, 3));
			Assert.AreEqual("LightBlue", level1.GetBGColor(3, 5));
			Assert.AreEqual("LightBlue", level1.GetBGColor(7, 3));

			Assert.AreEqual("Black", level1.GetBGColor(9, 4));
			Assert.AreEqual("Attempted to get bgcolor on invalid tile: 9, 4", ErrorLogger.GetNextTestMessage());
			ErrorLogger.ClearTestMessages();

			Assert.AreEqual("GrayFour", level1.GetFogColor(0, 0));
			Assert.AreEqual("GrayFour", level1.GetFogColor(3, 3));
			Assert.AreEqual("Blue", level1.GetFogColor(3, 5));
			Assert.AreEqual("Blue", level1.GetFogColor(7, 3));

			Assert.AreEqual("Black", level1.GetFogColor(4, 6));
			Assert.AreEqual("Attempted to get fogcolor on invalid tile: 4, 6", ErrorLogger.GetNextTestMessage());
			ErrorLogger.ClearTestMessages();

			// TODO: Add in tests for FG colors as well once entities in properly.
		}

		[Test]
		public void TestFurnishingFunctions()
		{
			var testLevel1 = new Level(LevelId.TestLevel1);

			Assert.IsTrue(testLevel1.HasFurnishing(2, 2));
			Assert.IsTrue(testLevel1.HasFurnishing(2, 3));
			Assert.IsFalse(testLevel1.HasFurnishing(3, 2));
			Assert.IsFalse(testLevel1.HasFurnishing(4, 3));

			Assert.AreEqual("TestFurnishing1", testLevel1.GetFurnishing(2, 2).EntityName);
			Assert.AreEqual("TestFurnishing2", testLevel1.GetFurnishing(6, 4).EntityName);

			var furnishing1 = EntityFactory.CreateFurnishing("TestFurnishing1", 3, 2, new Dictionary<string, string>());

			testLevel1.AddFurnishing(furnishing1);

			Assert.IsTrue(testLevel1.HasFurnishing(3, 2));
			Assert.AreEqual(furnishing1, testLevel1.GetFurnishing(3, 2));

			var furnishing2 = testLevel1.GetFurnishing(2, 2);

			testLevel1.RemoveFurnishing(furnishing2, 2, 2);

			Assert.IsFalse(testLevel1.HasFurnishing(2, 2));
		}

		[Test]
		public void TestActorFunctions()
		{
			// TODO: Add in once actors in.
		}

		[Test]
		public void TestPathibilityFunctions()
		{
			// TODO: Add in once other entity types are in.
		}

		[Test]
		public void TestDisposeLevel()
		{
			// TODO: Add this in
		}

		[Test]
		public void TestSaveAndLoadLevel()
		{
			// TODO: Add this in.
		}
	}
}
