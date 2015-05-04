Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D

Public Class frmControls

	Public Speed As Single
	Public Cycle As Single
	Public ActiveButton As Integer

	Private Sub SetupAnalogControl(ByVal ControlName As String, ByVal Description As String) Handles ControlsGUI.SetupAnalogControls
		SetupMap(ControlName, Description)
		ControlManager.Show
	End Sub
	Private Sub SetupButtonControl(ByVal ControlName As String, ByVal Description As String) Handles ControlsGUI.SetupButtonControls
		SetupMap(ControlName, Description)
		ControlManager.Show
	End Sub

	Public Sub UpdateControls(ByVal DataMap As Dictionary(Of String, Single))
		
		' Update the speedometer
		ControlsGUI.Speed = DataMap("Speed")
		If SpeedoWindow.Visible = True Then SpeedoWindow.SetSpeed(DataMap("Speed"))
		
		If DataMap.Count = 1 Then Exit Sub
		' Update throttle etc
		ControlsGUI.Throttle.SetPercent(DataMap("Throttle"))
		ControlsGUI.TrainBrakes.SetPercent(DataMap("TrainBrake"))
		ControlsGUI.LocomotiveBrakes.SetPercent(DataMap("LocomotiveBrake"))
		ControlsGUI.DynamicBrakes.SetPercent(DataMap("DynamicBrake"))
		ControlsGUI.Reverser.SetPercent(DataMap("Reverser"))
		ControlsGUI.Gear.SetPercent(DataMap("Gear"))

	End Sub

	Public Sub SetupMap(ByVal ControlName As String, ByVal Description As String)
		Select Case ControlName
			Case "Throttle"
				ControlManager.APIMapIndex = InputHandler.ActionList.Throttle
			Case "TrainBrake"
				ControlManager.APIMapIndex = InputHandler.ActionList.TrainBrake
			Case "LocomotiveBrake"
				ControlManager.APIMapIndex = InputHandler.ActionList.LocomotiveBrake
			Case "DynamicBrake"
				ControlManager.APIMapIndex = InputHandler.ActionList.DynamicBrake
			Case "Reverser"
				ControlManager.APIMapIndex = InputHandler.ActionList.Reverser
			Case "Gear"
				ControlManager.APIMapIndex = InputHandler.ActionList.GearLever
			Case "Wipers"
				ControlManager.APIMapIndex = InputHandler.ActionList.Wipers
			Case "Lights"
				ControlManager.APIMapIndex = InputHandler.ActionList.Headlights
			Case "Pantograph"
				ControlManager.APIMapIndex = InputHandler.ActionList.Pantograph
			Case "LoadUnload"
				ControlManager.APIMapIndex = InputHandler.ActionList.LoadCargo
			Case "Sander"
				ControlManager.APIMapIndex = InputHandler.ActionList.Sander
			Case "SmallEjector"
				ControlManager.APIMapIndex = InputHandler.ActionList.SmallCompressor
			Case "Horn"
				ControlManager.APIMapIndex = InputHandler.ActionList.Horn
			Case "CylinderCocks"
				ControlManager.APIMapIndex = InputHandler.ActionList.CylinderCock
			Case "HeadOutLeft"
				ControlManager.APIMapIndex = InputHandler.ActionList.HeadOutCamera
			Case "Cab"
				ControlManager.APIMapIndex = InputHandler.ActionList.CabCamera
			Case "HeadOutRight"
				ControlManager.APIMapIndex = InputHandler.ActionList.HeadOutCamera
			Case "Trackside"
				ControlManager.APIMapIndex = InputHandler.ActionList.TrainBrake
			Case "ExternalFront"
				ControlManager.APIMapIndex = InputHandler.ActionList.FollowCamera
			Case "ExternalRear"
				ControlManager.APIMapIndex = InputHandler.ActionList.RearCamera
			Case "NextCab"
				ControlManager.APIMapIndex = InputHandler.ActionList.SwitchToNextFrontCab
			Case "PreviousCab"
				ControlManager.APIMapIndex = InputHandler.ActionList.SwitchToNextRearCab
			Case "Coupling"
				ControlManager.APIMapIndex = InputHandler.ActionList.CouplingCamera
			Case "Yard"
				ControlManager.APIMapIndex = InputHandler.ActionList.YardCamera
			Case "FreeRoam"
				ControlManager.APIMapIndex = InputHandler.ActionList.FreeCamera
			Case "Starter"
				ControlManager.APIMapIndex = InputHandler.ActionList.StartStopEngine
			Case "ManualCoupling"
				ControlManager.APIMapIndex = InputHandler.ActionList.ManualCouple
			Case "AWS"
				ControlManager.APIMapIndex = InputHandler.ActionList.AWSReset
			Case "EBrake"
				ControlManager.APIMapIndex = InputHandler.ActionList.EmergencyBrake
			Case "PBrake"
				ControlManager.APIMapIndex = InputHandler.ActionList.HandBrake
			Case "DriverGuide"
				ControlManager.APIMapIndex = InputHandler.ActionList.DriverGuide
			Case "Map"
				ControlManager.APIMapIndex = InputHandler.ActionList.Toggle2DMap
			Case "PADAhead"
				ControlManager.APIMapIndex = InputHandler.ActionList.PassAtDangerAhead
			Case "PADBehind"
				ControlManager.APIMapIndex = InputHandler.ActionList.PassAtDangerBehind
			Case "JunctionAhead"
				ControlManager.APIMapIndex = InputHandler.ActionList.SwitchJunktionAhead
			Case "JunctionBehind"
				ControlManager.APIMapIndex = InputHandler.ActionList.SwitchJunktionBehind
			Case "Vigilance"
				ControlManager.APIMapIndex = InputHandler.ActionList.Vigilance
			Case Else
				Debug.Print("Unknown/Unhandled control: " & ControlName)
				Exit Sub
		End Select
		ControlManager.Description = Description
		ControlManager.InputMapIndex = -1
		ControlManager.LoadControlMap()
	End Sub

 	Private Sub frmControls_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		Me.Visible = False
		e.Cancel = True
	End Sub

	
	' Old testing code, not used...
	Private Spd As Single
	Private Sub Timer1_Tick( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles Timer1.Tick
		Timer1.Interval = 10
		Spd += CSng(0.3)
		Dim DataMap As New Dictionary(Of String, Single)
		DataMap.Add("Speed", Spd)
		UpdateControls(DataMap)
		If Spd > 250 Then Spd = 0
	End Sub
End Class