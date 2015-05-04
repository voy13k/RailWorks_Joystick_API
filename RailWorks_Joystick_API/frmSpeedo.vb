Public Class frmSpeedo
	
	Public Sub SetSpeed(ByVal Speed As Single)
		ControlsSpeedo.Speed = Speed
	End Sub

	Private Sub frmSpeedo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		Me.Visible = False
		e.Cancel = True
	End Sub
End Class