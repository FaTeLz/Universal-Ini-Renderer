Public Class universal
    'General
    Public window As String = "" ' eg. "Untitled - Notepad"
    Public name As String = "" ' eg. "My hack name"
    Public inipath As String = "C:\" & name & "\" & name & ".ini"
    Public logpath As String = "C:\" & name & "\log.ini"
    Public version As String = "" ' eg. "1.0"

    'Aimbot
    Public Aimbot_Smoothness As Int32 = 20
    Public Aimbot_Enabled As Boolean = True
    Public Aimbot_Key = Keys.XButton1
    'Watermark
    Public watermark As Boolean = True
    Public watermarklocation As Point = New Point(8, 30)
    'Colors
    Public Enemy_Color As Color = Color.FromArgb(200, 255, 0, 0)
    Public Friendly_Color As Color  = Color.FromArgb(200, 0, 255, 0)
    Public Other_Color As Color = Color.FromArgb(200, 255, 174, 66)
    Public Fill_Color As Color
    'ESP
    Public Esp As Boolean = True
    Public Boxes As Boolean = True
    Public Names As Boolean = True
    Public Health As Boolean = True
    Public Armor As Boolean = True
    Public Weapon As Boolean = True
    Public Head As Boolean = True
    Public Snaplines As Boolean = True
End Class
