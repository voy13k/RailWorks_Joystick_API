Imports Microsoft.DirectX
Imports Microsoft.DirectX.DirectInput
Imports System.Reflection.Assembly
Imports System.Diagnostics.FileVersionInfo
Imports System.IO


Public Class frmMain

 	Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		JoyDevices.StopPolling
		IHandler.StopAPI
		ControlManager.Running = False
		If Not (JoyDevices.IsStopped And IHandler.IsStopped) Then
			Closer.Enabled = True
			e.Cancel = True
		End If
		ControlManager.Close
	End Sub
	Private Sub frmMain_Load( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles MyBase.Load
		
		JoyDevices = New Joystick(ME)
		If Not JoyDevices.DeviceFound Then
			MsgBox("No devices found!", MsgBoxStyle.Critical, "Error")
		End If

		DeviceNames = JoyDevices.GetDeviceNames
		DevStatus = txtDevStatus
		DevStatus.Text = String.Join(vbCrLf, DeviceNames.ToArray)
		Me.Show
		Application.DoEvents
		IHandler = New InputHandler(DeviceNames)
		Threading.Thread.Sleep(100)
		JoyDevices.StartPolling
		IHandler.StartAPI

		Dim VI As FileVersionInfo = GetVersionInfo(GetExecutingAssembly.Location)
		Me.Text &= "   (VERSION: " & VI.ProductMajorPart.ToString & "." & VI.ProductMinorPart.ToString & " BUILD: " & VI.ProductBuildPart.ToString & ")"

		PopulateSettingsList

	End Sub

	Private Sub PopulateSettingsList()
		Dim DI As New DirectoryInfo(Application.StartupPath)
		Dim Files As FileInfo() = DI.GetFiles("*.CMAP")
		lstSettings.Items.Clear
		For Each FI In Files
			lstSettings.Items.Add(FI.Name)
		Next
	End Sub

	Private Sub ShowControlsBtn_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles ShowControlsBtn.Click
		ControlsWindow.Show
	End Sub

	Private Sub btnShowLargeSpeedo_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnShowLargeSpeedo.Click
		SpeedoWindow.Show
	End Sub

	Private Sub btnVigilanceSettings_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnVigilanceSettings.Click
		frmVigilanceSystem.Show
	End Sub

	Private Sub Closer_Tick( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles Closer.Tick
		Me.Close
	End Sub

	Private Sub btnNew_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnNew.Click
		Dim FileName As String = InputBox("!!! THIS WILL CLEAR ANY CHANGES MADE SINCE LAST SAVE !!!" & vbCrLf & vbCrLf & "Enter filename:", "Create new settings")
		If FileName = "" Then Exit Sub
		FileName &= ".CMap"
		ControlsWindow.Visible = False
		ControlManager.Visible = False
		IHandler.LoadBlank
		IHandler.SaveMaps(FileName)
		PopulateSettingsList
	End Sub

	Private Sub btnLoadSelected_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnLoadSelected.Click
		If lstSettings.SelectedIndex = -1 Then Exit Sub
		ControlsWindow.Visible = False
		ControlManager.Visible = False
		IHandler.LoadMaps(lstSettings.Items(lstSettings.SelectedIndex).ToString)
		PopulateSettingsList
	End Sub

	Private Sub btnSaveSelected_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnSaveSelected.Click
		If lstSettings.SelectedIndex = -1 Then Exit Sub
		IHandler.SaveMaps(lstSettings.Items(lstSettings.SelectedIndex).ToString)
		PopulateSettingsList
	End Sub

	Private Sub btnDuplicateSelected_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnDuplicateSelected.Click
		If lstSettings.SelectedIndex = -1 Then Exit Sub
		Dim FileName As String = InputBox("This will copy the selected file, did you save first?" & vbCrLf & vbCrLf & "Enter filename:", "Create new settings")
		If FileName = "" Then Exit Sub
		FileName &= ".CMap"
		Try
			File.Copy(lstSettings.Items(lstSettings.SelectedIndex).ToString, FileName)
		Catch ex As Exception
			MsgBox("The file could not be duplicated/copied!")
		End Try
		PopulateSettingsList
	End Sub

	Private Sub btnRemoveSelected_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnRemoveSelected.Click
		If lstSettings.SelectedIndex = -1 Then Exit Sub
		MsgBox("This will delete the selected file! Are you sure?", MsgBoxStyle.YesNo, "Are you sure?")
		File.Delete(lstSettings.Items(lstSettings.SelectedIndex).ToString)
		PopulateSettingsList
	End Sub

	Private Sub btnPrevDevice_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnPrevDevice.Click
		DevStatusID = Clamp(DevStatusID - 1, 0, DeviceNames.Count - 1)
		lblCurrentDevice.Text = "Listening to: " & DeviceNames(DevStatusID)
	End Sub

	Private Sub btnNextDevice_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnNextDevice.Click
		DevStatusID = Clamp(DevStatusID + 1, 0, DeviceNames.Count - 1)
		lblCurrentDevice.Text = "Listening to: " & DeviceNames(DevStatusID)
	End Sub

	Private Sub btnAllDevice_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnAllDevice.Click
		DevStatusID = -1
		lblCurrentDevice.Text = "Listening to: ALL Devices..."
	End Sub

	Private Sub btnShiftDown_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnShiftDown.Click
		IHandler.ShiftMaps(-1)
	End Sub
	Private Sub btnShiftUp_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnShiftUp.Click
		IHandler.ShiftMaps(1)
	End Sub

End Class
