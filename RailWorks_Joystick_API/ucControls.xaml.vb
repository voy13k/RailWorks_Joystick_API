Imports System.Math
Imports System.Threading
Imports System.Windows

Public Class ucControls

	Public Structure AnalogControl
		Public Min As Integer
		Public Max As Integer
		Public Current As Integer
		Private Ctrl As Controls.StackPanel

		Public Sub New(ByRef ControlGrid As Controls.StackPanel, ByVal MinValue As Integer, ByVal MaxValue As Integer, ByVal CurrentValue As Integer)
			Ctrl = ControlGrid
			Min = MinValue
			Max = MaxValue
			SetAdjusted(CurrentValue)
		End Sub
		Public Function GetRange() As Integer
			Return Max - Min
		End Function
		Public Function GetAdjustedNeg() As Integer
			Return Abs(Min) + Current
		End Function
		Public Sub SetAdjustedNeg(ByVal Value As Integer)
			Current = Clamp(Min + Value, Min, Max)
			Update
		End Sub
		Public Function GetAdjusted() As Integer
			Return GetRange - GetAdjustedNeg
		End Function
		Public Sub SetAdjusted(ByVal Value As Integer)
			SetAdjustedNeg(GetRange - Value)
		End Sub
		Public Function GetPercentNeg() As Single
			Return CSng(GetAdjustedNeg / (Max - Min))
		End Function
		Public Sub SetPercentNeg(ByVal Value As Single)
			SetAdjustedNeg(CInt(Value * GetRange))
		End Sub
		Public Function GetPercent() As Single
			Return 1 - GetPercentNeg
		End Function
		Public Sub SetPercent(ByVal Value As Single)
			SetPercentNeg(1 - Value)
		End Sub
		Private Sub Update()
			Dim SizeAdjust As Double = CType(CType(Ctrl.Children(0), Controls.Grid).Children(1), Controls.Image).Height
			Dim Marg As New Thickness(0, Current - (SizeAdjust / 2), 0, 0)
			CType(CType(Ctrl.Children(0), Controls.Grid).Children(1), Controls.Image).Margin = Marg
			If Ctrl.Name = "GearControls" Then
				Dim Gear As Integer = CInt((GetPercent) * 4) + 1
				CType(CType(Ctrl.Children(0), Controls.Grid).Children(2), Controls.Label).Content = Gear.ToString
			Else
				CType(CType(Ctrl.Children(0), Controls.Grid).Children(2), Controls.Label).Content = CInt(GetPercent * 100).ToString & "%"
			End If
			
		End Sub
	End Structure

	Public Structure ButtonControl
		Public State As Integer
		Public Sub SetState(ByVal Value As Integer)
			If (StateImages Is Nothing) Then
				StateImages = New List(Of String)
				If (Ctrl.Name = "StarterActive" Or Ctrl.Name = "ManualCouplingActive" Or Ctrl.Name = "AWSActive" Or Ctrl.Name = "EBrakeActive" Or Ctrl.Name = "PBrakeActive" Or _
					Ctrl.Name = "DriverGuideActive" Or Ctrl.Name = "MapActive" Or Ctrl.Name = "PADAheadActive" Or Ctrl.Name = "PADBehindActive" Or _
					Ctrl.Name = "JunctionAheadActive" Or Ctrl.Name = "JunctionBehindActive" Or Ctrl.Name = "VigilanceActive")  Then
					StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/Btn" & Ctrl.Name.Replace("Active", "") & "Inactive.png")
				Else
					StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnBlank.png")
				End If
			End If
			State = Clamp(Value, 0, Max)
			State = Clamp(State, 0, StateImages.Count - 1)
			Update
		End Sub
		Public Max As Integer
		Private Ctrl As Controls.Image
		Public StateImages As List(Of String)

		Public Sub New(ByRef ControlImage As Controls.Image, ByVal StateValue As Integer, ByVal MaxValue As Integer)
			Ctrl = ControlImage
			MaxValue = Clamp(MaxValue, 0, MaxValue)
			Max = MaxValue
			SetState(StateValue)
		End Sub

		Public Sub Toggle()
			If Ctrl.Name = "AWSActive" Then
				If State > 0 Then
					SetState(1 + (1 XOR State - 1))
					Exit Sub
				End If
				Exit Sub
			End If
			If State >= Max Then
				SetState(0)
			Else
				SetState(State + 1)
			End If
		End Sub

		Public Sub Update()
			Dim ImgSource As New Media.Imaging.BitmapImage(New Uri(StateImages(State), UriKind.Relative))
			Ctrl.Source = ImgSource
		End Sub
	End Structure

	Public Throttle As AnalogControl
	Public TrainBrakes As AnalogControl
	Public LocomotiveBrakes As AnalogControl
	Public DynamicBrakes As AnalogControl
	Public Reverser As AnalogControl
	Public Gear As AnalogControl

	Public Wipers As ButtonControl
	Public Lights As ButtonControl
	Public Pantograph As ButtonControl
	Public LoadUnload As ButtonControl
	Public Sander As ButtonControl
	Public SmallEjector As ButtonControl
	Public Horn As ButtonControl
	Public Bell As ButtonControl
	Public CylinderCocks As ButtonControl
	
	Public HeadOutLeft As ButtonControl
	Public Cab As ButtonControl
	Public HeadOutRight As ButtonControl
	Public Trackside As ButtonControl
	Public Alternate As ButtonControl
	Public ExternalFront As ButtonControl
	Public ExternalRear As ButtonControl
	Public Passenger As ButtonControl
	Public NextCab As ButtonControl
	Public PreviousCab As ButtonControl
	Public Coupling As ButtonControl
	Public Yard As ButtonControl
	Public FreeRoam As ButtonControl
	Public ZoomIn As ButtonControl
	Public ZoomOut As ButtonControl

	Public Starter As ButtonControl
	Public ManualCoupling As ButtonControl
	Public AWS As ButtonControl
	Public EBrake As ButtonControl

	Public PBrake As ButtonControl
	Public DriverGuide As ButtonControl
	Public Map As ButtonControl
	Public PADAhead As ButtonControl
	Public PADBehind As ButtonControl
	Public JunctionAhead As ButtonControl
	Public JunctionBehind As ButtonControl

	Public Vigilance As ButtonControl

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
			If Speedos.Count = 0 Then Exit Property
			Dim S As SpeedometerData = Speedos(_SpeedometerIndex)
			If S.IsKPH Then
				SpeedValue *= CSng(1.609344)
			End If
			Dim Range As Single = S.EndAngle - S.StartAngle
			Dim Angle As Single = CSng((Clamp(SpeedValue, 0, S.MaxSpeed) * (Range / S.MaxSpeed)) + S.StartAngle)
			Dim RT As New Windows.Media.RotateTransform(Angle)
			SpeedoIndicator.RenderTransform = RT
		
			SpeedValue = Clamp(SpeedValue, 0, 999.9)
			Dim SpeedString() As Char = String.Format("{0,5:###.0}", Round(SpeedValue, 1)).ToCharArray
			Dim ImgSource As New Media.Imaging.BitmapImage
			Dim Num As String
			For I As Integer = 0 To SpeedString.Length - 1
				Num = SpeedString(I)
				If I = 2 And Num = " " Then Num = "0"
				If Num = " " Then Num = "Blank"
				If Num = "." Then Continue For
				ImgSource = New Media.Imaging.BitmapImage(New Uri("/RailWorks_Joystick_API;component/bin/Debug/Images/Digi" & Num & ".png", UriKind.Relative))
				CType(CType(DigitalSpeedo.Children(I), Controls.Grid).Children(0), Controls.Image).Source = ImgSource
			Next

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

	Private _CombinedThrottleBrakes As Boolean
	Public Property CombinedThrottleBrakes As Boolean
	Get
		Return _CombinedThrottleBrakes
	End Get
	Set(ByVal Value As Boolean)
		_CombinedThrottleBrakes = Value
		If _CombinedThrottleBrakes Then
			Dim ImgSource As New Media.Imaging.BitmapImage(New Uri("/RailWorks_Joystick_API;component/bin/Debug/Images/ThrottleBrakeHandle.png", UriKind.Relative))
			CType(CType(ThrottleControls.Children(0), Controls.Grid).Children(1), Controls.Image).Source = ImgSource
			CType(CType(ThrottleControls.Children(1), Controls.Grid).Children(1), Controls.TextBlock).Text = "Combined" & vbCrLf & "Throttle /" & vbCrLf & "Brakes"
		Else
			CType(CType(ThrottleControls.Children(1), Controls.Grid).Children(1), Controls.TextBlock).Text = "Throttle"
		End If
	End Set
	End Property

	Private Sub ucControls_Loaded(ByVal sender As System.Object , ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
		Throttle = New AnalogControl(ThrottleControls, -4, 86, 0)
		TrainBrakes = New AnalogControl(TrainBrakeControls, 0, 90, 0)
		LocomotiveBrakes = New AnalogControl(LocomotiveBrakeControls, 0, 90, 0)
		DynamicBrakes = New AnalogControl(DynamicBrakeControls, 0, 90, 0)
		Reverser = New AnalogControl(ReverserControls, 0, 90, 0)
		Gear = New AnalogControl(GearControls, 0, 80, 0)
		
		CombinedThrottleBrakes = False

		Wipers = New ButtonControl(WipersActive, 0, 1)
		Lights = New ButtonControl(LightsActive, 0, 2)
		Pantograph = New ButtonControl(PantographActive, 0, 1)
		LoadUnload = New ButtonControl(LoadUnloadActive, 0, 1)
		Sander = New ButtonControl(SanderActive, 0, 1)
		SmallEjector = New ButtonControl(SmallEjectorActive, 0, 1)
		Horn = New ButtonControl(HornActive, 0, 1)
		Bell = New ButtonControl(BellActive, 0, 1)
		CylinderCocks = New ButtonControl(CylinderCocksActive, 0, 1)

		Wipers.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnWipersActive.png")
		Lights.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnLightsActive1.png")
		Lights.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnLightsActive2.png")
		Pantograph.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnPantographActive.png")
		LoadUnload.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnLoadUnloadActive.png")
		Sander.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnSanderActive.png")
		SmallEjector.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnSmallEjectorActive.png")
		Horn.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnHornActive.png")
		Bell.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnBellActive.png")
		CylinderCocks.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnCylinderCocksActive.png")

		HeadOutLeft = New ButtonControl(HeadOutLeftActive, 0, 1)
		Cab = New ButtonControl(CabActive, 0, 1)
		HeadOutRight = New ButtonControl(HeadOutRightActive, 0, 1)
		Trackside = New ButtonControl(TracksideActive, 0, 1)
		Alternate = New ButtonControl(AlternateActive, 0, 1)
		ExternalFront = New ButtonControl(ExternalFrontActive, 0, 1)
		ExternalRear = New ButtonControl(ExternalRearActive, 0, 1)
		Passenger = New ButtonControl(PassengerActive, 0, 1)
		NextCab = New ButtonControl(NextCabActive, 0, 1)
		PreviousCab = New ButtonControl(PreviousCabActive, 0, 1)
		Coupling = New ButtonControl(CouplingActive, 0, 1)
		Yard = New ButtonControl(YardActive, 0, 1)
		FreeRoam = New ButtonControl(FreeRoamActive, 0, 1)
		ZoomIn = New ButtonControl(ZoomInActive, 0, 1)
		ZoomOut = New ButtonControl(ZoomOutActive, 0, 1)

		HeadOutLeft.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnHeadOutLeftActive.png")
		Cab.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnCabActive.png")
		HeadOutRight.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnHeadOutRightActive.png")
		Trackside.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnTracksideActive.png")
		Alternate.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnAlternateActive.png")
		ExternalFront.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnExternalFrontActive.png")
		ExternalRear.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnExternalRearActive.png")
		Passenger.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnPassengerActive.png")
		NextCab.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnNextCabActive.png")
		PreviousCab.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnPreviousCabActive.png")
		Coupling.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnCouplingActive.png")
		Yard.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnYardActive.png")
		FreeRoam.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnFreeRoamActive.png")
		ZoomIn.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnZoomInActive.png")
		ZoomOut.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnZoomOutActive.png")

		Starter = New ButtonControl(StarterActive, 0, 1)
		ManualCoupling = New ButtonControl(ManualCouplingActive, 0, 1)
		AWS = New ButtonControl(AWSActive, 0, 2)
		EBrake = New ButtonControl(EBrakeActive, 0, 1)

		Starter.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnStarterActive.png")
		ManualCoupling.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnManualCouplingActive.png")
		AWS.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnAWSActive1.png")
		AWS.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnAWSActive2.png")
		EBrake.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnEBrakeActive.png")

		PBrake = New ButtonControl(PBrakeActive, 0, 1)
		DriverGuide = New ButtonControl(DriverGuideActive, 0, 1)
		Map = New ButtonControl(MapActive, 0, 1)
		PADAhead = New ButtonControl(PADAheadActive, 0, 1)
		PADBehind = New ButtonControl(PADBehindActive, 0, 1)
		JunctionAhead = New ButtonControl(JunctionAheadActive, 0, 1)
		JunctionBehind = New ButtonControl(JunctionBehindActive, 0, 1)

		PBrake.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnPBrakeActive.png")
		DriverGuide.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnDriverGuideActive.png")
		Map.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/Btn2DMapActive.png")
		PADAhead.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnPADAheadActive.png")
		PADBehind.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnPADBehindActive.png")
		JunctionAhead.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnJunctionAheadActive.png")
		JunctionBehind.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnJunctionBehindActive.png")

		Vigilance = New ButtonControl(VigilanceActive, 0, 1)
		
		Vigilance.StateImages.Add("/RailWorks_Joystick_API;component/bin/Debug/Images/BtnVigilanceActive.png")

		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBgDay.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicator.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlass120.png", _
			120, 0, 270, False))

		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBgOrange.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicator.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlass120.png", _
			120, 0, 270, False))

		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBgBlue.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicatorBlue.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlass120.png", _
			120, 0, 270, False))

		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBg200Day.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicator.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlass200.png", _
			200, 0, 315, False))

		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBg200KPHDay.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicator.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlass200.png", _
			200, 0, 315, True))

		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBg300KPHDay.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicator.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlass200.png", _
			300, 0, 315, True))

		Speedos.Add(New SpeedometerData( _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoBg360KPHDay.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoIndicator.png", _
			"/RailWorks_Joystick_API;component/bin/Debug/Images/SpeedoGlass200.png", _
			360, 0, 315, True))

	End Sub

	Public Event SetupAnalogControls(ByVal Name As String, ByVal Description As String)
	Public Event SetupButtonControls(ByVal Name As String, ByVal Description As String)

	Private Sub AnalogControl_MouseDown(ByVal Sender As Object, ByVal e As Input.MouseButtonEventArgs) Handles _
		ThrottleControls.MouseDown, _
		TrainBrakeControls.MouseDown, _
		LocomotiveBrakeControls.MouseDown, _
		DynamicBrakeControls.MouseDown, _
		ReverserControls.MouseDown, _
		GearControls.MouseDown

		If (e.RightButton = Input.MouseButtonState.Pressed) Then
			Dim Ctrl As Controls.StackPanel = CType(Sender, Controls.StackPanel)
			Dim CtrlName As String = Ctrl.Name.Replace("Controls", "")
			Dim Description As String
			If (CtrlName = "Throttle") Then
				 Description = "Throttle / Combined Throttle & Brakes"
			Else
				 Description = Ctrl.ToolTip.ToString
			End If
			RaiseEvent SetupAnalogControls(CtrlName, Description)
		End If

	End Sub

	Private Sub ButtonControl_MouseDown(ByVal Sender As Object, ByVal e As Input.MouseButtonEventArgs) Handles _
			WipersActive.MouseDown, _
			LightsActive.MouseDown, _
			PantographActive.MouseDown, _
			LoadUnloadActive.MouseDown, _
			SanderActive.MouseDown, _
			SmallEjectorActive.MouseDown, _
			HornActive.MouseDown, _
			CylinderCocksActive.MouseDown, _
			HeadOutLeftActive.MouseDown, _
			CabActive.MouseDown, _
			HeadOutRightActive.MouseDown, _
			TracksideActive.MouseDown, _
			ExternalFrontActive.MouseDown, _
			ExternalRearActive.MouseDown, _
			NextCabActive.MouseDown, _
			PreviousCabActive.MouseDown, _
			CouplingActive.MouseDown, _
			YardActive.MouseDown, _
			FreeRoamActive.MouseDown, _
			StarterActive.MouseDown, _
			ManualCouplingActive.MouseDown, _
			AWSActive.MouseDown, _
			EBrakeActive.MouseDown, _
			PBrakeActive.MouseDown, _
			DriverGuideActive.MouseDown, _
			MapActive.MouseDown, _
			PADAheadActive.MouseDown, _
			PADBehindActive.MouseDown, _
			JunctionAheadActive.MouseDown, _
			JunctionBehindActive.MouseDown, _
			VigilanceActive.MouseDown
		
			' Not included because there is no Action ID for this
			' BellActive.MouseDown, _
			' AlternateActive.MouseDown, _
			' PassengerActive.MouseDown, _
			' ZoomInActive.MouseDown, _
			' ZoomOutActive.MouseDown, _

		If (e.RightButton = Input.MouseButtonState.Pressed) Then
			Dim Ctrl As Controls.Image = CType(Sender, Controls.Image)
			Dim CtrlName As String = Ctrl.Name.Replace("Active", "")
			Dim Description As String = Ctrl.ToolTip.ToString
			RaiseEvent SetupButtonControls(CtrlName, Description)
		End If
		
	End Sub

End Class