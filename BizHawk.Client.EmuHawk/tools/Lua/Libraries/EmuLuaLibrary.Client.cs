﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using LuaInterface;
using BizHawk.Common;
using BizHawk.Emulation.Common;
using BizHawk.Client.Common;


namespace BizHawk.Client.EmuHawk
{
	[Description("A library for manipulating the EmuHawk client UI")]
	public sealed class EmuHawkLuaLibrary : LuaLibraryBase
	{
		[RequiredService]
		public IEmulator Emulator { get; set; }
		[RequiredService]
		public IVideoProvider VideoProvider { get; set; }

		private readonly Dictionary<int, string> _filterMappings = new Dictionary<int, string>
			{
				{ 0, "None" },
				{ 1, "x2SAI" },
				{ 2, "SuperX2SAI" },
				{ 3, "SuperEagle" },
				{ 4, "Scanlines" },
			};

		public EmuHawkLuaLibrary(Lua lua)
			: base(lua) { }

		public EmuHawkLuaLibrary(Lua lua, Action<string> logOutputCallback)
			: base(lua, logOutputCallback) { }

		public override string Name { get { return "client"; } }

		[LuaMethodAttributes(
			"borderheight",
			"Gets the current height in pixels of the border around the emulator's drawing area"
		)]
		public static int BorderHeight()
		{
			var point = new System.Drawing.Point(0, 0);
			return GlobalWin.DisplayManager.TransformPoint(point).Y;
		}

		[LuaMethodAttributes(
			"borderwidth",
			"Gets the current width in pixels of the border around the emulator's drawing area"
		)]
		public static int BorderWidth()
		{
			var point = new System.Drawing.Point(0, 0);
			return GlobalWin.DisplayManager.TransformPoint(point).X;
		}

		[LuaMethodAttributes(
			"bufferheight",
			"Gets the current height in pixels of the emulator's drawing area"
		)]
		public int BufferHeight()
		{
			var height = VideoProvider.BufferHeight;
			var point = new System.Drawing.Point(0, height);

			return GlobalWin.DisplayManager.TransformPoint(point).Y - BorderHeight();
		}

		[LuaMethodAttributes(
			"bufferwidth",
			"Gets the current width in pixels of the emulator's drawing area"
		)]
		public int BufferWidth()
		{
			var width = VideoProvider.BufferWidth;
			var point = new System.Drawing.Point(width, 0);

			return GlobalWin.DisplayManager.TransformPoint(point).X - BorderWidth();
		}

		[LuaMethodAttributes(
			"clearautohold",
			"Clears all autohold keys"
		)]
		public void ClearAutohold()
		{
			GlobalWin.MainForm.ClearHolds();
		}

		[LuaMethodAttributes(
			"closerom",
			"Closes the loaded Rom"
		)]
		public static void CloseRom()
		{
			GlobalWin.MainForm.CloseRom();
		}

		[LuaMethodAttributes(
			"enablerewind",
			"Sets whether or not the rewind feature is enabled"
		)]
		public void EnableRewind(bool enabled)
		{
			GlobalWin.MainForm.EnableRewind(enabled);
		}

		[LuaMethodAttributes(
			"frameskip",
			"Sets the frame skip value of the client UI"
		)]
		public void FrameSkip(int numFrames)
		{
			if (numFrames > 0)
			{
				Global.Config.FrameSkip = numFrames;
				GlobalWin.MainForm.FrameSkipMessage();
			}
			else
			{
				ConsoleLuaLibrary.Log("Invalid frame skip value");
			}
		}

		[LuaMethodAttributes(
			"gettargetscanlineintensity",
			"Gets the current scanline intensity setting, used for the scanline display filter"
		)]
		public static int GetTargetScanlineIntensity()
		{
			return Global.Config.TargetScanlineFilterIntensity;
		}

		[LuaMethodAttributes(
			"getwindowsize",
			"Gets the main window's size Possible values are 1, 2, 3, 4, 5, and 10"
		)]
		public static int GetWindowSize()
		{
			return Global.Config.TargetZoomFactor;
		}

		[LuaMethodAttributes(
			"ispaused",
			"Returns true if emulator is paused, otherwise, false"
		)]
		public static bool IsPaused()
		{
			return GlobalWin.MainForm.EmulatorPaused;
		}

