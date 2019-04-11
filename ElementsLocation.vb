Public Class ElementsLocation

    Public Sub setLocation(ByVal gameBoard As Panel,
                           ByVal bottomPanel As Panel,
                           ByVal topPanel As Panel,
                           ByVal level As Integer,
                           ByVal formHeight As Integer,
                           ByVal formWidth As Integer)

        ' Dim height As Integer = formHeight - gameBoard.Location.Y - (formHeight - bottomPanel.Location.Y)
        Dim height As Integer = formHeight - topPanel.Location.Y - topPanel.Height - (formHeight - bottomPanel.Location.Y)
        Dim width As Integer = formWidth

        If Not (width < 360 + 120 * (level - 1) Or height < 360 + 120 * (level - 1)) Then
            gameBoard.Width = 360 + 120 * (level - 1)
            gameBoard.Height = 360 + 120 * (level - 1)
        Else
            If width > height Then
                gameBoard.Width = height - 20
                gameBoard.Height = height - 20
            Else
                gameBoard.Width = width - 20
                gameBoard.Height = width - 20
            End If
        End If
        gameBoard.Location = New Point((formWidth - gameBoard.Width) / 2,
                                       (bottomPanel.Location.Y - topPanel.Height - gameBoard.Height) / 2 + topPanel.Height + 7)
    End Sub

End Class
