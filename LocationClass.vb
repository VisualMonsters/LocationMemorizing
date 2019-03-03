Public Class LocationClass
    Dim RandomNum As New Random
    Public lives As Integer
    Dim gameBoard As Panel
    Dim matrix As New List(Of List(Of Boolean))
    Dim mainBoard As New TableLayoutPanel
    Dim clickCounts As Integer = 0
    Dim points As Double = 0
    Dim pointsLabel As Label
    Dim negativePoints As Integer
    Dim clickCountsLabel As Label
    Dim level As Integer
    Dim ElementsLocation As New ElementsLocation

    Public ReadOnly Property getLives() As Integer
        Get
            Return lives
        End Get
    End Property

    Public WriteOnly Property setLevel As Integer
        Set(value As Integer)
            level =level+ value
        End Set
    End Property

    Public Sub New(lives As Integer, gameBoard As Panel, pointsLabel As Label, clickCountsLabel As Label, level As Integer)
        Me.lives = lives
        Me.gameBoard = gameBoard
        Me.pointsLabel = pointsLabel
        Me.clickCountsLabel = clickCountsLabel
        Me.level = level
    End Sub

    Public Sub stworzTabeleKwadratow(ByRef _panel As Panel)
        negativePoints = 0
        _panel.Controls.Clear()
        clickCounts = 0
        Dim iloscelementow2 As Integer
        _panel.Visible = False
        mainBoard.Controls.Clear()
        matrix.Clear()
        mainBoard.ColumnCount = 3 + level
        mainBoard.RowCount = 3 + level
        mainBoard.Margin = New Padding(3, 0, 0, 0)
        mainBoard.Dock = DockStyle.Fill

        For i As Integer = 0 To mainBoard.ColumnCount - 1
            mainBoard.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        Next
        For i As Integer = 0 To mainBoard.RowCount - 1
            mainBoard.RowStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        Next
        _panel.Controls.Add(mainBoard)
        For i As Integer = 0 To mainBoard.RowCount - 1
            iloscelementow2 = RandomNum.Next(0, 3 + level)
            Dim linia As New List(Of Boolean)
            For k As Integer = 0 To mainBoard.ColumnCount - 1
                linia.Add(False)
            Next
            ''tutaj musze zwiekszyc ilosc wraz ze zwiekszeniem poziomu trudnosci
            For l As Integer = 0 To iloscelementow2
                linia(RandomNum.Next(0, mainBoard.ColumnCount)) = True
            Next
            matrix.Add(linia)
            For j As Integer = 0 To mainBoard.ColumnCount - 1
                Dim _PictureBox As New PictureBox
                With _PictureBox
                    If linia(j) = True Then
                        .BackColor = Color.Red
                        .Enabled = False
                        AddHandler _PictureBox.Click, AddressOf PB_red
                    Else
                        .BackColor = Color.Gray
                        .Enabled = False
                        AddHandler _PictureBox.Click, AddressOf PB_grey
                    End If
                    .Dock = DockStyle.Fill
                End With
                mainBoard.Controls.Add(_PictureBox, j, i)
            Next
        Next
        For i As Integer = 0 To mainBoard.RowCount - 1
            For j As Integer = 0 To mainBoard.ColumnCount - 1
                If matrix(i).Item(j) = True Then
                    clickCounts += 1
                End If
            Next
        Next
        clickCountsLabel.Text = clickCounts
        mainBoard.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        _panel.Visible = True
    End Sub

    Public Sub UkryjTabeleKwadratow()
        For Each _pb As PictureBox In mainBoard.Controls
            _pb.BackColor = Color.DarkGray
            _pb.Cursor = Cursors.Hand
            _pb.Enabled = True
        Next
    End Sub

    Public Sub PB_red(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clickCounts -= 1
        DirectCast(sender, PictureBox).Enabled = False
        clickCountsLabel.Text = clickCounts
        If Not clickCounts < 1 Then
            DirectCast(sender, PictureBox).BackColor = Color.Red
            points += level / (clickCounts + 1)
            pointsLabel.Text = Math.Round(points).ToString

        Else
            DirectCast(sender, PictureBox).BackColor = Color.Red
            pointsLabel.Text = Math.Round(points).ToString
            checker(0)
        End If
    End Sub

    Public Sub PB_grey(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clickCountsLabel.Text = clickCounts
        If Not clickCounts < 1 Then
            DirectCast(sender, PictureBox).BackColor = Color.Gray
            If Not points - level <= 0 Then
                pointsLabel.Text = Math.Round(points).ToString
            End If
            negativePoints += 1
            If negativePoints >= 3 Then
                If lives = 1 Then
                    checker(1)
                Else
                    lives -= 1
                    checker(2)
                    checker(3)
                End If
            End If
        Else
            DirectCast(sender, PictureBox).BackColor = Color.Gray
            negativePoints += 1
            If negativePoints >= 3 Then
                checker(2)
            Else
                checker(3)
            End If
        End If
    End Sub

    Private Sub checker(ByVal wybor As Integer)
        If wybor = 0 Then
            Form1.nowykwadrat()
        ElseIf wybor = 1 Then
            gameBoard.BackColor = Color.Red
            gameBoard.Enabled = False
            Form1.Label1.Text = "0"
        ElseIf wybor = 2 Then
            gameBoard.BackColor = Color.Red
        ElseIf wybor = 3 Then
            Form1.nowykwadrat()
        End If
    End Sub
End Class
