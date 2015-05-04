Imports Microsoft.DirectX
Imports Microsoft.DirectX.DirectInput
Imports System.Threading

Public Class Joystick
	
	Private _Devices As New List(Of DirectInput.Device)
	Private _PollThread As Thread
	Private _Running As Boolean
	Private _IsStopped As Boolean = True
	Private _lastDeviceStates As New List(Of JoystickState)
	Public DeviceFound As Boolean
	Public PollInterval As Integer = 20
	Public AxisFilter As Integer = 0

	Public Event DeviceAxisChanged(ByVal DeviceName As String, ByVal DeviceID As Integer, ByVal AxisName As String, ByVal NewValue As Integer, ByVal Delta As Integer)
	Public Event DeviceButtonChanged(ByVal DeviceName As String, ByVal DeviceID As Integer, ByVal ButtonName As String, ByVal NewValue As Integer, ByVal Delta As Integer)

	Public Sub New(ByRef Handle As Form)
		' Get list of attached Joysticks fron DInput
		Dim _DeviceList As DeviceList = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly)
		' If there are any devices in the list we add them and aquire them
		If (_DeviceList.Count > 0) Then
			For Each DevInstance As DeviceInstance In _DeviceList
				Try
					Dim TmpDev As DirectInput.Device = New Device(DevInstance.InstanceGuid)
					' TODO: Check to make sure we can set the cooperative lever to 0
					TmpDev.SetCooperativeLevel(Handle, CooperativeLevelFlags.Background or CooperativeLevelFlags.NonExclusive)
					TmpDev.SetDataFormat(DeviceDataFormat.Joystick)
					TmpDev.Acquire()
					_Devices.Add(TmpDev)
					DeviceFound = True
				Catch ex As Exception
					DeviceFound = False
					Exit For
				End Try
			Next
		End If

	End Sub

	Public Sub StartPolling()
		
		_Running = True
		_IsStopped = False
		_PollThread = New Thread(AddressOf PollThreadMain)
		_PollThread.Start
		
	End Sub

	Public Sub StopPolling()
		_Running = False
	End Sub
	Public Function IsStopped As Boolean
		Return _IsStopped
	End Function

	Public Function GetDeviceNames() As List(Of String)
		Dim Result As New List(Of String)
		For Each Dev As Device In _Devices
			Result.Add(Dev.DeviceInformation.ProductName)
		Next
		Return Result
	End Function

	Private Sub PollThreadMain()
		
		Dim _Dev As DirectInput.Device
		While _Running
			For I = 0 To _Devices.Count - 1
				_Dev = _Devices(I)
				_Dev.Poll
				Dim _DeviceState As JoystickState
				_DeviceState = _Dev.CurrentJoystickState
				If (_lastDeviceStates.Count < _Devices.Count) Then
					_lastDeviceStates.Add(New JoystickState)
				End If
				CheckDeviceEvents(I, _DeviceState, _lastDeviceStates(I))
			Next
			Thread.Sleep(PollInterval)
		End While
		_IsStopped = True
	End Sub

	Private Sub CheckDeviceEvents(ByVal DeviceID As Integer, ByRef CurrentState As JoystickState, ByRef PreviousState As JoystickState)
		
		' Check the Axis
		CompareAxisState(DeviceID, "X", CurrentState.X, PreviousState.X)
		CompareAxisState(DeviceID, "Y", CurrentState.Y, PreviousState.Y)
		CompareAxisState(DeviceID, "Z", CurrentState.Z, PreviousState.Z)
		CompareAxisState(DeviceID, "RX", CurrentState.Rx, PreviousState.Rx)
		CompareAxisState(DeviceID, "RY", CurrentState.Ry, PreviousState.Ry)
		CompareAxisState(DeviceID, "RZ", CurrentState.Rz, PreviousState.Rz)

		' Check sliders but treat them as Axis
		Dim CurrentSliders As Integer() = CurrentState.GetSlider
		Dim PreviousSliders As Integer() = PreviousState.GetSlider
		For I As Integer = 0 to CurrentSliders.Length - 1
			CompareAxisState(DeviceID, "Slider" & I, CurrentSliders(I), PreviousSliders(I))
		Next

		' Check POV and treat them as buttons
		Dim CurrentPOV As Integer() = CurrentState.GetPointOfView
		Dim PreviousPOV As Integer() = PreviousState.GetPointOfView
		For I As Integer = 0 to _Devices(DeviceID).Caps.NumberPointOfViews - 1
			Dim CPOVButtons As Byte() = POVToButtons(CurrentPOV(I))
			Dim PPOVButtons As Byte() = POVToButtons(PreviousPOV(I))
			For J As Integer = 0 To 3
				CompareButtonState(DeviceID, "Pov" & I & ":" & J, CPOVButtons(J), PPOVButtons(J))
			Next
		Next

		' Check the Buttons
		Dim CurrentButtons As Byte() = CurrentState.GetButtons
		Dim PreviousButtons As Byte() = PreviousState.GetButtons
		For I As Integer = 0 to CurrentButtons.Length - 1
			CompareButtonState(DeviceID, "B" & I, CurrentButtons(I), PreviousButtons(I))
		Next
		
		' Set the previous state to current.
		PreviousState = CurrentState
		
	End Sub

	Private Function POVToButtons(ByVal PovState As Integer) As Byte()
		Dim Ret(3) As Byte
		Select Case PovState
			Case 0
				Ret(0) = 128
				Return Ret
			Case 4500
				Ret(0) = 128
				Ret(1) = 128
				Return Ret
			Case 9000
				Ret(1) = 128
				Return Ret
			Case 13500
				Ret(1) = 128
				Ret(2) = 128
				Return Ret
			Case 18000
				Ret(2) = 128
				Return Ret
			Case 22500
				Ret(2) = 128
				Ret(3) = 128
				Return Ret
			Case 27000
				Ret(3) = 128
				Return Ret
			Case 31500
				Ret(3) = 128
				Ret(0) = 128
				Return Ret
			Case Else
				Return Ret
		End Select
	End Function

	Private Sub CompareAxisState(ByVal DeviceID As Integer, ByVal Name As String, ByVal NewVal As Integer, ByVal OldVal As Integer)
		Dim Delta As Integer = NewVal - OldVal
		If (Math.Abs(Delta) > AxisFilter) Then
			Dim DeviceName As String = _Devices(DeviceID).DeviceInformation.ProductName
			RaiseEvent DeviceAxisChanged(DeviceName, DeviceID, Name, NewVal, Delta)
		End If
	End Sub

	Private Sub CompareButtonState(ByVal DeviceID As Integer, ByVal Name As String, ByVal NewVal As Integer, ByVal OldVal As Integer)
		Dim Delta As Integer = NewVal - OldVal
		If (Math.Abs(Delta) > 0) Then
			Dim DeviceName As String = _Devices(DeviceID).DeviceInformation.ProductName
			RaiseEvent DeviceButtonChanged(DeviceName, DeviceID, Name, NewVal, Delta)
		End If
	End Sub

End Class
