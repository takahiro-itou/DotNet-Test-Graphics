Module View

    Public Structure ViewInfo
        Public gView As System.Drawing.Graphics
        Public imgCanvas As System.Drawing.Bitmap
        Public imgIcons As System.Drawing.Bitmap
    End Structure

    Public Const SRCAND As Integer = &H8800C6
    Public Const SRCCOPY As Integer = &HCC0020
    Public Const SRCPAINT As Integer = &HEE0086

    <Runtime.InteropServices.DllImport("gdi32.dll")>
    Public Function BitBlt(ByVal hDestDC As IntPtr,
                       ByVal X As Integer, ByVal Y As Integer,
                       ByVal nWidth As Integer, ByVal nHeight As Integer,
                       ByVal hSrcDC As IntPtr,
                       ByVal xSrc As Integer, ByVal ySrc As Integer,
                       ByVal dwRop As Integer) As Integer
    End Function

    <Runtime.InteropServices.DllImport("gdi32.dll")>
    Public Function CreateCompatibleDC(ByVal hDC As IntPtr) As IntPtr
    End Function

    <Runtime.InteropServices.DllImport("gdi32.dll")>
    Public Function DeleteDC(ByVal hDC As IntPtr) As Integer
    End Function

    <Runtime.InteropServices.DllImport("gdi32.dll")>
    Public Function SelectObject(ByVal hDC As IntPtr, ByVal hGdiObj As IntPtr) As IntPtr
    End Function

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

    Public Sub DrawTextOn(ByRef utViewInfo As ViewInfo, ByVal strText As String, ByRef fnt As Font)

        Dim gCanvas As System.Drawing.Graphics
        Dim gIcons As System.Drawing.Graphics
        Dim destX As Integer, destY As Integer
        Dim copyWidth As Integer, copyHeight As Integer
        Dim srcX As Integer, srcY As Integer
        Dim hdcSrc As IntPtr, hdcDst As IntPtr, hobjOld As IntPtr

        With utViewInfo
            gCanvas = System.Drawing.Graphics.FromImage(.imgCanvas)
            gIcons = System.Drawing.Graphics.FromImage(.imgIcons)

            gCanvas.FillRectangle(Brushes.LightGray, gCanvas.VisibleClipBounds)

            ' アイコンを転送する
            destX = 32
            destY = 32
            copyWidth = 16
            copyHeight = 16
            srcX = 16
            srcY = 0

            hdcDst = gCanvas.GetHdc()
            hdcSrc = CreateCompatibleDC(hdcDst)
            hobjOld = SelectObject(hdcSrc, .imgIcons.GetHbitmap())
            BitBlt(hdcDst, destX, destY, copyWidth, copyHeight, hdcSrc, srcX, srcY + 16, SRCAND)
            BitBlt(hdcDst, destX, destY, copyWidth, copyHeight, hdcSrc, srcX, srcY, SRCPAINT)

            DeleteDC(hdcSrc)
            gCanvas.ReleaseHdc(hdcDst)

            gCanvas.DrawString(strText, fnt, Brushes.Red, destX + 32, destY)

            ' 最後に表示用のピクチャーボックスに転送する
            .gView.DrawImage(.imgCanvas, 0, 0)
        End With

        gCanvas.Dispose()
        gIcons.Dispose()

    End Sub

End Module
