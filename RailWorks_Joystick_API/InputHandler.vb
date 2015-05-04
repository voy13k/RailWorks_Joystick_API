Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.IO
Imports System.Runtime.Serialization

Public Class InputHandler
	
    <DllImport("user32.dll")> _
    Shared Function GetForegroundWindow() As IntPtr
    End Function
    <DllImport("user32.dll")> _
    Shared Function GetWindowText(ByVal hWnd As IntPtr, ByVal lpWindowText As System.Text.StringBuilder, _
    ByVal nMaxCount As Integer) As Integer
    End Function
	
    <DllImport("RailDriver.dll", CharSet:=CharSet.Auto)> _
    Shared Sub SetRailSimValue(ByVal Action As ActionList, ByVal Value As Single)
    End Sub

    <DllImport("RailDriver.dll", CharSet:=CharSet.Auto)> _
    Shared Function GetRailSimValue(ByVal Action As ActionList, ByVal Value As GetValueModifier) As Single
    End Function

    <DllImport("RailDriver.dll", CharSet:=CharSet.Auto)> _
    Shared Function GetRailSimConnected() As Boolean
    End Function

    <DllImport("RailDriver.dll", CharSet:=CharSet.Auto)> _
    Shared Sub SetRailSimConnected(ByVal Value As Boolean)
    End Sub

    <DllImport("RailDriver.dll", CharSet:=CharSet.Auto)> _
    Shared Sub SetRailDriverConnected(ByVal Value As Boolean)
    End Sub

    <DllImport("RailDriver.dll", CharSet:=CharSet.Auto)> _
    Shared Function GetRailSimCombinedThrottleBrake() As Boolean
    End Function

    <DllImport("RailDriver.dll", CharSet:=CharSet.Auto)> _
    Shared Function GetRailSimLocoChanged() As Boolean
    End Function

	Public Structure InputAxisStruct
		Public Value As Integer
		Public Delta As Integer
		Public Name As String

		Public Sub New(ByVal AxisName As String, ByVal AxisValue As Integer, ByVal AxisDelta As Integer)
			Name = AxisName
			Value = AxisValue
			Delta = AxisDelta
		End Sub

		Public Overrides Function ToString() As String
			Return String.Format("{0}:  {1,-20}Delta: {2}", Name, Value, Delta)
		End Function

	End Structure

	Public Structure InputButtonStruct
		Public Pressed As Boolean
		Public Name As String

		Public Sub New(ByVal ButtonName As String, ByVal ButtonPressed As Boolean)
			Name = ButtonName
			Pressed = ButtonPressed
		End Sub

		Public Overrides Function ToString() As String
			Return IIf(Pressed, String.Format("{0,-10}", Name), "").ToString
		End Function
	End Structure

	Public Structure DeviceStruct
		Public Name As String
		Public ID As Integer
		Public Axis As List(Of InputAxisStruct)
		Public Buttons As List(Of InputButtonStruct)

		Public Sub New(ByVal DeviceName As String, ByVal DeviceID As Integer)
			Name = DeviceName
			ID = DeviceID
		End Sub

		Public Function FindAxis(ByVal FindName As String) As Integer
			For I As Integer = 0 To Axis.Count - 1
				If Axis(I).Name = FindName Then Return I
			Next
			Return -1
		End Function

		Public Function FindButton(ByVal FindName As String) As Integer
			For I As Integer = 0 To Buttons.Count - 1
				If Buttons(I).Name = FindName Then Return I
			Next
			Return -1
		End Function

		Public Overrides Function ToString() As String
			Dim Txt As New System.Text.StringBuilder
			Txt.AppendLine("Device: " & Name & " (" & ID & ")")
			Txt.AppendLine("Axis:")
			For Each Axle As InputAxisStruct In Axis
				Txt.AppendLine("    " & Axle.ToString)
			Next
			Txt.AppendLine("Buttons:")
			Dim I As Integer = 0
			For Each Button As InputButtonStruct In Buttons
				Txt.Append(Button.ToString)
				If Not (Button.ToString = "") Then I += 1
				If I >= 8 Then
					Txt.AppendLine
					I = 0
				End If
			Next
			Return Txt.ToString
		End Function
	End Structure

	Public Enum ActionList As Integer
		Invalid = -1
		Reverser
		Throttle
		CombinedThrottle
		GearLever
		TrainBrake
		LocomotiveBrake
		DynamicBrake
		EmergencyBrake
		HandBrake
		WarningSystemReset
		StartStopEngine
		Horn
		Wipers
		Sander
		Headlights
		Pantograph
		FireboxDoor
		ExhaustInjectorSteam
		ExhaustInjectorWater
		LiveInjectorSteam
		LiveInjectorWater
		Damper
		Blower
		Stoking
		CylinderCock
		Waterscoop
		SmallCompressor
		AWS
		AWSReset
		Startup
		Speedometer
		' Events
		PromptSave
		ToggleLabels
		Toggle2DMap
		ToggleHud
		ToggleQut
		Pause
		DriverGuide
		ToggleRvNumber
		DialogAssignment
		SwitchJunktionAhead
		SwitchJunktionBehind
		LoadCargo
		UnloadCargo
		PassAtDangerAhead
		PassAtDangerBehind
		ManualCouple
		' Camera
		CabCamera
		FollowCamera
		HeadOutCamera
		RearCamera
		TrackSideCamera
		CarriageCamera
		CouplingCamera
		YardCamera
		SwitchToNextFrontCab
		SwitchToNextRearCab
		FreeCamera
		Vigilance
	End Enum

	Public Enum GetValueModifier As Integer
		Current
		Min
		Max
	End Enum

	Public Devices As New List(Of DeviceStruct)
	Public APIInterval As Integer = 33
	Private RailWorksThread As Thread
	Private _Running As Boolean
	Private _IsStopped As Boolean = True
	Public AssignFilter As Integer = 1500
	Public AxisNotchOffset As Single = 0.002
	
	Public Sub New(ByRef DeviceNames As List(Of String))
		For I As Integer = 0 To DeviceNames.Count - 1
			Dim Dev As New DeviceStruct(DeviceNames(I), I)
			Dev.Axis = New List(Of InputAxisStruct)
			Dev.Buttons = New List(Of InputButtonStruct)
			Devices.Add(Dev)
		Next
		LoadBlank
		Vigilance.MinDelay = My.Settings.MinDelay
		Vigilance.MaxDelay = My.Settings.MaxDelay
		Vigilance.EarlyTriggerChance = My.Settings.EarlyTriggerChance
		Vigilance.TrainSpeedOffsetFactor = My.Settings.TrainSpeedOffset
		Vigilance.MinimumTrainSpeed = My.Settings.MinimumTrainSpeed
		Vigilance.TriggerTimeout = My.Settings.TriggerTimeout
		Vigilance.UseVigilanceSystem = My.Settings.UseVigilance
	End Sub

	Public Sub UpdateAxis(ByVal DeviceID As Integer, ByVal AxisName As String, ByVal AxisValue As Integer, ByVal AxisDelta As Integer)
		Dim Dev As DeviceStruct = Devices(DeviceID)
		Dim Result As Integer = Dev.FindAxis(AxisName)
		If Result = -1 Then
			Dev.Axis.Add(New InputAxisStruct(AxisName, AxisValue, AxisDelta))
			Exit Sub
		End If
		Dev.Axis(Result) = New InputAxisStruct(AxisName, AxisValue, AxisDelta)
		Devices(DeviceID) = Dev
		CheckMappings(DeviceID, AxisName, AxisValue, AxisDelta)
	End Sub

	Public Sub UpdateButton(ByVal DeviceID As Integer, ByVal ButtonName As String, ByVal ButtonValue As Integer, ByVal ButtonDelta As Integer)
		Dim Dev As DeviceStruct = Devices(DeviceID)
		Dim Result As Integer = Dev.FindButton(ButtonName)
		Dim ButtonPressed As Boolean = CBool(ButtonValue)
		If Result = -1 Then
			Dev.Buttons.Add(New InputButtonStruct(ButtonName, ButtonPressed))
			Exit Sub
		End If
		Dev.Buttons(Result) = New InputButtonStruct(ButtonName, ButtonPressed)
		Devices(DeviceID) = Dev
		CheckMappings(DeviceID, ButtonName, ButtonValue, ButtonDelta)
	End Sub
	
	Public Sub StartAPI()
		_Running = True
		_IsStopped = False
		RailWorksThread = New Thread(AddressOf RailWorksThreadMain)
		RailWorksThread.IsBackground = True
		RailWorksThread.Start
	End Sub

	Public FileName As String = "LastUsed.CMap"
	Public Sub SaveMaps(Optional ByVal FilePath As String = "")
		If FilePath = "" Then FilePath = FileName
		Dim FStream As New FileStream(FilePath, FileMode.OpenOrCreate)
		Dim BFormatter As New Formatters.Binary.BinaryFormatter
		Try
			BFormatter.Serialize(FStream, Mappings)
		Catch ex As Exception
			MsgBox("The settings could not be saved to " & FilePath & "!")
		Finally
			FStream.Close
		End Try
	End Sub

	Public Sub LoadMaps(Optional ByVal FilePath As String = "")
		Dim FStream As FileStream
		Try
			FStream = New FileStream(FilePath, FileMode.Open)
			Dim BFormatter As New Formatters.Binary.BinaryFormatter

			Mappings.Clear
			Mappings = CType(BFormatter.Deserialize(FStream), List(Of Map))
		Catch ex As Exception
			MsgBox("The settings could not be loaded from " & FilePath & "!")
		Finally
			If FStream IsNot Nothing Then FStream.Close
		End Try
	End Sub

	Public Sub ShiftMaps(ByVal Direction As Integer)
		Dim CMap As Map
		Dim IMap As InputMap

		For I As Integer = 0 To Mappings.Count - 1
			CMap = Mappings(I)
			Removed:
			For J As Integer = 0 To CMap.InputMaps.Count - 1
				IMap = CMap.InputMaps(J)
				IMap.DeviceID += Direction
				If IMap.DeviceID < 0 Or IMap.DeviceID > DeviceNames.Count - 1 Then
					CMap.InputMaps.RemoveAt(J)
					GoTo Removed
				End If
				CMap.InputMaps(J) = IMap
			Next
			Mappings(I) = CMap
		Next
	End Sub

	Public Sub StopAPI()
		SaveMaps
		My.Settings.MinDelay = Vigilance.MinDelay
		My.Settings.MaxDelay = Vigilance.MaxDelay
		My.Settings.EarlyTriggerChance = Vigilance.EarlyTriggerChance
		My.Settings.TrainSpeedOffset = Vigilance.TrainSpeedOffsetFactor
		My.Settings.MinimumTrainSpeed = Vigilance.MinimumTrainSpeed
		My.Settings.TriggerTimeout = Vigilance.TriggerTimeout
		My.Settings.UseVigilance = Vigilance.UseVigilanceSystem
		_Running = False
	End Sub

	Public Function IsStopped() As Boolean
		Return _IsStopped
	End Function

	Public Sub RailWorksThreadMain()
		
		SetRailSimConnected(True)
		SetRailDriverConnected(True)
		While _Running
			Dim CurHWnd As IntPtr = GetForegroundWindow
			Dim CurWindowText As New System.Text.StringBuilder(255)
			GetWindowText(CurHWnd, CurWindowText, 255)
            If CurWindowText.ToString = "RailWorks" Or CurWindowText.ToString = "Train Simulator 2015" Then
                Vigilance.InGame = True
                SendToRW()
                Vigilance.LastTrainSpeed = Math.Abs(GetRailSimValue(ActionList.Speedometer, GetValueModifier.Current))
                Debug.Print(GetRailSimValue(ActionList.EmergencyBrake, GetValueModifier.Current).ToString)
                If (ControlsWindow.Visible) Then
                    GetCurrentValues()
                End If
            Else
                Vigilance.InGame = False
            End If
			Thread.Sleep(APIInterval)
		End While
		_IsStopped = True
	End Sub

	Private Delegate Sub OneShotDelegate(ByVal Action As ActionList, ByVal Value As Single)
	Private Class OneShotArgs
		Public Action As ActionList
		Public Value As Single
		Public OneShotDel As OneShotDelegate

		Public Sub OneShot
			OneShotDel(Action, Value)
		End Sub
	End Class

	Public Vigilance As New VigilanceSystem	

	Public Sub SendToRW()
		Dim Maps As Map() = Mappings.ToArray
		For Each CMap In Maps
			If CMap.InputMaps.Count > 0 Then
				If CMap.Action = ActionList.Vigilance Then
					Dim Value As Single = CMap.GetOutputPercent
					If Value > 0.5 Then 
						Vigilance.ButtonState = VigilanceSystem.Actions.Press
					Else
						Vigilance.ButtonState = VigilanceSystem.Actions.Release
					End If
					Continue For
				End If
				If Vigilance.State = VigilanceSystem.VigilanceState.Triggered Then
					SetRailSimValue(ActionList.EmergencyBrake, 1)
					Continue For
				End If
				If CMap.APIType = InputMap.ControlType.Button Then
					If CMap.OneTimeShotConsumed Then Continue For
					CMap.OneTimeShotConsumed = True
					Dim OneShot As New OneShotArgs
					OneShot.Action = CMap.Action
					OneShot.Value = CMap.GetOutputPercent
					OneShot.OneShotDel = AddressOf SendToRWOneShot
					Dim OneShotThread As New Thread(AddressOf OneShot.OneShot)
					OneShotThread.IsBackground = True
					OneShotThread.Start
				Else
					Dim Value As Single = CMap.GetOutputPercent
					'Debug.Print("Sending " & Value & " to game with control " & CMap.Action.ToString)
					Select Case CMap.Action
						Case ActionList.Reverser
							Value = -1 + Value * 2
							SetRailSimValue(CMap.Action, Value)
						Case ActionList.Throttle
							SetRailSimValue(CMap.Action, Value)
							SetRailSimValue(ActionList.CombinedThrottle, Value)
						Case Else
							SetRailSimValue(CMap.Action, Value)
					End Select
				End If
			End If
		Next
	End Sub

	Public Sub SendToRWOneShot(ByVal Action As ActionList, ByVal Value As Single)
		Select Case Action
		' The following are treated differently
		Case ActionList.Sander, ActionList.EmergencyBrake, ActionList.Horn, ActionList.SwitchJunktionAhead, ActionList.SwitchJunktionBehind, ActionList.SwitchToNextFrontCab, ActionList.SwitchToNextRearCab
			SetRailSimValue(Action, Value)
		Case ActionList.LoadCargo
			SetRailSimValue(Action, Value)
			SetRailSimValue(ActionList.UnloadCargo, Value)
			Thread.Sleep(50)
			SetRailSimValue(Action, 0)
			SetRailSimValue(ActionList.UnloadCargo, 0)
		Case Else
			SetRailSimValue(Action, Value)
			Thread.Sleep(100)
			SetRailSimValue(Action, 0)
		End Select
		
	End Sub

	Private Delegate Sub GetFromRWDelegate()

	Public Sub GetCurrentValues()
		If (ControlsWindow.InvokeRequired) Then
			Dim Del As New GetFromRWDelegate(AddressOf GetCurrentValues)
			ControlsWindow.Invoke(Del)
			Exit Sub
		End If
		Dim DataMap As New Dictionary(Of String, Single)
		' Get Speedometer
		DataMap.Add("Speed", Math.Abs(GetRailSimValue(ActionList.Speedometer, GetValueModifier.Current)))

		' Get throttle etc
		' TODO: Move this someplace else?
		DataMap.Add("Throttle", Mappings(ActionList.Throttle).GetOutputPercent)
		DataMap.Add("TrainBrake", Mappings(ActionList.TrainBrake).GetOutputPercent)
		DataMap.Add("LocomotiveBrake", Mappings(ActionList.LocomotiveBrake).GetOutputPercent)
		DataMap.Add("DynamicBrake", Mappings(ActionList.DynamicBrake).GetOutputPercent)
		DataMap.Add("Reverser", Mappings(ActionList.Reverser).GetOutputPercent)
		DataMap.Add("Gear", Mappings(ActionList.GearLever).GetOutputPercent)

		ControlsWindow.UpdateControls(DataMap)

	End Sub

	Public Mappings As New List(Of Map)

	Public Sub LoadBlank()
		
		Mappings.Clear

		Mappings.Add(New Map(ActionList.Reverser, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.Throttle, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.CombinedThrottle, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.GearLever, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.TrainBrake, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.LocomotiveBrake, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.DynamicBrake, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.EmergencyBrake, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.HandBrake, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.WarningSystemReset, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.StartStopEngine, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.Horn, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.Wipers, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.Sander, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.Headlights, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.Pantograph, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.FireboxDoor, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.ExhaustInjectorSteam, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.ExhaustInjectorWater, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.LiveInjectorSteam, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.LiveInjectorWater, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.Damper, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.Blower, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.Stoking, InputMap.ControlType.Axis))
		Mappings.Add(New Map(ActionList.CylinderCock, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.Waterscoop, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.SmallCompressor, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.AWS, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.AWSReset, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.Startup, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.Speedometer, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.PromptSave, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.ToggleLabels, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.Toggle2DMap, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.ToggleHud, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.ToggleQut, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.Pause, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.DriverGuide, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.ToggleRvNumber, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.DialogAssignment, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.SwitchJunktionAhead, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.SwitchJunktionBehind, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.LoadCargo, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.UnloadCargo, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.PassAtDangerAhead, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.PassAtDangerBehind, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.ManualCouple, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.CabCamera, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.FollowCamera, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.HeadOutCamera, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.RearCamera, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.TrackSideCamera, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.CarriageCamera, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.CouplingCamera, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.YardCamera, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.SwitchToNextFrontCab, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.SwitchToNextRearCab, InputMap.ControlType.Button))
		Mappings.Add(New Map(ActionList.FreeCamera, InputMap.ControlType.Button))
		' Vigilance system is NOT part of the Raildriver dll.
		Mappings.Add(New Map(ActionList.Vigilance, InputMap.ControlType.Button))

	End Sub

	Private Sub CheckMappings(ByVal DeviceID As Integer, ByVal DeviceInputName As String, ByVal Value As Integer, ByVal Delta As Integer)
		Dim CMap As Map
		For I As Integer = 0 To Mappings.Count - 1
			CMap = Mappings(I)
			CheckInputMappings(CMap, DeviceID, DeviceInputName, Value, Delta)
			Mappings(I) = CMap
			If I = ControlManager.APIMapIndex And ControlManager.Visible Then ControlManager.UpdateSubscription
		Next
	End Sub

	Private Sub CheckInputMappings(ByRef CMap As Map, ByVal DeviceID As Integer, ByVal DeviceInputName As String, ByVal Value As Integer, ByVal Delta As Integer)
		Dim IMap As InputMap
		Dim WasAssigned As Boolean = False
		For I As Integer = 0 To CMap.InputMaps.Count - 1
			IMap = CMap.InputMaps(I)
			If (IMap.TreatAs = InputMap.ControlType.NotAssigned) Then
				IMap.MapName = ""
				IMap.DeviceID = -1
				Continue For
			End If
			If IMap.TreatAs = InputMap.ControlType.Assigning And (Math.Abs(Delta) >= AssignFilter Or DeviceInputName.Contains("B") or DeviceInputName.Contains("Pov")) Then
				IMap.DeviceID = DeviceID
				IMap.MapName = DeviceInputName
				If IMap.MapName.Contains("B") Or IMap.MapName.Contains("Pov") Then
					IMap.TreatAs = InputMap.ControlType.Button
				Else
					IMap.TreatAs = InputMap.ControlType.Axis
				End If
				If Delta < 0 Then IMap.Direction = InputMap.ControlDirection.Negative
				If Delta > 0 Then IMap.Direction = InputMap.ControlDirection.Positive
				If Delta = 0 Then IMap.Direction = InputMap.ControlDirection.Neutral ' This shall NOT happen!
				WasAssigned = True
			End If
			If (IMap.DeviceID = DeviceID And IMap.MapName = DeviceInputName) Then
				IMap.DeviceValue = Value
				IMap.DeviceDelta = Delta
				CMap.InputMaps(I) = IMap
				If WasAssigned Then 
					ControlManager.LoadControlMap
					WasAssigned = False
				End If
			End If
		Next
	End Sub

	Public Sub StopAssigning()
		Dim CMap As Map
		Dim IMap As InputMap
		For I As Integer = 0 To Mappings.Count - 1
			CMap = Mappings(I)
			For J As Integer = 0 To Mappings(I).InputMaps.Count - 1
				IMap = CMap.InputMaps(J)
				If IMap.TreatAs = InputMap.ControlType.Assigning Then
					IMap.TreatAs = InputMap.ControlType.NotAssigned
					CMap.InputMaps(J) = IMap
					Mappings(I) = CMap
				End If
			Next
		Next
	End Sub
