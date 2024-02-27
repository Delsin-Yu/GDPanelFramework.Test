﻿using GDPanelFramework.Panels;
using GdUnit4;
using Godot;
using GodotPanelFramework;
using GodotTask;

namespace GDPanelFramework.Tests;

public partial class UIPanel_InputTest : UIPanel
{
    public class TestMonitor
    {
        public bool UIAcceptPressed { get; set; }
        public bool UIAcceptReleased { get; set; }
        public bool UICancelPressed { get; set; }
        public bool UICancelReleased { get; set; }
    }

    public TestMonitor Monitor { get; set; }
    public ISceneRunner SceneRunner { get; set; }


    protected override void _OnPanelOpen()
    {
        void Call1(InputEvent _) => Monitor.UIAcceptPressed = !Monitor.UIAcceptPressed;
        void Call2(InputEvent _) => Monitor.UIAcceptReleased = !Monitor.UIAcceptReleased;
        void Call3() => Monitor.UICancelPressed = !Monitor.UICancelPressed;
        void Call4() => Monitor.UICancelReleased = !Monitor.UICancelReleased;

        RegisterInput(BuiltinInputNames.UIAccept, Call1, InputActionPhase.Pressed);
        RegisterInput(BuiltinInputNames.UIAccept, Call2, InputActionPhase.Released);
        
        SceneRunner.SimulateKeyPressed(Key.Enter);
        
        RemoveInput(BuiltinInputNames.UIAccept, Call1, InputActionPhase.Pressed);
        RemoveInput(BuiltinInputNames.UIAccept, Call2, InputActionPhase.Released);
        
        SceneRunner.SimulateKeyPressed(Key.Enter);
        
        RegisterCancelInput(Call3, InputActionPhase.Pressed);
        RegisterCancelInput(Call4, InputActionPhase.Released);
        
        SceneRunner.SimulateKeyPressed(Key.Escape);
                
        RegisterCancelInput(Call3, InputActionPhase.Pressed);
        RegisterCancelInput(Call4, InputActionPhase.Released);

        SceneRunner.SimulateKeyPressed(Key.Escape);
        
        GDTask.NextFrame().ContinueWith(ClosePanel);
    }
}