		[LuaMethodAttributes(
			"opencheats",
			"opens the Cheats dialog"
		)]
		public static void OpenCheats()
		{
			GlobalWin.Tools.Load<Cheats>();
		}

		[LuaMethodAttributes(
			"openhexeditor",
			"opens the Hex Editor dialog"
		)]
		public static void OpenHexEditor()
		{
			GlobalWin.Tools.Load<HexEditor>();
		}

		[LuaMethodAttributes(
			"openramwatch",
			"opens the Ram Watch dialog"
		)]
		public static void OpenRamWatch()
		{
			GlobalWin.Tools.LoadRamWatch(loadDialog: true);
		}

		[LuaMethodAttributes(
			"openramsearch",
			"opens the Ram Search dialog"
		)]
		public static void OpenRamSearch()
		{
			GlobalWin.Tools.Load<RamSearch>();
		}

		[LuaMethodAttributes(
			"openrom",
			"opens the Open ROM dialog"
		)]
		public static void OpenRom(string path)
		{
			GlobalWin.MainForm.LoadRom(path);
		}

		[LuaMethodAttributes(
			"opentasstudio",
			"opens the TAStudio dialog"
		)]
		public static void OpenTasStudio()
		{
			GlobalWin.Tools.Load<TAStudio>();
		}

		[LuaMethodAttributes(
			"opentoolbox",
			"opens the Toolbox Dialog"
		)]
		public static void OpenToolBox()
		{
			GlobalWin.Tools.Load<ToolBox>();
		}

		[LuaMethodAttributes(
			"opentracelogger",
			"opens the tracelogger if it is available for the given core"
		)]
		public static void OpenTraceLogger()
		{
			GlobalWin.Tools.Load<TraceLogger>();
		}

		[LuaMethodAttributes(
			"paint",
			"Causes the client UI to repaint the screen"
		)]
		public static void Paint()
		{
			GlobalWin.DisplayManager.NeedsToPaint = true;
		}

		[LuaMethodAttributes(
			"pause",
			"Pauses the emulator"
		)]
		public static void Pause()
		{
			GlobalWin.MainForm.PauseEmulator();
		}

		[LuaMethodAttributes(
			"pause_av",
			"If currently capturing Audio/Video, this will suspend the record. Frames will not be captured into the AV until client.unpause_av() is called"
		)]
		public static void PauseAv()
		{
			GlobalWin.MainForm.PauseAVI = true;
		}

		[LuaMethodAttributes(
			"reboot_core",
			"Reboots the currently loaded core"
		)]
		public static void RebootCore()
		{
			GlobalWin.MainForm.RebootCore();
		}

		[LuaMethodAttributes(
			"screenheight",
			"Gets the current width in pixels of the emulator's drawing area"
		)]
		public static int ScreenHeight()
		{
			return GlobalWin.MainForm.PresentationPanel.NativeSize.Height;
		}

		[LuaMethodAttributes(
			"screenshot",
			"if a parameter is passed it will function as the Screenshot As menu item of EmuHawk, else it will function as the Screenshot menu item"
		)]
		public static void Screenshot(string path = null)
		{
			if (path == null)
			{
				GlobalWin.MainForm.TakeScreenshot();
			}
			else
			{
				GlobalWin.MainForm.TakeScreenshot(path);
			}
		}

		[LuaMethodAttributes(
			"screenshottoclipboard",
			"Performs the same function as EmuHawk's Screenshot To Clipboard menu item"
		)]
		public static void ScreenshotToClipboard()
		{
			GlobalWin.MainForm.TakeScreenshotToClipboard();
		}

		[LuaMethodAttributes(
			"settargetscanlineintensity",
			"Sets the current scanline intensity setting, used for the scanline display filter"
		)]
		public static void SetTargetScanlineIntensity(int val)
		{
			Global.Config.TargetScanlineFilterIntensity = val;
		}

		[LuaMethodAttributes(
			"setscreenshotosd",
			"Sets the screenshot Capture OSD property of the client"
		)]
		public static void SetScreenshotOSD(bool value)
		{
			Global.Config.Screenshot_CaptureOSD = value;
		}

		[LuaMethodAttributes(
			"screenwidth",
			"Gets the current height in pixels of the emulator's drawing area"
		)]
		public static int ScreenWidth()
		{
			return GlobalWin.MainForm.PresentationPanel.NativeSize.Width;
		}