End Class


' Map Class
<Serializable()> Public Class Map
	Public Action As InputHandler.ActionList
	Public APIType As InputMap.ControlType
	Public InputMaps As List(Of InputMap)
	Public OneTimeShotConsumed As Boolean

	Public LatchedValue As Single
	Private RelativeLatch As Single
	Private LastInput As Integer = -1
	Private Toggle As Integer

	Public Sub New(ByVal MapAction As InputHandler.ActionList, ByVal MapAPIType As InputMap.ControlType)
		Action = MapAction
		APIType = MapAPIType
		InputMaps = New List(Of InputMap)
	End Sub

	Public Sub AddInput(ByRef IMap As InputMap)
		AddHandler IMap.GotInput, AddressOf InputMaps_GotInput
		InputMaps.Add(IMap)
	End Sub

	Public Function GetPercent() As Single
		Dim IMap As InputMap
		If (LastInput > InputMaps.Count - 1) Then LastInput = -1
		If (LastInput = -1) Then
			If InputMaps.Count = 0 Then Return 0
			LastInput = 0
		End If
		IMap = InputMaps(LastInput)
		Return Clamp(IMap.GetPercent, 0, 1)
	End Function

	Public Function GetOutputPercent() As Single
		Dim IMap As InputMap
		If (LastInput > InputMaps.Count - 1) Then LastInput = -1
		If (LastInput = -1) Then
			If InputMaps.Count = 0 Then Return 0
			LastInput = 0
		End If
		IMap = InputMaps(LastInput)
		Select Case IMap.MapMode
			Case InputMap.ControlMode.Slider To InputMap.ControlMode.NotchedSliderTargets
				If IMap.IsNegativePositiveRange Then Return Clamp(IMap.GetOutputPercent, -1, 1)
				Return Clamp(IMap.GetOutputPercent, 0, 1)
			Case Else
				If IMap.IsNegativePositiveRange Then Return Clamp(LatchedValue, -1, 1)
				Return Clamp(LatchedValue, 0, 1)
		End Select
	End Function

	Private Sub InputMaps_GotInput(ByRef IMap As InputMap)
		LastInput = InputMaps.IndexOf(IMap)
		OneTimeShotConsumed = False
		Select Case IMap.MapMode
			Case InputMap.ControlMode.Slider To InputMap.ControlMode.NotchedSliderTargets
			Case InputMap.ControlMode.UpDownLatch
				' TODO: Check if we can use direction here too...
				If IMap.GetOutputPercent > 0.501 And IMap.GetOutputPercent > LatchedValue Then
					LatchedValue = IMap.GetOutputPercent
				ElseIf IMap.GetOutputPercent < 0.499 And IMap.GetOutputPercent < LatchedValue
					LatchedValue = IMap.GetOutputPercent
				End If
			Case InputMap.ControlMode.UpDownRelative
				Dim OutputPercent As Single = IMap.GetOutputPercent
				Dim DeltaValue As Single
				If OutputPercent > 0.501 And ((IMap.DeviceDelta > 0 And IMap.Direction = InputMap.ControlDirection.Positive) Or (IMap.DeviceDelta < 0 And IMap.Direction = InputMap.ControlDirection.Negative)) Then
					DeltaValue = CSng(OutputPercent - RelativeLatch)
					If DeltaValue > 0 Then LatchedValue += DeltaValue * 2
				ElseIf IMap.GetOutputPercent < 0.499 And ((IMap.DeviceDelta < 0 And IMap.Direction = InputMap.ControlDirection.Positive) Or (IMap.DeviceDelta > 0 And IMap.Direction = InputMap.ControlDirection.Negative)) Then
					DeltaValue = CSng(OutputPercent - RelativeLatch)
					If DeltaValue < 0 Then LatchedValue += DeltaValue * 2
				End If
				LatchedValue = Clamp(LatchedValue, 0, 1)
				RelativeLatch = OutputPercent
			Case InputMap.ControlMode.Toggle
				If IMap.NotchRanges.Count = 0 Then Exit Sub
				If IMap.DeviceDelta > 0 And IMap.Direction = InputMap.ControlDirection.Positive Then
					Toggle += 1
				End If
				If IMap.DeviceDelta > 0 And IMap.Direction = InputMap.ControlDirection.Negative Then
					Toggle -= 1
				End If
				If Toggle >= IMap.NotchRanges.Count Then Toggle = 0
				If Toggle < 0 Then Toggle = IMap.NotchRanges.Count - 1
				LatchedValue = Clamp(IMap.NotchRanges(Toggle), 0, 1)
		End Select
	End Sub
