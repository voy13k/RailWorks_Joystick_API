Imports System.Math

Module Globals
	
	Public WithEvents JoyDevices As Joystick
	Public DeviceNames As List(Of String)
	Public WithEvents IHandler As InputHandler
	Public DevStatus As TextBox
	Public DevStatusID As Integer = -1

	Public ControlManager As New frmManageControls
	Public ControlsWindow As New frmControls
	Public SpeedoWindow As New frmSpeedo

	Private Delegate Sub UpdateDeviceDebugDelegate(ByVal DeviceID As Integer)

	Public Function Clamp(ByVal Value As Single, ByVal MinValue As Single, ByVal MaxValue As Single) As Single
		Return Max(MinValue, Min(MaxValue, Value))
	End Function

	Public Function Clamp(ByVal Value As Integer, ByVal MinValue As Integer, ByVal MaxValue As Integer) As Integer
		Return Max(MinValue, Min(MaxValue, Value))
	End Function
	
	Public Sub AxisChanged(ByVal DeviceName As String, ByVal DeviceID As Integer, ByVal AxisName As String, ByVal NewValue As Integer, ByVal Delta As Integer) Handles JoyDevices.DeviceAxisChanged
		IHandler.UpdateAxis(DeviceID, AxisName, NewValue, Delta)
		Dim Del As New UpdateDeviceDebugDelegate(AddressOf UpdateDeviceDebug)
		DevStatus.Invoke(Del, DeviceID)
	End Sub
	Public Sub ButtonChanged(ByVal DeviceName As String, ByVal DeviceID As Integer, ByVal ButtonName As String, ByVal NewValue As Integer, ByVal Delta As Integer) Handles JoyDevices.DeviceButtonChanged
		IHandler.UpdateButton(DeviceID, ButtonName, NewValue, Delta)
		Dim Del As New UpdateDeviceDebugDelegate(AddressOf UpdateDeviceDebug)
		DevStatus.Invoke(Del, DeviceID)
	End Sub

	Public Sub UpdateDeviceDebug(ByVal DeviceID As Integer)
		If DevStatusID = -1 Then
			DevStatus.Text = IHandler.Devices(DeviceID).ToString
		ElseIf DevStatusID = DeviceID Then
			DevStatus.Text = IHandler.Devices(DeviceID).ToString
		End If
	End Sub

	Public Function IntToStr(ByVal Int As Integer) As String
		Return Int.ToString
	End Function
	

End Module
