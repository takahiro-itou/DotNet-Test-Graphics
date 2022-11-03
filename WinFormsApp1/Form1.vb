Public Class Form1

    Private utViewInfo As ViewInfo

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strDir As String

        strDir = GetRootDir(getAppPath(), "bin")
        LoadResources(strDir & "\Resource", utViewInfo)

        SetupGraphics(Me.picView, utViewInfo)
    End Sub

    Private Sub btnDraw_Click(sender As Object, e As EventArgs) Handles btnDraw.Click
        DrawTextOn(utViewInfo, Me.TextBox1.Text)
    End Sub
End Class
