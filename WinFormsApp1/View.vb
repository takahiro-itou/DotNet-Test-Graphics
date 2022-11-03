Module View

    Public Structure ViewInfo
        Public gView As System.Drawing.Graphics
        Public imgCanvas As System.Drawing.Bitmap
        Public imgIcons As System.Drawing.Image
    End Structure

    Public Sub LoadResources(ByVal baseDir As String, ByRef utViewInfo As ViewInfo)

        With utViewInfo
            .imgIcons = New System.Drawing.Bitmap(baseDir & "\Icons.bmp")
        End With

    End Sub

    Public Sub SetupGraphics(ByRef picView As System.Windows.Forms.PictureBox, ByRef utViewInfo As ViewInfo)
        Dim w As Int32
        Dim h As Int32

        With utViewInfo
            .gView = System.Drawing.Graphics.FromImage(picView.Image)
            With .gView.VisibleClipBounds
                w = .Width
                h = .Height
            End With
            .imgCanvas = New System.Drawing.Bitmap(w, h)
        End With

    End Sub

    Public Sub DrawTextOn(ByRef utViewInfo As ViewInfo, ByVal strText As String)

        Dim gCanvas As System.Drawing.Graphics

        With utViewInfo
            gCanvas = System.Drawing.Graphics.FromImage(.imgCanvas)
            gCanvas.FillRectangle(Brushes.Yellow, gCanvas.VisibleClipBounds)
        End With

    End Sub

End Module
