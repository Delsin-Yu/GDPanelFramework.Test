﻿using System;
using System.Diagnostics;
using GDPanelFramework.Panels;
using GdUnit4;
using Godot;
using GodotPanelFramework;
using GodotTask;
using Environment = System.Environment;

namespace GDPanelFramework.Tests;

public partial class UIPanel_CompositeInputTest : UIPanel
{
    public class TestMonitor
    {
        public bool Composite_Axis_Started { get; set; }
        public bool Composite_Axis_Negative_Updated { get; set; }
        public bool Composite_Axis_Positive_Updated { get; set; }
        public bool Composite_Axis_Ended { get; set; }
        
        public bool Composite_Vector_Started { get; set; }
        public bool Composite_Vector_Up_Updated { get; set; }
        public bool Composite_Vector_Down_Updated { get; set; }
        public bool Composite_Vector_Left_Updated { get; set; }
        public bool Composite_Vector_Right_Updated { get; set; }
        public bool Composite_Vector_Ended { get; set; }
    }

    public TestMonitor Monitor { get; set; }
    public ISceneRunner SceneRunner { get; set; }

    protected override void _OnPanelOpen()
    {
        void CallAxisStarted(float inputDirection) => Monitor.Composite_Axis_Started = !Monitor.Composite_Axis_Started;
        void CallAxisUpdated(float inputDirection)
        {
            switch (inputDirection)
            {
                case < 0:
                    Monitor.Composite_Axis_Negative_Updated = !Monitor.Composite_Axis_Negative_Updated;
                    break;
                case > 0:
                    Monitor.Composite_Axis_Positive_Updated = !Monitor.Composite_Axis_Positive_Updated;
                    break;
            }
        }
        void CallAxisEnded(float inputDirection) => Monitor.Composite_Axis_Ended = !Monitor.Composite_Axis_Ended;
        void CallVectorStarted(Vector2 inputDirection) => Monitor.Composite_Vector_Started = !Monitor.Composite_Vector_Started;
        void CallVectorUpdated(Vector2 inputDirection)
        {
            if (inputDirection == Vector2.Up) Monitor.Composite_Vector_Up_Updated = !Monitor.Composite_Vector_Up_Updated;
            if (inputDirection == Vector2.Down) Monitor.Composite_Vector_Down_Updated = !Monitor.Composite_Vector_Down_Updated;
            if (inputDirection == Vector2.Left) Monitor.Composite_Vector_Left_Updated = !Monitor.Composite_Vector_Left_Updated;
            if (inputDirection == Vector2.Right) Monitor.Composite_Vector_Right_Updated = !Monitor.Composite_Vector_Right_Updated;
        }
        void CallVectorEnded(Vector2 inputDirection) => Monitor.Composite_Vector_Ended = !Monitor.Composite_Vector_Ended;

        RegisterInputAxis(
            BuiltinInputNames.UILeft,
            BuiltinInputNames.UIRight,
            CallAxisStarted,
            CompositeInputActionState.Start
        );

        RegisterInputAxis(
            BuiltinInputNames.UILeft,
            BuiltinInputNames.UIRight,
            CallAxisUpdated,
            CompositeInputActionState.Update
        );

        RegisterInputAxis(
            BuiltinInputNames.UILeft,
            BuiltinInputNames.UIRight,
            CallAxisEnded,
            CompositeInputActionState.End
        );

        SceneRunner.SimulateKeyPress(Key.Left);
        SceneRunner.SimulateKeyPress(Key.Right);
        SceneRunner.SimulateKeyRelease(Key.Left);
        SceneRunner.SimulateKeyRelease(Key.Right);
        
        RemoveInputAxis(
            BuiltinInputNames.UILeft,
            BuiltinInputNames.UIRight,
            CallAxisStarted,
            CompositeInputActionState.Start
        );

        RemoveInputAxis(
            BuiltinInputNames.UILeft,
            BuiltinInputNames.UIRight,
            CallAxisUpdated,
            CompositeInputActionState.Update
        );

        RemoveInputAxis(
            BuiltinInputNames.UILeft,
            BuiltinInputNames.UIRight,
            CallAxisEnded,
            CompositeInputActionState.End
        );
        
        SceneRunner.SimulateKeyPress(Key.Left);
        SceneRunner.SimulateKeyPress(Key.Right);
        SceneRunner.SimulateKeyRelease(Key.Left);
        SceneRunner.SimulateKeyRelease(Key.Right);

        RegisterInputVector(
            BuiltinInputNames.UIUp,
            BuiltinInputNames.UIDown,
            BuiltinInputNames.UILeft,
            BuiltinInputNames.UIRight,      
            CallVectorStarted,
            CompositeInputActionState.Start
        );

        RegisterInputVector(
            BuiltinInputNames.UIUp,
            BuiltinInputNames.UIDown,
            BuiltinInputNames.UILeft,
            BuiltinInputNames.UIRight,
            CallVectorUpdated,
            CompositeInputActionState.Update
        );

        RegisterInputVector(
            BuiltinInputNames.UIUp,
            BuiltinInputNames.UIDown,
            BuiltinInputNames.UILeft,
            BuiltinInputNames.UIRight,    
            CallVectorEnded,
            CompositeInputActionState.End
        );
        SceneRunner.SimulateKeyPress(Key.Up);
        SceneRunner.SimulateKeyPress(Key.Right);
        SceneRunner.SimulateKeyRelease(Key.Up);
        SceneRunner.SimulateKeyPress(Key.Down);
        SceneRunner.SimulateKeyRelease(Key.Right);
        SceneRunner.SimulateKeyPress(Key.Left);
        SceneRunner.SimulateKeyRelease(Key.Down);
        SceneRunner.SimulateKeyRelease(Key.Left);
        
        RemoveInputVector(
            BuiltinInputNames.UIUp,
            BuiltinInputNames.UIDown,
            BuiltinInputNames.UILeft,
            BuiltinInputNames.UIRight,      
            CallVectorStarted,
            CompositeInputActionState.Start
        );

        RemoveInputVector(
            BuiltinInputNames.UIUp,
            BuiltinInputNames.UIDown,
            BuiltinInputNames.UILeft,
            BuiltinInputNames.UIRight,
            CallVectorUpdated,
            CompositeInputActionState.Update
        );

        RemoveInputVector(
            BuiltinInputNames.UIUp,
            BuiltinInputNames.UIDown,
            BuiltinInputNames.UILeft,
            BuiltinInputNames.UIRight,    
            CallVectorEnded,
            CompositeInputActionState.End
        );

        SceneRunner.SimulateKeyPress(Key.Up);
        SceneRunner.SimulateKeyPress(Key.Right);
        SceneRunner.SimulateKeyRelease(Key.Up);
        SceneRunner.SimulateKeyPress(Key.Down);
        SceneRunner.SimulateKeyRelease(Key.Right);
        SceneRunner.SimulateKeyPress(Key.Left);
        SceneRunner.SimulateKeyRelease(Key.Down);
        SceneRunner.SimulateKeyRelease(Key.Left);
        
        GDTask.NextFrame().ContinueWith(ClosePanel);
    }
}