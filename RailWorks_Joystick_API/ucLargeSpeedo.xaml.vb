Imports System.Windows

Public Class ucLargeSpeedo
	
	Private _SpeedometerIndex As Integer = 0
	Public Property SpeedometerIndex As Integer
	Get
		Return _SpeedometerIndex
	End Get
	Set(ByVal Value As Integer)
		_SpeedometerIndex = Value
		Dim S As SpeedometerData = Speedos(_SpeedometerIndex)
		SpeedoBG.Source = S.Background
		SpeedoIndicator.Source = S.Indicator
		SpeedoGlass.Source = S.Glass
	End Set
	End Property

	Public WriteOnly Property Speed As Single
		Set(ByVal SpeedValue As Single)
			Dim S As SpeedometerData = Speedos(_SpeedometerIndex)
			If S.IsKPH Then
				SpeedValue *= CSng(1.609344)
			End If
			Dim Range As Single = S.EndAngle - S.StartAngle
			Dim Angle As Single = CSng((Clamp(SpeedValue, 0, S.MaxSpeed) * (Range / S.MaxSpeed)) + S.StartAngle)
			Dim RT As New Windows.Media.RotateTransform(Angle)
			SpeedoIndicator.RenderTransform = RT
		End Set
	End Property

	Private Structure SpeedometerData
		Public Background As Media.Imaging.BitmapImage
		Public Indicator As Media.Imaging.BitmapImage
		Public Glass As Media.Imaging.BitmapImage
		Public MaxSpeed As Single
		Public StartAngle As Single
		Public EndAngle As Single
		Public IsKPH As Boolean

		Public Sub New(ByVal BackgroundPath As String, ByVal IndicatorPath As String, ByVal GlassPath As String, ByVal MaxSpeedValue As Single, ByVal StartAngleValue As Single, ByVal EndAngleValue As Single, ByVal IsKPHValue As Boolean)
			Background = New Media.Imaging.BitmapImage(New Uri(BackgroundPath, UriKind.Relative))
			Indicator = New Media.Imaging.BitmapImage(New Uri(IndicatorPath, UriKind.Relative))
			Glass = New Media.Imaging.BitmapImage(New Uri(GlassPath, UriKind.Relative))
			MaxSpeed = MaxSpeedValue
			StartAngle = StartAngleValue
			EndAngle = EndAngleValue
			IsKPH = IsKPHValue
		End Sub
	End Structure
	Private Speedos As New List(Of SpeedometerData)

	Private Sub SpeedoGlass_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles SpeedoGlass.MouseDown
		If e.LeftButton = Input.MouseButtonState.Pressed Then
			If SpeedometerIndex = Speedos.Count - 1 Then
				SpeedometerIndex = 0
			Else
				SpeedometerIndex += 1
			End If
		End If
	End Sub

	Private Sub ucLargeSpeedo_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
		
		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBgLargeDay.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicatorLarge.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlassLarge120.png", _
			120, 0, 270, False))

		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBgLargeOrange.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicatorLarge.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlassLarge120.png", _
			120, 0, 270, False))

		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBgLargeBlue.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicatorLargeBlue.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlassLarge120.png", _
			120, 0, 270, False))
		
		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBgLarge200Day.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicatorLarge.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlassLarge200.png", _
			200, 0, 315, False))

		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBgLarge200KPHDay.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicatorLarge.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlassLarge200.png", _
			200, 0, 315, True))

		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBgLarge300KPHDay.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicatorLarge.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlassLarge200.png", _
			300, 0, 315, True))

		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBgLarge360KPHDay.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicatorLarge.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlassLarge200.png", _
			360, 0, 315, True))

	End Sub
End Class
