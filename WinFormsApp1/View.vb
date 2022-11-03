Module View

    Public Structure ViewInfo
        Public gView As System.Drawing.Graphics
        Public imgCanvas As System.Drawing.Bitmap
        Public imgIcons As System.Drawing.Image
    End Structure

    Public Function getAppPath() As String
        ''--------------------------------------------------------------------
        ''    アプリケーションの実行ディレクトリを取得する。
        ''--------------------------------------------------------------------
        Dim strDir As String

        strDir = System.IO.Path.GetDirectoryName(
            System.Reflection.Assembly.GetExecutingAssembly().Location)
        getAppPath = strDir

    End Function

    Public Sub LoadResources(ByVal baseDir As String, ByRef utViewInfo As ViewInfo)
        Dim strDir As String

        With utViewInfo
            .imgIcons = New System.Drawing.Bitmap(baseDir & "\Icons.bmp")
        End With

    End Sub

    Public Sub SetupGraphics(ByRef picView As System.Windows.Forms.PictureBox, ByRef utViewInfo As ViewInfo)
        Dim w As Int32
        Dim h As Int32

        With picView
            If .Image Is Nothing Then
                .Image = New System.Drawing.Bitmap(.Width, .Height)
            End If
        End With

        With utViewInfo
            .gView = System.Drawing.Graphics.FromImage(picView.Image)
            With .gView.VisibleClipBounds
                w = .Width
                h = .Height
            End With
            .imgCanvas = New System.Drawing.Bitmap(w, h)
        End With

    End Sub

    Public Function GetRootDir(ByVal strFullPath As String, ByVal stripDir As String) As String

        Dim strWorkPath As String
        Dim strDir As String
        Dim strFile As String

        strWorkPath = strFullPath

        While strWorkPath <> ""
            strDir = System.IO.Path.GetDirectoryName(strWorkPath)
            strFile = System.IO.Path.GetFileName(strWorkPath)
            If strFile = stripDir Then
                GetRootDir = strDir
                Exit Function
            End If
            strWorkPath = strDir
        End While

        GetRootDir = strFullPath

    End Function

    Public Sub DrawTextOn(ByRef utViewInfo As ViewInfo, ByVal strText As String)

        Dim gCanvas As System.Drawing.Graphics
        Dim srcRect As System.Drawing.Rectangle

        With utViewInfo
            gCanvas = System.Drawing.Graphics.FromImage(.imgCanvas)
            gCanvas.FillRectangle(Brushes.Yellow, gCanvas.VisibleClipBounds)

            ' アイコンを転送する
            srcRect = New System.Drawing.Rectangle(0, 0, 16, 16)
            gCanvas.DrawImage(.imgIcons, 32, 32, srcRect, GraphicsUnit.Pixel)

            ' 最後に表示用のピクチャーボックスに転送する
            .gView.DrawImage(.imgCanvas, 0, 0)
        End With

    End Sub

End Module
