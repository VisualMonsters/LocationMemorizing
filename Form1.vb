Imports System.Threading

Public Class Form1
    Dim LocationClass As LocationClass
    Dim ElementsLocation As New ElementsLocation

    Dim czas As Integer = 0
    Dim zmienna As Integer = 0


    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles Label9.Click
        LocationClass = New LocationClass(3, Panel3, Label11, Label2, Label10.Text)

        Label11.Text = "0"
        Panel3.Controls.Clear()
        Panel3.Enabled = True
        Panel3.BackColor = Color.FromArgb(240, 240, 240)

        czas = 0
        Timer1.Start()

        ElementsLocation.setLocation(Panel3, Panel1, Panel4, Label10.Text, Me.Height, Me.Width)

        Label1.Text = LocationClass.getLives.ToString

        LocationClass.stworzTabeleKwadratow(Panel3)

        'pasek w zegarze
        Panel2.Location = New Point(0, 0)
        zmienna = 4
    End Sub

    Public Sub nowykwadrat()
        ' Panel3.BackColor = Color.LightGreen
        Panel3.Enabled = False
        Application.DoEvents()
        Thread.Sleep(500)
        Application.DoEvents()
        Panel3.Visible = False
        Label1.Text = LocationClass.getLives.ToString
        Panel3.BackColor = Color.FromArgb(240, 240, 240)
        Panel3.Enabled = True
        ElementsLocation.setLocation(Panel3, Panel1, Panel4,  Label10.Text, Me.Height, Me.Width)

        zmienna -= 1

        If Not zmienna = 0 Then
            czas = 0
            Timer1.Start()
            LocationClass.stworzTabeleKwadratow(Panel3)
        Else
            czas = 0
            Timer1.Start()
            LocationClass.stworzTabeleKwadratow(Panel3)
            zmienna = 4
            LocationClass.setLevel = 1
        End If
        Panel3.Visible = True
    End Sub

#Region "pomocnicze"

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Label10.Text += 1
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        If Not Label10.Text - 1 <= 0 Then
            Label10.Text -= 1
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim _point As New Point
        _point = Panel2.Location
        _point.X -= 2
        Panel2.Location = _point
        If _point.X <= -120 Then
            LocationClass.UkryjTabeleKwadratow()
            Panel2.Location = New Point(0, 0)
            Timer1.Stop()
        End If

    End Sub
#End Region

End Class
