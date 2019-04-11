Imports System.Threading

Public Class LocationClass
    Dim RandomNum As New Random
    Dim ElementsLocation As New ElementsLocation
    Dim mainBoard As New TableLayoutPanel
    Dim gameBoard As Panel
    Dim pointsLabel As Label
    Dim clickCountsLabel As Label
    Dim livesLabel As Label

    Public lives As Integer
    Dim clickCounts As Integer
    Dim points As Double = 0
    Dim negativePoints As Integer
    Dim level As Integer
    Dim gameCount As Integer = 4
    Dim matrix As New List(Of List(Of Boolean))

    Public ReadOnly Property getLevel As Integer
        Get
            Return level
        End Get
    End Property

    Public Sub New(lives As Integer, gameBoard As Panel, pointsLabel As Label, clickCountsLabel As Label, level As Integer, livesLabel As Label)
        Me.lives = lives
        Me.gameBoard = gameBoard
        Me.pointsLabel = pointsLabel
        Me.clickCountsLabel = clickCountsLabel
        Me.level = level
        Me.livesLabel = livesLabel
    End Sub

    '1. tworzymy kwadraty, inicjujemy gre
    Public Sub createNewGame()

        livesLabel.Text = lives
        negativePoints = 0
        gameBoard.Controls.Clear()
        clickCounts = 0

        gameBoard.Visible = False
        mainBoard.Controls.Clear()
        matrix.Clear()
        'inicjujemy wielkość planszy
        mainBoard.ColumnCount = 3 + level
        mainBoard.RowCount = 3 + level

        mainBoard.Margin = New Padding(3, 0, 0, 0)
        mainBoard.Dock = DockStyle.Fill

        'ponieważ nasza gra bazuje na obiekcie TableLayoutPanel, musimy go sensownie przygotować i podzielić
        For i As Integer = 0 To mainBoard.ColumnCount - 1
            mainBoard.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        Next
        For i As Integer = 0 To mainBoard.RowCount - 1
            mainBoard.RowStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        Next
        'dodajemy TableLayoutPanel do naszego Panelu gry
        gameBoard.Controls.Add(mainBoard)

        Dim amountOfElements As Integer
        For i As Integer = 0 To mainBoard.RowCount - 1
            'losujemy ile w danym wierszu ma być losowych elementów do zapamiętania od 0 do 3 + lewel
            amountOfElements = RandomNum.Next(0, 3 + level)
            'określamy liste elementów boolen w której zaznaczymy nasze elementy do zapamiętania
            Dim line As New List(Of Boolean)
            For k As Integer = 0 To mainBoard.ColumnCount - 1
                'wszystkie pola chwilowo robimy na false
                line.Add(False)
            Next
            'teraz trik który zaznaczy nam pola, 
            'dla wylosowanych pól, losujemy miejsca
            For l As Integer = 0 To amountOfElements
                line(RandomNum.Next(0, mainBoard.ColumnCount)) = True
            Next
            'dodajemy linie do zestawu
            matrix.Add(line)
            'tworzymy obiekty w TableLayoutPanel, w każdym polu tworzymy
            'mały panel odpowiedniego koloru
            For j As Integer = 0 To mainBoard.ColumnCount - 1
                Dim _PictureBox As New PictureBox
                With _PictureBox
                    If line(j) = True Then
                        .BackColor = Color.Red
                        .Enabled = False
                        'dodajemy im zdarzenie kliknięcia
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
        'określamy ile jest czerwonych obiektów na planczy, tych do zapamiętania
        For i As Integer = 0 To mainBoard.RowCount - 1
            For j As Integer = 0 To mainBoard.ColumnCount - 1
                If matrix(i).Item(j) = True Then
                    clickCounts += 1
                End If
            Next
        Next

        clickCountsLabel.Text = clickCounts
        mainBoard.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        'odsłaniamy naszą plansze
        gameBoard.Visible = True
    End Sub

    '2. ukryj kwadraty
    Public Sub hideSquares()
        For Each _pb As PictureBox In mainBoard.Controls
            _pb.BackColor = Color.DarkGray
            _pb.Cursor = Cursors.Hand
            _pb.Enabled = True
        Next
    End Sub

    '3. mimo tego, że zmienimy kolory naszych elementów ich adresy pozostaną bez zian
    ''' dlatego gdy klikniemy na szry kwadrat który kiedyś był czerwony jego adres się nie zmieni
    ''' 
    Public Sub PB_red(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'użytkownik trafił na właściwy element
        ''zmniejszamy ilość szukanych elementów
        clickCounts -= 1
        ''blokuj element
        DirectCast(sender, PictureBox).Enabled = False
        clickCountsLabel.Text = clickCounts 'wyświetl pozostałe elementy
        'jeśli zostały jakieś elementy wtedy
        If Not clickCounts < 1 Then
            'ustaw odpowiedni kolor
            DirectCast(sender, PictureBox).BackColor = Color.Red
            'dodaj punkty
            points += level / (clickCounts + 1)
            pointsLabel.Text = Math.Round(points).ToString 'wyświetl punkty
        Else
            'nie ma więcej elementów
            DirectCast(sender, PictureBox).BackColor = Color.Red
            pointsLabel.Text = Math.Round(points).ToString
            'koniec gry, zwiększ poziom i przejdź do następnej gry
            checker(0)
        End If
    End Sub
    ''' 
    Public Sub PB_grey(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'użytkownik trafił na szary element
        ''zmieniamy kolor
        DirectCast(sender, PictureBox).BackColor = Color.Gray
        'dodajemy punkty negatywne, trzy punkty i koniec etapu
        negativePoints += 1
        If negativePoints = 3 Then
            If lives = 1 Then
                'koniec gry, ostatnie życie
                checker(1)
            Else
                lives -= 1
                checker(2)
            End If
        End If
    End Sub
    ''' 
    'ten element sprawdza czy pzostały jeszcze elementy do odnalezienia
    Private Sub checker(ByVal wybor As Integer)
        If wybor = 0 Then
            'gra udana
            gameBoard.BackColor = Color.LightGreen
            nextGame()
        ElseIf wybor = 1 Then
            gameBoard.BackColor = Color.Red
            gameBoard.Enabled = False
            livesLabel.Text = "0"
        ElseIf wybor = 2 Then
            gameBoard.BackColor = Color.Red
            nextGame()
        End If
    End Sub

    Public Sub nextGame()
        'unieruchamiamy naszą plansze na pół sekundy
        gameBoard.Enabled = False
        Application.DoEvents()
        Thread.Sleep(500)
        Application.DoEvents()
        gameBoard.Visible = False

        livesLabel.Text = lives.ToString

        gameBoard.BackColor = Color.FromArgb(240, 240, 240)
        gameBoard.Enabled = True

        'każda seria składa się z 4 gier
        'poziom zwiększany ejst co cztery gry

        gameCount -= 1

        If Not gameCount = 0 Then
            'uruchom następną gre
            Form1.time = 0
            Form1.Timer1.Start()
            createNewGame()
        Else
            'to już ostatnia gra, zwiększ poziom
            'aby następna plansza była większa
            Form1.time = 0
            Form1.Timer1.Start()
            createNewGame()
            gameCount = 4
            level += 1
        End If

        gameBoard.Visible = True
        Form1.setMainBoardLocation(level)
    End Sub
End Class
