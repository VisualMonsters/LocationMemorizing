Imports System.Threading

Public Class Form1
    'rdzeń gry
    Dim LocationClass As LocationClass
    'odpowiedzialna za ułożenie elementów
    Dim ElementsLocation As New ElementsLocation

    Public time As Integer = 0


    Private Sub startLabel_Click(sender As Object, e As EventArgs) Handles startLabel.Click
        'rzutowanie elementów gry
        LocationClass = New LocationClass(3, mainBoard, points, left, level.Text, lives)
        'przygotowanie elementów
        points.Text = "0"

        mainBoard.Controls.Clear()
        mainBoard.Enabled = True
        mainBoard.BackColor = Color.FromArgb(240, 240, 240)

        time = 0
        'czas start
        Timer1.Start()
        'ustawiamy lokacje planszy na podstawie levelu gracza
        setMainBoardLocation(level.Text)
        'zaczynamy gre od stworzenia kwadratów
        LocationClass.createNewGame()

        'pasek w zegarze
        Panel2.Location = New Point(0, 0)
    End Sub

    Public Sub setMainBoardLocation(ByVal level As Integer)
        ElementsLocation.setLocation(mainBoard, timeControlPanel, gameControlPanel, level, Me.Height, Me.Width)
    End Sub

#Region "pomocnicze"

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        level.Text += 1
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        If Not level.Text - 1 <= 0 Then
            level.Text -= 1
        End If
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Not (LocationClass Is Nothing) Then
            setMainBoardLocation(LocationClass.getLevel)
        End If

    End Sub
#End Region

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim _point As New Point
        _point = Panel2.Location
        _point.X -= 2
        Panel2.Location = _point
        If _point.X <= -120 Then
            '
            LocationClass.hideSquares()
            Panel2.Location = New Point(0, 0)
            Timer1.Stop()
        End If

    End Sub

End Class
