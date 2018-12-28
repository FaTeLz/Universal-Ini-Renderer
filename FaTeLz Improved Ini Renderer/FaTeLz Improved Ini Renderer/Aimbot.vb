Public Class Aimbot
    Public Sub SetCursorPosition(pnt As Point)
        Cursor.Position = pnt
    End Sub
    Public Function GetCursorPosition()
        Return Cursor.Position
    End Function
    Public Sub Sleep(time As Int32)
        Threading.Thread.Sleep(time)
    End Sub

    Public Sub LinearSmoothMove(ByVal newPosition As Point, ByVal duration As TimeSpan)
        Dim start As Point = GetCursorPosition()
        Dim deltaX As Double = newPosition.X - start.X
        Dim deltaY As Double = newPosition.Y - start.Y
        Dim stopwatch As Stopwatch = New Stopwatch()
        stopwatch.Start()
        Dim timeFraction As Double = 0.0

        Do
            timeFraction = CDbl(stopwatch.Elapsed.Ticks) / duration.Ticks
            If timeFraction > 1.0 Then timeFraction = 1.0
            Dim curPoint As PointF = New PointF(start.X + timeFraction * deltaX, start.Y + timeFraction * deltaY)
            SetCursorPosition(Point.Round(curPoint))
            Sleep(20)
        Loop While timeFraction < 1.0
    End Sub

    Public Smoothness As Int32

    Public Function GetHeadFromPosition(pos As Point)
        Return New Point(pos.X + (60 / 2) - (10 / 2), pos.Y + (32))
    End Function
    Public cfg As New universal()
    Public Sub Run(Coords As Point)
        If (cfg.Aimbot_Enabled) Then
            Dim aimpoint As Point = GetHeadFromPosition(Coords)
            Dim smooth As New TimeSpan(Smoothness)
            LinearSmoothMove(aimpoint, smooth)
        End If
    End Sub

End Class
