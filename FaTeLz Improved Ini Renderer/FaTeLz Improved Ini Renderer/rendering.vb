Public Class rendering
    Private pp As Graphics

    Public Function Init(g)
        pp = g
        Return True
    End Function
    Public Function Draw_Rect(size As Size, pos As Point, col As Color, p As Graphics)
        'p.DrawRectangle(New Pen(New SolidBrush(col)), New Rectangle(pos, size))
        p.DrawRectangle(Pens.White, New Rectangle(pos, size))

    End Function
    Public Function Draw_Rect(size As Size, pos As Point, col As Color, fillcol As Color)
        pp.FillRectangle(New SolidBrush(fillcol), New Rectangle(pos, size))
        pp.DrawRectangle(New Pen(New SolidBrush(col)), New Rectangle(pos, size))
    End Function
    Dim stringFormat As StringFormat = New StringFormat()

    Public Function Draw_Text(size As Int32, pos As Point, col As Color, text As String, centered As Boolean)
        stringFormat.Alignment = StringAlignment.Center

        If (centered) Then
            pp.DrawString(text, New Font("Verdana", size), New SolidBrush(col), pos, stringFormat)
        Else
            pp.DrawString(text, New Font("Verdana", size), New SolidBrush(col), pos)
        End If
    End Function
    Public Function Draw_Circle(size As Int32, pos As Point, col As Color, width As Int32)
        pp.DrawEllipse(New Pen(New SolidBrush(col), width), New Rectangle(pos, New Size(size, size)))
    End Function
    Public Function Draw_Line(p1 As Point, p2 As Point, col As Color)
        pp.DrawLine(New Pen(New SolidBrush(col)), p1, p2)
    End Function
End Class
