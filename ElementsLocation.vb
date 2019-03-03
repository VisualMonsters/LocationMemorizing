Public Class ElementsLocation

    Public Sub setLocation(ByVal gameBoard As Panel, ByVal bottomPanel As Panel, ByVal topPanel As Panel, ByVal level As Integer, ByVal formHeight As Integer, ByVal formWidth As Integer)
        Dim wysokosc As Integer = formHeight - gameBoard.Location.Y - (formHeight - bottomPanel.Location.Y)
        Dim szerokosc As Integer = formWidth

        If Not (szerokosc < 360 + 120 * (level - 1) Or wysokosc < 360 + 120 * (level - 1)) Then

            gameBoard.Width = 360 + 120 * (level - 1) 'numericupdown2 to kolumny- 9,16,25,36,49,
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 3,4,5,6,7
            gameBoard.Height = 360 + 120 * (level - 1)
        Else
            If szerokosc > wysokosc Then
                gameBoard.Width = wysokosc - 20 'numericupdown2 to kolumny- 9,16,25,36,49,
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 3,4,5,6,7
                gameBoard.Height = wysokosc - 20

            Else
                gameBoard.Width = szerokosc - 20 'numericupdown2 to kolumny- 9,16,25,36,49,
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 3,4,5,6,7
                gameBoard.Height = szerokosc - 20

            End If
        End If
        gameBoard.Location = New Point((formWidth - gameBoard.Width) / 2, (bottomPanel.Location.Y - topPanel.Height - gameBoard.Height) / 2 + topPanel.Height)

        gameBoard.Visible = True
    End Sub

End Class
