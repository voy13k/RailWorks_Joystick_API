Imports System.Threading

Public Class VigilanceSystem
	
	Public AlertTimer As Stopwatch
	Public TriggerTimer As Stopwatch

	Public MinDelay As Long = 5000
	Public MaxDelay As Long = 10000
	Public EarlyTriggerChance As Single = 0.5
	Public EarlyTriggerOffset As Long = 0

	Public UseVigilanceSystem As Boolean = False
	Public LastTrainSpeed As Single
	Public MinimumTrainSpeed As Single = 0.5
	Public TrainSpeedOffset As Long = 0
	Public TrainSpeedOffsetFactor As Single = 0.1

	Public ButtonState As Actions = Actions.Release

	Public TriggerTimeout As Long = 5000

	Public InGame As Boolean = False

	Public Enum Actions
		Press
		Release
	End Enum

	Public Enum VigilanceState
		Vigilant
		Alert
		Triggered
	End Enum

	Public State As VigilanceState = VigilanceState.Vigilant

	Public ActionRequired As Actions = Actions.Release

	Private VigilanceThread As Thread

	Public Sub New()
		Start
	End Sub

	Public Sub Start()
		Running = True
		AlertTimer = New Stopwatch
		TriggerTimer = New Stopwatch
		VigilanceThread = New Thread(AddressOf VigilanceThreadMain)
		VigilanceThread.IsBackground = True
		VigilanceThread.Start
	End Sub

	Public Running As Boolean = False

	Public Sub VigilanceThreadMain()
		
		Randomize
		Dim EarlyTrigger As Boolean = False
		While Running
            Thread.Sleep(100)
            If Not UseVigilanceSystem Then Continue While
			If AlertTimer Is Nothing Or TriggerTimer Is Nothing Then 
				AlertTimer = New Stopwatch
				TriggerTimer = New Stopwatch
			End If
			If Not InGame Then 
				Reset
				Continue While
			End If
			If Not AlertTimer.IsRunning Then
				AlertTimer.Reset
				AlertTimer.Start
				If Rnd() < EarlyTriggerChance Then
					EarlyTriggerOffset = CInt(Rnd() * (MaxDelay - MinDelay))
				Else
					EarlyTriggerOffset = 0
				End If
			End If
			
			Dim MS As Long = AlertTimer.ElapsedMilliseconds
			
			If LastTrainSpeed < MinimumTrainSpeed And Not State = VigilanceState.Triggered Then
				Reset
				Continue While
			End If
			If MS < MinDelay And Not TriggerTimer.IsRunning Then 
				ActionRequired = Actions.Press
			End If
			TrainSpeedOffset = CLng((LastTrainSpeed * TrainSpeedOffsetFactor) * 1000)
			If MS >= MaxDelay - EarlyTriggerOffset - TrainSpeedOffset And Not TriggerTimer.IsRunning Then
				ActionRequired = Actions.Release
				TriggerTimer.Reset
				TriggerTimer.Start
			End If
			If ButtonState = ActionRequired Then
				If State = VigilanceState.Triggered And Not LastTrainSpeed < MinimumTrainSpeed Then 
				Else
					If TriggerTimer.IsRunning Then Reset
					ChangeState(VigilanceState.Vigilant)
				End If
			Else
				If Not State = VigilanceState.Triggered Then ChangeState(VigilanceState.Alert)
				If Not TriggerTimer.IsRunning Then
					TriggerTimer.Reset
					TriggerTimer.Start
				End If
			End If
			If TriggerTimer.IsRunning And TriggerTimer.ElapsedMilliseconds > TriggerTimeout Then
				ChangeState(VigilanceState.Triggered)
			End If
        End While

	End Sub
	
	Public Sub ChangeState(ByVal NewState As VigilanceState)
		If State = NewState Then Exit Sub
		State = NewState
		'Debug.Print("Changed state to " & State.ToString)
		Select Case State
			Case VigilanceState.Vigilant
				My.Computer.Audio.Stop
			Case VigilanceState.Alert
                My.Computer.Audio.Play("VigilanceAlert.mp3", AudioPlayMode.BackgroundLoop)
			Case VigilanceState.Triggered
                My.Computer.Audio.Play("VigilanceTriggered.mp3", AudioPlayMode.BackgroundLoop)
		End Select
	End Sub

	Public Sub Reset()
		AlertTimer.Stop
		AlertTimer.Reset
		TriggerTimer.Stop
		TriggerTimer.Reset
		ActionRequired = Actions.Release
		My.Computer.Audio.Stop
		ChangeState(VigilanceState.Vigilant)
	End Sub

End Class
