Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Runtime.Serialization


Public Class frmManageControls

	Private Delegate Sub LoadControlMapDelegate()	
	Private Delegate Sub UpdateCurrentDelegate()	
	Public APIMapIndex As Integer = -1
	Public InputMapIndex As Integer = -1
	Public Description As String
	Public Running As Boolean = True
	Public AssignMode As Boolean
	Private InLoadControl As Boolean

 	Private Sub frmManageControls_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		IHandler.StopAssigning
		If (Running) Then
			Me.Visible = False
			e.Cancel = True
		End If
	End Sub

 	Private Sub frmManageControls_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, cbMode.KeyDown, cbTreatAs.KeyDown, cbNotchPresets.KeyDown, cbRangePresets.KeyDown
		If AssignMode And e.KeyCode = Keys.Escape then
			AssignMode = False
			IHandler.StopAssigning
			LoadControlMap()
		End If
	End Sub

	Public Sub LoadControlMap()
		
		If Not Running Then Exit Sub
		InLoadControl = True
		If (lbCommandDescription.InvokeRequired) Then
			Dim Del As New LoadControlMapDelegate(AddressOf LoadControlMap)
			lbCommandDescription.Invoke(Del)
			Exit Sub
		End If
		lbCommandDescription.Text = Description.Replace("&", "&&")

		cbMode.SelectedIndex = 0
		cbTreatAs.SelectedIndex = 0
		btnReload.Enabled = False

		Dim CMap As Map
		Dim IMap As InputMap
		If (APIMapIndex >= 0) Then
			CMap = IHandler.Mappings(APIMapIndex)
			If (InputMapIndex >= 0) Then
				IMap = CMap.InputMaps(InputMapIndex)
				If (IMap.TreatAs = InputMap.ControlType.Assigning) Then IMap.TreatAs = InputMap.ControlType.NotAssigned
				CMap.InputMaps(InputMapIndex) = IMap
				cbTreatAs.Enabled = True
				cbMode.Enabled = True
				If IMap.MapMode = InputMap.ControlMode.NotchedSlider Or IMap.MapMode = InputMap.ControlMode.NotchedSliderRanges Or IMap.MapMode = InputMap.ControlMode.NotchedSliderTargets Or IMap.MapMode = InputMap.ControlMode.UpDownLatch or IMap.MapMode = InputMap.ControlMode.UpDownRelative Then
					gbNotches.Visible = True
					cbNotchPresets.SelectedIndex = 0
					Dim txt As String = ""
					For I As Integer = 0 To IMap.Notches.Count - 1
						txt &= (IMap.Notches(I) * 100).ToString
						If I < IMap.Notches.Count - 1 Then txt &= ", "
					Next
					txtNotches.Text = txt
				Else
					gbNotches.Visible = False
				End If
				If IMap.MapMode = InputMap.ControlMode.NotchedSliderRanges Or IMap.MapMode = InputMap.ControlMode.NotchedSliderTargets Or IMap.MapMode = InputMap.ControlMode.UpDownLatch or IMap.MapMode = InputMap.ControlMode.UpDownRelative Or IMap.MapMode = InputMap.ControlMode.Toggle Then
					If IMap.MapMode = InputMap.ControlMode.Toggle Then
						gbNotchRanges.Top = 416
						gbNotchRanges.Text = "Toggles..."
					ElseIf IMap.MapMode = InputMap.ControlMode.NotchedSliderTargets Then
						gbNotchRanges.Top = 472
						gbNotchRanges.Text = "Targets..."
					Else
						gbNotchRanges.Top = 472
						gbNotchRanges.Text = "Ranges..."
					End If
					gbNotchRanges.Visible = True
					cbRangePresets.SelectedIndex = 0
					Dim txt As String = ""
					For I As Integer = 0 To IMap.NotchRanges.Count - 1
						txt &= (IMap.NotchRanges(I) * 100).ToString
						If I < IMap.NotchRanges.Count - 1 Then txt &= ", "
					Next
					txtRanges.Text = txt
				Else
					gbNotchRanges.Visible = False
				End If
			Else
				cbTreatAs.Enabled = False
				cbMode.Enabled = False
			End If
			IHandler.Mappings(APIMapIndex) = CMap
		Else
			Debug.Print("APIMapIndex sent to frmManageControls.LoadControlMap was invalid: " & APIMapIndex.ToString)
			Exit Sub
		End If
		
		lswInputList.Items.Clear
		For Each IM As InputMap In CMap.InputMaps
			lswInputList.Items.Add(IM.ToString)
			If Not (IM.TreatAs = InputMap.ControlType.NotAssigned) Then
				lswInputList.Items(lswInputList.Items.Count - 1).ForeColor = Color.Green
			End If
		Next
		lswInputList.SelectedIndices.Clear
		lswInputList.SelectedItems.Clear
		If lswInputList.Items.Count = 0 Then 
			InputMapIndex = -1
		End If

		lbCommandDescription.Text = Description.Replace("&", "&&")
		LoadInput
		InLoadControl = False
	End Sub
	
	Public Sub LoadInput()
		Dim CMap As Map
		Dim IMap As InputMap
		If (InputMapIndex >= 0) Then
			CMap = IHandler.Mappings(APIMapIndex)
			IMap = CMap.InputMaps(InputMapIndex)
			lswInputList.Items(InputMapIndex).Selected = True
			cbTreatAs.Items.Clear
			Dim Names() As String = [Enum].GetNames(GetType(InputMap.ControlType))
			ReDim Preserve Names(2)
			cbTreatAs.Items.AddRange(Names)
			cbTreatAs.SelectedIndex = IMap.TreatAs
			cbMode.Items.Clear
			cbMode.Items.AddRange([Enum].GetNames(GetType(InputMap.ControlMode)))
			cbMode.SelectedIndex = IMap.MapMode
			chkIsNegativePositiveRange.Checked = IMap.IsNegativePositiveRange
			UpdateSubscription
		End If
	End Sub

	Public Sub UpdateSubscription()
		If pbInputValue.InvokeRequired Then
			Dim Del As New UpdateCurrentDelegate(AddressOf UpdateSubscription)
			If pbInputValue.IsHandleCreated And Running Then pbInputValue.Invoke(Del) ' Damn threading... TODO: Fix crash that occurs when the device is spazzing sending messages after form close.
			Exit Sub
		End If
		pbInputValue.Refresh
		pbOutputValue.Refresh
		pbAPIOutput.Refresh
	End Sub

	Private Sub pbInputValue_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pbInputValue.Paint, pbOutputValue.Paint, pbAPIOutput.Paint
		Dim CMap As Map
		Dim IMap As InputMap
		Dim Percent As Single
		If (InputMapIndex >= 0) Then
			CMap = IHandler.Mappings(APIMapIndex)
			IMap = CMap.InputMaps(InputMapIndex)
			If CType(sender, PictureBox).Name = "pbInputValue" Then
				Percent = IMap.GetPercent
			ElseIf CType(sender, PictureBox).Name = "pbOutputValue" Then
				Percent = IMap.GetOutputPercent
			Else
				Percent = CMap.GetOutputPercent
			End If
		End If
		Dim CurVal As Double
		CurVal = 530 * Percent
		Dim G As Graphics = e.Graphics
		Dim Rect As New Rectangle(0, 0, 55, 530)
		Dim RectCurrent As New Rectangle(0, 530 - CInt(CurVal), 55, 530)
		Dim lgBrushFill As New LinearGradientBrush(Rect, Color.Red, Color.LimeGreen, 90)
		Dim lgBrushFillBg As New LinearGradientBrush(Rect, Color.Gray, Color.Black, 90)
		Dim lgBrushFrame As New LinearGradientBrush(Rect, Color.Blue, Color.Black, 90)
		Dim pnFrame As New Pen(Brushes.Black, 4)
		Dim pnPercentSmall As New Pen(Brushes.White, 1)
		Dim pnPercentLarge As New Pen(Brushes.White, 2)
		G.FillRectangle(lgBrushFillBg, Rect)
		G.FillRectangle(lgBrushFill, RectCurrent)
		
		G.DrawLine(pnFrame, New Point(0, 530 - CInt(CurVal)), New Point(55, 530 - CInt(CurVal)))

		For I As Integer = 530 To 0 Step -53
			G.DrawLine(pnPercentSmall, New Point(50, 530 - I + 26), New Point(55, 530 - I + 26))
			G.DrawLine(pnPercentLarge, New Point(45, 530 - I), New Point(55, 530 - I))
		Next

		G.DrawRectangle(pnFrame, Rect)
		G.DrawString((Percent * 100).ToString("0.#") & "%", New Font("Arial", 11), Brushes.White, New Point(10, 10))
	End Sub

	Private Sub cbMode_SelectedIndexChanged( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles cbMode.SelectedIndexChanged
		Dim CMap As Map
		Dim IMap As InputMap
		If ((Not InLoadControl) And InputMapIndex >= 0) Then
				CMap = IHandler.Mappings(APIMapIndex)
				IMap = CMap.InputMaps(InputMapIndex)
				IMap.MapMode = CType(cbMode.SelectedIndex, InputMap.ControlMode)
				CMap.InputMaps(InputMapIndex) = IMap
				IHandler.Mappings(APIMapIndex) = CMap
				LoadControlMap()
		End If
	End Sub

	Private Sub cbTreatAs_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbTreatAs.SelectedIndexChanged
		Dim CMap As Map
		Dim IMap As InputMap
		If ((Not InLoadControl) And InputMapIndex >= 0) Then
				CMap = IHandler.Mappings(APIMapIndex)
				IMap = CMap.InputMaps(InputMapIndex)
				IMap.TreatAs = CType(cbTreatAs.SelectedIndex, InputMap.ControlType)
				If (Not IMap.TreatAs = InputMap.ControlType.NotAssigned) And IMap.DeviceID = -1 And IMap.MapName = "" Then
					btnAssignSelected_Click(btnAssignSelected, New System.EventArgs)
					Exit Sub
				End If
				If IMap.TreatAs = InputMap.ControlType.NotAssigned Then
					IMap.MapName = ""
					IMap.DeviceID = -1
				End If
				CMap.InputMaps(InputMapIndex) = IMap
				IHandler.Mappings(APIMapIndex) = CMap
				LoadControlMap()
		End If
	End Sub

 	Private Sub lswInputList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lswInputList.Click
		If ((Not InLoadControl) And lswInputList.SelectedIndices.Count > 0) Then
			InputMapIndex = lswInputList.SelectedIndices(0)
			LoadControlMap
		End If
	End Sub

	Private Sub btnAddInput_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnAddInput.Click
		Dim IMap As New InputMap(InputMap.ControlType.NotAssigned)
		IHandler.Mappings(APIMapIndex).AddInput(IMap)
		LoadControlMap
	End Sub

	Private Sub btnRemoveInput_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnRemoveInput.Click
		If (InputMapIndex >= 0) Then IHandler.Mappings(APIMapIndex).InputMaps.RemoveAt(InputMapIndex)
		InputMapIndex = -1
		LoadControlMap
	End Sub

	Private Sub btnImport_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnImport.Click
			Dim FStream As FileStream
			Dim Dlg As New Windows.Forms.OpenFileDialog
			Dim IMap As InputMap
			Dlg.InitialDirectory = Application.StartupPath
			Dlg.DefaultExt = ".IMAP"
			Dlg.Filter = "Input map files (*.IMAP)|*.IMAP"
			Dlg.FilterIndex = 0
			Dlg.RestoreDirectory = True
			Dlg.Title = "Import input map from..."
			Dlg.AddExtension = True
			If (Dlg.ShowDialog = Windows.Forms.DialogResult.OK) Then
				Try
					FStream = New FileStream(Dlg.FileName, FileMode.Open)
					Dim BFormatter As New Formatters.Binary.BinaryFormatter
					IMap = CType(BFormatter.Deserialize(FStream), InputMap)
					Select Case MessageBox.Show("Would you like to import the assigned device in this input map as well?" & vbCrLf & "If you do, remember that the assigned device and axis/button might not exist/be connected to your computer...", "Import assigned Device + Axis/Button?", MessageBoxButtons.YesNoCancel)
						Case Windows.Forms.DialogResult.Yes
						Case Windows.Forms.DialogResult.No
							IMap.DeviceID = -1
							IMap.MapName = ""
							IMap.TreatAs = InputMap.ControlType.NotAssigned
						Case Windows.Forms.DialogResult.Cancel
							Exit Sub
					End Select
					IHandler.Mappings(APIMapIndex).AddInput(IMap)
					LoadControlMap
				Catch ex As Exception
					MsgBox("The input map could not be loaded from " & Dlg.FileName & "!")
				Finally
					If FStream IsNot Nothing Then FStream.Close
				End Try
			End If
	End Sub

	Private Sub btnExport_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnExport.Click
		Dim IMap As InputMap
		If (InputMapIndex >= 0) Then
			IMap = IHandler.Mappings(APIMapIndex).InputMaps(InputMapIndex)
			Dim Dlg As New Windows.Forms.SaveFileDialog
			Dlg.InitialDirectory = Application.StartupPath
			Dlg.DefaultExt = ".IMAP"
			Dlg.Filter = "Input map files (*.IMAP)|*.IMAP"
			Dlg.FilterIndex = 0
			Dlg.OverwritePrompt = True
			Dlg.RestoreDirectory = True
			Dlg.AddExtension = True
			Dlg.Title = "Export input map to..."
			If (Dlg.ShowDialog = Windows.Forms.DialogResult.OK) Then
				Dim FStream As New FileStream(Dlg.FileName, FileMode.OpenOrCreate)
				Dim BFormatter As New Formatters.Binary.BinaryFormatter
				Try
					BFormatter.Serialize(FStream, IMap)
				Catch ex As Exception
					MsgBox("The input map could not be saved to " & Dlg.FileName & "!")
				Finally
					FStream.Close
				End Try
			End If
		End If
	End Sub

	Private Sub btnAssignSelected_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnAssignSelected.Click
		Dim CMap As Map
		Dim IMap As InputMap
		If (InputMapIndex >= 0) Then
			AssignMode = True
			CMap = IHandler.Mappings(APIMapIndex)
			IMap = CMap.InputMaps(InputMapIndex)
			IMap.TreatAs = InputMap.ControlType.Assigning
			lswInputList.Items(InputMapIndex).Text = IMap.ToString
			CMap.InputMaps(InputMapIndex) = IMap
			IHandler.Mappings(APIMapIndex) = CMap
		End If
	End Sub

	Private Sub cbNotchPresets_SelectedIndexChanged( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles cbNotchPresets.SelectedIndexChanged, cbRangePresets.SelectedIndexChanged
		Dim Notches As Integer
		If (CType(sender, ComboBox).Name = "cbNotchPresets") Then
			Notches = cbNotchPresets.SelectedIndex
		Else
			Notches = cbRangePresets.SelectedIndex
		End If
		Dim txt As String = "0, "
		If (Notches < 1) Then
			txt = ""
		Else
			Dim NotchSize As Single = CSng(100 / Notches)
			For I As Integer = 1 To Notches - 1
				txt &= Math.Round((NotchSize * I), 1).ToString & ", "
			Next
			txt &= "100"
		End If
		If (CType(sender, ComboBox).Name = "cbNotchPresets") Then
			txtNotches.Text = txt
		Else
			txtRanges.Text = txt
		End If
	End Sub

 	Private NotchNonNumberEntered As Boolean

	Private Sub txtNotches_TextChanged( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles txtNotches.TextChanged, txtRanges.TextChanged
		If Not (InLoadControl) Then
			Dim Notches As String()
			If (CType(sender, TextBox).Name = "txtNotches") Then
				Notches = txtNotches.Text.Replace(" ", "").Split(","c)
			Else
				Notches = txtRanges.Text.Replace(" ", "").Split(","c)
			End If
			Dim CMap As Map
			Dim IMap As InputMap
			If (InputMapIndex >= 0) Then
				CMap = IHandler.Mappings(APIMapIndex)
				IMap = CMap.InputMaps(InputMapIndex)
				If (CType(sender, TextBox).Name = "txtNotches") Then
					IMap.Notches.Clear
				Else
					IMap.NotchRanges.Clear
				End If
				Try
					For Each Notch As String In Notches
						If (Notch = "") Then Continue For
						If (CSng(Notch) > 0) Then
							Notch = (CSng(Notch) / 100).ToString
						End If
						If IsNumeric(Notch) Then
							If (CType(sender, TextBox).Name = "txtNotches") Then
								IMap.Notches.Add(CSng(Notch))
							Else
								IMap.NotchRanges.Add(CSng(Notch))
							End If
						End If
					Next
					CMap.InputMaps(InputMapIndex) = IMap
					IHandler.Mappings(APIMapIndex) = CMap
					btnReload.Enabled = True
				Catch ex As Exception

				End Try
			End If
		End If
	End Sub

	Private Sub btnReload_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnReload.Click
		LoadControlMap
	End Sub

	Private Sub chkIsNegativePositiveRange_CheckedChanged( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles chkIsNegativePositiveRange.CheckedChanged
			Dim CMap As Map
			Dim IMap As InputMap
			If (InputMapIndex >= 0) Then
				CMap = IHandler.Mappings(APIMapIndex)
				IMap = CMap.InputMaps(InputMapIndex)
				IMap.IsNegativePositiveRange = chkIsNegativePositiveRange.Checked
				CMap.InputMaps(InputMapIndex) = IMap
				IHandler.Mappings(APIMapIndex) = CMap
			End If
	End Sub
End Class