End Class


' InputMap Class
<Serializable()> Public Class InputMap

	Public Enum ControlDirection As Integer
		Negative = -1
		Neutral = 0
		Positive = 1
	End Enum
	Public Enum ControlType As Integer
		Assigning = -1
		NotAssigned
		Axis
		Button
	End Enum

	Public Enum ControlMode As Integer
		Slider
		NotchedSlider
		NotchedSliderRanges
		NotchedSliderTargets
		UpDownLatch
		UpDownRelative
		Toggle
	End Enum

	Public DeviceID As Integer
	Public ReadOnly Property DeviceName As String
		Get
			If DeviceID < DeviceNames.Count Then
				Return DeviceNames(DeviceID)
			End If
			Return "<DEVICE DISCONNECTED>"
		End Get
	End Property
	Public MapName As String
	Public MapMode As ControlMode
	Private _DeviceValue As Integer
	Public Property DeviceValue As Integer
	Get
		Return _DeviceValue
	End Get
	Set(ByVal Value As Integer)
		DeviceDelta = Value - _DeviceValue
		_DeviceValue = Value
		RaiseEvent GotInput(Me)
	End Set
	End Property
	Public DeviceDelta As Integer
	Public Direction As ControlDirection
	Public TreatAs As ControlType

	Public Notches As List(Of Single)
	Public NotchRanges As List(Of Single)
	Public IsNegativePositiveRange As Boolean = False

	Public Event GotInput(ByRef IMap As InputMap)

	Public Sub New(ByVal TreatAsValue As ControlType)
		TreatAs = TreatAsValue
		MapMode = ControlMode.Slider
		Notches = New List(Of Single)
		NotchRanges = New List(Of Single)
	End Sub

	Public Overrides Function ToString() As String
		If (TreatAs = ControlType.NotAssigned) Then 
			Return "<NOT ASSIGNED>"
		ElseIf (TreatAs = ControlType.Assigning) Then
			Return "<ASSIGNING...>"
		Else
			Return String.Format("{0} ({1}), {2}, {3}", DeviceName, DeviceID, MapName, Direction.ToString)
		End If
	End Function

	Public Function GetPercent() As Single
			
		Select Case TreatAs
			Case ControlType.Axis
				If Direction = ControlDirection.Negative Then
					If IsNegativePositiveRange Then Return -1 + (Clamp(1 - CSng(_DeviceValue / 65535), -1, 1) * 2)
					Return Clamp(1 - CSng(_DeviceValue / 65535), 0, 1)
				Else
					If IsNegativePositiveRange Then Return -1 + (Clamp(CSng(_DeviceValue / 65535), -1, 1) * 2)
					Return Clamp(CSng(_DeviceValue / 65535), 0, 1)
				End If
			Case ControlType.Button
				If Direction = ControlDirection.Negative Then
					Return 1 - CSng(Clamp(_DeviceValue, 0, 1))
				Else
					Return Clamp(CSng(Clamp(_DeviceValue, 0, 1)), 0, 1)
				End If
		End Select
		Return 0

	End Function

	Public Function GetOutputPercent() As Single
		Dim Perc As Single = GetPercent
		Select Case MapMode
			Case ControlMode.Slider
				Return Perc
			Case ControlMode.NotchedSlider
				If Notches.Count > 0 Then
					Dim ClosestNotches As List(Of Integer) = GetClosestNotches(Perc, Notches)
					If IsNegativePositiveRange Then Return -1 + (Clamp(Notches(ClosestNotches(0)), -1, 1) * 2)
					Return Clamp(Notches(ClosestNotches(0)), 0, 1)
				Else
					Return 0
				End If
			Case ControlMode.NotchedSliderTargets
				If Notches.Count > 1 And NotchRanges.Count > 1 And Notches.Count <= NotchRanges.Count Then
					Dim ClosestNotches As List(Of Integer) = GetClosestNotches(Perc, Notches)
					If IsNegativePositiveRange Then Return -1 + (Clamp(NotchRanges(ClosestNotches(0)), -1, 1) * 2)
					Return Clamp(NotchRanges(ClosestNotches(0)), 0, 1)
				End If
			Case ControlMode.NotchedSliderRanges To ControlMode.UpDownRelative
				Dim ClosestNotches As List(Of Integer)
				Dim ClosestNotchAbove, ClosestNotchBelow As Integer
				Dim NotchRange, NotchPercent As Single
				Dim RangeRange As Single
				If Notches.Count > 1 And NotchRanges.Count > 1 And Notches.Count <= NotchRanges.Count Then
					ClosestNotches = GetClosestNotches(Perc, Notches)
					ClosestNotchBelow = GetClosestNotch(False, Perc, ClosestNotches, Notches)
					ClosestNotchAbove = GetClosestNotch(True, Perc, ClosestNotches, Notches)
					NotchRange = Notches(ClosestNotchAbove) - Notches(ClosestNotchBelow)
					If NotchRange = 0 Then
						NotchPercent = 0
					Else
						NotchPercent = (Perc - Notches(ClosestNotchBelow)) / NotchRange
					End If
					RangeRange = NotchRanges(ClosestNotchAbove) - NotchRanges(ClosestNotchBelow)
					If IsNegativePositiveRange Then Return -1 + (Clamp(NotchRanges(ClosestNotchBelow) + (RangeRange * NotchPercent), -1, 1) * 2)
					Return Clamp(NotchRanges(ClosestNotchBelow) + (RangeRange * NotchPercent), 0, 1)
				Else
					Return 0
				End If
			Case ControlMode.Toggle
				Return Perc
		End Select
		Return 0
	End Function

	Private Function GetClosestNotches(ByVal InputPercent As Single, ByVal NotchList As List(Of Single)) As List(Of Integer)
		Dim SortDict As New Dictionary(Of Single, Integer)
		Dim Notch As Single
		For I As Integer = 0 To NotchList.Count - 1
			Notch = NotchList(I)
			Dim KeyName As Single = Math.Abs(InputPercent - Notch)
			If SortDict.ContainsKey(KeyName) Then KeyName += CSng(0.001)
			SortDict.Add(Math.Abs(InputPercent - Notch), I)
		Next
		Dim Keys As List(Of Single) = SortDict.Keys.ToList
		Keys.Sort
		Dim Ret As New List(Of Integer)
		For Each Key In Keys
			Ret.Add(SortDict(Key))
		Next
		Return Ret
	End Function

	Private Function GetClosestNotch(ByVal Above As Boolean, ByVal InputPercent As Single, ByVal ClosestList As List(Of Integer), ByVal NotchList As List(Of Single)) As Integer
		Select Case Above
			Case True
				For I As Integer = 0 To ClosestList.Count - 1
					If (NotchList(ClosestList(I)) >= InputPercent) Then Return ClosestList(I)
				Next
				Return 1
			Case False
				For I As Integer = 0 To ClosestList.Count - 1
					If (NotchList(ClosestList(I)) < InputPercent) Then Return ClosestList(I)
				Next
				Return 0
		End Select
		Return 0
	End Function

End Class