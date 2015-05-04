Public Class frmVigilanceSystem

	Private Sub tmUpdateInfo_Tick( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles tmUpdateInfo.Tick
		Try
			lstStatus.Items.Clear
			lstStatus.Items.Add("AlertTimer: " & IHandler.Vigilance.AlertTimer.ElapsedMilliseconds)
			lstStatus.Items.Add("TriggerTimer: " & IHandler.Vigilance.TriggerTimer.ElapsedMilliseconds)
			lstStatus.Items.Add("MinDelay: " & IHandler.Vigilance.MinDelay)
			lstStatus.Items.Add("MaxDelay: " & IHandler.Vigilance.MaxDelay)
			lstStatus.Items.Add("EarlyTriggerChance: " & IHandler.Vigilance.EarlyTriggerChance)
			lstStatus.Items.Add("EarlyTriggerOffset: " & IHandler.Vigilance.EarlyTriggerOffset)
			lstStatus.Items.Add("UseVigilanceSystem: " & IHandler.Vigilance.UseVigilanceSystem)
			lstStatus.Items.Add("LastTrainSpeed: " & IHandler.Vigilance.LastTrainSpeed)
			lstStatus.Items.Add("MinimumTrainSpeed: " & IHandler.Vigilance.MinimumTrainSpeed)
			lstStatus.Items.Add("TrainSpeedOffset: " & IHandler.Vigilance.TrainSpeedOffset)
			lstStatus.Items.Add("TrainSpeedOffsetFactor: " & IHandler.Vigilance.TrainSpeedOffsetFactor)
			lstStatus.Items.Add("TriggerTimeout: " & IHandler.Vigilance.TriggerTimeout)
			lstStatus.Items.Add("Button: " & IHandler.Vigilance.ButtonState.ToString)
			lstStatus.Items.Add("Required: " & IHandler.Vigilance.ActionRequired.ToString)
			lstStatus.Items.Add("State: " & IHandler.Vigilance.State.ToString)
		Catch Ex As Exception
			Debug.Print("Something went wrong in the VigilanceSystem form: " & Ex.Message)
		End Try
	End Sub

 	Private Sub frmVigilanceSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		Me.Visible = False
		e.Cancel = True
	End Sub


	Private Sub txtMinDelay_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles _
		txtMinDelay.KeyDown, _
		txtMaxDelay.KeyDown, _
		txtEarlyTriggerChance.KeyDown, _
		txtMinTrainSpeed.KeyDown, _
		txtTrainSpeedFactor.KeyDown, _
		txtTimeout.KeyDown
		
		If e.KeyCode = Keys.Return Then
			Dim MinDelay, MaxDelay, Timeout As Long
			Dim EarlyTriggerChance, MinTrainSpeed, TrainSpeedFactor As Single
			Try
				MinDelay = CLng(txtMinDelay.Text)
				MaxDelay = CLng(txtMaxDelay.Text)
				EarlyTriggerChance = CSng(txtEarlyTriggerChance.Text)
				MinTrainSpeed = CSng(txtMinTrainSpeed.Text)
				TrainSpeedFactor = CSng(txtTrainSpeedFactor.Text)
				Timeout = CLng(txtTimeout.Text)

				IHandler.Vigilance.MinDelay = MinDelay
				IHandler.Vigilance.MaxDelay = MaxDelay
				IHandler.Vigilance.EarlyTriggerChance = EarlyTriggerChance / 100
				IHandler.Vigilance.MinimumTrainSpeed = MinTrainSpeed
				IHandler.Vigilance.TrainSpeedOffsetFactor = TrainSpeedFactor
				IHandler.Vigilance.TriggerTimeout = Timeout

			Catch ex As Exception
				MsgBox("Invalid values... Check to see that there are no letters or decimals where there are milliseconds", MsgBoxStyle.Critical, "ERROR!")
			End Try
		End If

	End Sub

	Private Sub frmVigilanceSystem_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
		txtMinDelay.Text = IHandler.Vigilance.MinDelay.ToString
		txtMaxDelay.Text = IHandler.Vigilance.MaxDelay.ToString
		txtEarlyTriggerChance.Text = (IHandler.Vigilance.EarlyTriggerChance * 100).ToString
		txtMinTrainSpeed.Text = IHandler.Vigilance.MinimumTrainSpeed.ToString
		txtTrainSpeedFactor.Text = IHandler.Vigilance.TrainSpeedOffsetFactor.ToString
		txtTimeout.Text = IHandler.Vigilance.TriggerTimeout.ToString
		chkUseVigilanceSystem.Checked = IHandler.Vigilance.UseVigilanceSystem
	End Sub

Private Sub chkUseVigilanceSystem_CheckedChanged( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles chkUseVigilanceSystem.CheckedChanged
		IHandler.Vigilance.UseVigilanceSystem = chkUseVigilanceSystem.Checked
End Sub
End Class