		[LuaMethodAttributes(
			"setwindowsize",
			"Sets the main window's size to the give value. Accepted values are 1, 2, 3, 4, 5, and 10"
		)]
		public void SetWindowSize(int size)
		{
			if (size == 1 || size == 2 || size == 3 || size == 4 || size == 5 || size == 10)
			{
				Global.Config.TargetZoomFactor = size;
				GlobalWin.MainForm.FrameBufferResized();
				GlobalWin.OSD.AddMessage("Window size set to " + size + "x");
			}
			else
			{
				Log("Invalid window size");
			}
		}

		[LuaMethodAttributes(
			"speedmode",
			"Sets the speed of the emulator (in terms of percent)"
		)]
		public void SpeedMode(int percent)
		{
			if (percent > 0 && percent < 6400)
			{
				GlobalWin.MainForm.ClickSpeedItem(percent);
			}
			else
			{
				Log("Invalid speed value");
			}
		}

		[LuaMethodAttributes(
			"togglepause",
			"Toggles the current pause state"
		)]
		public static void TogglePause()
		{
			GlobalWin.MainForm.TogglePause();
		}
		
		[LuaMethodAttributes(
			"transformPointX",
			"Transforms an x-coordinate in emulator space to an x-coordinate in client space"
		)]
		public static int TransformPointX(int x)
		{
			var point = new System.Drawing.Point(x, 0);
			return GlobalWin.DisplayManager.TransformPoint(point).X;
		}

		[LuaMethodAttributes(
			"transformPointY",
			"Transforms an y-coordinate in emulator space to an y-coordinate in client space"
		)]
		public static int TransformPointY(int y)
		{
			var point = new System.Drawing.Point(0, y);
			return GlobalWin.DisplayManager.TransformPoint(point).Y;
		}

		[LuaMethodAttributes(
			"unpause",
			"Unpauses the emulator"
		)]
		public static void Unpause()
		{
			GlobalWin.MainForm.UnpauseEmulator();
		}

		[LuaMethodAttributes(
			"unpause_av",
			"If currently capturing Audio/Video this resumes capturing"
		)]
		public static void UnpauseAv()
		{
			GlobalWin.MainForm.PauseAVI = false;
		}

		[LuaMethodAttributes(
			"xpos",
			"Returns the x value of the screen position where the client currently sits"
		)]
		public static int Xpos()
		{
			return GlobalWin.MainForm.DesktopLocation.X;
		}

		[LuaMethodAttributes(
			"ypos",
			"Returns the y value of the screen position where the client currently sits"
		)]
		public static int Ypos()
		{
			return GlobalWin.MainForm.DesktopLocation.Y;
		}

		[LuaMethodAttributes(
			"getavailabletools",
			"Returns a list of the tools currently open"
		)]
		public LuaTable GetAvailableTools()
		{
			var t = Lua.NewTable();
			var tools = GlobalWin.Tools.AvailableTools.ToList();
			for (int i = 0; i < tools.Count; i++)
			{
				t[i] = tools[i].Name.ToLower();
			}

			return t;
		}

		[LuaMethodAttributes(
			"gettool",
			"Returns an object that represents a tool of the given name (not case sensitive). If the tool is not open, it will be loaded if available. Use gettools to get a list of names"
		)]
		public LuaTable GetTool(string name)
		{
			var toolType  = ReflectionUtil.GetTypeByName(name)
				.FirstOrDefault(x => typeof(IToolForm).IsAssignableFrom(x) && !x.IsInterface);

			if (toolType != null)
			{
				GlobalWin.Tools.Load(toolType);
			}

			var selectedTool = GlobalWin.Tools.AvailableTools
				.FirstOrDefault(tool => tool.GetType().Name.ToLower() == name.ToLower());

			if (selectedTool != null)
			{
				return LuaHelper.ToLuaTable(Lua, selectedTool);
			}

			return null;
		}

		[LuaMethodAttributes(
			"createinstance",
			"returns a default instance of the given type of object if it exists (not case sensitive). Note: This will only work on objects which have a parameterless constructor.  If no suitable type is found, or the type does not have a parameterless constructor, then nil is returned"
		)]
		public LuaTable CreateInstance(string name)
		{
			var possibleTypes = ReflectionUtil.GetTypeByName(name);

			if (possibleTypes.Any())
			{
				var instance = Activator.CreateInstance(possibleTypes.First());
				return LuaHelper.ToLuaTable(Lua, instance);
			}

			return null;
		}
	}
}
