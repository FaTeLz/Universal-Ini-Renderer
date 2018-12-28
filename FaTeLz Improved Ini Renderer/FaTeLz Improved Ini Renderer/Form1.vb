
Imports System.IO
Imports System.Runtime.InteropServices

Public Class Form1
    <DllImport("user32.dll", EntryPoint:="FindWindowW")>
    Private Shared Function FindWindowW(<MarshalAs(UnmanagedType.LPTStr)> ByVal lpClassName As String, <MarshalAs(UnmanagedType.LPTStr)> ByVal lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", EntryPoint:="GetWindowRect")>
    Private Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As RECT) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Private Structure RECT
        Public left, top, right, bottom As Integer
    End Structure

    Public cfg As New universal()
    Public m_renderer As New rendering()
    Private Function update_properties()
        Dim hWnd As IntPtr = FindWindowW(Nothing, cfg.window)
        If hWnd <> IntPtr.Zero Then
            Dim wr As New RECT
            GetWindowRect(hWnd, wr)
            Me.Location = New Point(wr.left, wr.top)
            Me.Size = New Size(wr.right - wr.left, wr.bottom)
        End If
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Name = cfg.name
        Directory.CreateDirectory("C:\" & cfg.name)
        File.Create(cfg.inipath).Dispose()
        File.Create(cfg.logpath).Dispose()
        push_(cfg.name & " Successfully initialized.")

    End Sub
    Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Int32) As UShort

    Dim aim_lock_test As Boolean = False
    Dim objIniFile As New clsIni(cfg.inipath)
    Dim loaded = 0
    Public Sub push_(msg)
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(cfg.logpath, True)
        Dim str As String = "[" & TimeOfDay.ToString("h:mm:ss") & "] " & msg
        file.WriteLine(str)
        file.Close()
    End Sub
    Private Sub looper_Tick(sender As Object, e As EventArgs) Handles looper.Tick


        Me.TopMost = True
        Me.Name = cfg.name
        Me.Text = cfg.name
        update_properties()
        If (cfg.watermark) Then
            watermark.Visible = True
            watermark.Text = cfg.name & " | " & cfg.version

            watermark.Location = cfg.watermarklocation
        Else
            watermark.Visible = False
        End If

        If GetAsyncKeyState(Convert.ToInt32(Keys.End)) Then
            push_("Program  Exited.")
            Invalidate()
            Environment.Exit(0)
        End If

        If (loaded = 0) Then
            Console.Beep()
            cfg.Esp = False
            Panel1.Visible = True

            Threading.Thread.Sleep(5000)
            Panel1.Visible = False
            cfg.Esp = True

            loaded = 1
            Process.Start("https://discord.gg/gCG6BDN")

        End If
        Invalidate()

    End Sub
    Public Function hptocol(hp As Int32)
        If (hp < 25) Then
            Return Color.Red
        ElseIf (hp < 50) Then
            Return Color.Yellow
        ElseIf (hp < 75) Then
            Return Color.Green
        ElseIf (hp > 90) Then
            Return Color.Lime
        End If
        Return 0
    End Function

    Dim aimlocked = False
    Dim aimbot As New Aimbot
    Public Function Player(location As Point, state As String, name As String, hp As Int32, armor As Int32, wpn As String, amo As Int32, mamo As Int32)
        If (state = "enemy") Then
            If GetAsyncKeyState(Convert.ToInt32(cfg.Aimbot_Key)) Then
                If (hp > 0) Then
                    aimbot.Smoothness = cfg.Aimbot_Smoothness
                    aimbot.Run(location)
                    aimlocked = True
                End If
                watermark.Text = "Aimbot Locked On"
                watermark.BackColor = Color.HotPink
            Else
                aimlocked = False
                watermark.BackColor = Color.DarkOrange
                watermark.Text = cfg.name & " | " & cfg.version
            End If
        End If
        If (aimlocked = False) Then
            Dim C0l As Color
            If (state = "enemy") Then
                C0l = cfg.Enemy_Color
                'Box
                If (cfg.Boxes) Then
                    m_renderer.Draw_Rect(New Size(60, 160), location, C0l, cfg.Fill_Color)
                End If
                'Name
                If (cfg.Names) Then
                    m_renderer.Draw_Text(10.75, New Point(location.X + (60 / 2) - (7 / 2), location.Y - 25), C0l, name, True)
                End If
                'Head Circle
                If (cfg.Head) Then
                    m_renderer.Draw_Circle(7, New Point(location.X + (60 / 2) - (7 / 2), location.Y + (20)), C0l, 7)
                End If
                'health
                If (cfg.Health) Then
                    m_renderer.Draw_Rect(New Drawing.Size(10, 160), New Point(location.X - 15, location.Y), Color.Black, hptocol(hp))
                    m_renderer.Draw_Rect(New Drawing.Size(10, 160 - ((160 * hp) / 100)), New Point(location.X - 15, location.Y), Color.Black, Color.Black)
                End If
                'armor
                If (cfg.Armor) Then
                    m_renderer.Draw_Rect(New Drawing.Size(10, 160), New Point(location.X + 65, location.Y), Color.Black, Color.DodgerBlue)
                    m_renderer.Draw_Rect(New Drawing.Size(10, 160 - ((160 * armor) / 100)), New Point(location.X + 65, location.Y), Color.Black, Color.Black)
                End If
                'weapon
                If (cfg.Weapon) Then
                    m_renderer.Draw_Text(9.75, New Point(location.X + (60 / 2) - (7 / 2), location.Y + 160), Color.Gray, wpn & " (" & amo & "/" & mamo & ")", True)
                End If
                'SnapLines
                If (cfg.Snaplines) Then
                    m_renderer.Draw_Line(New Point(Width / 2, Height), New Point(location.X + 30, location.Y + 162), C0l)
                End If
            ElseIf (state = "friendly") Then
                If (show_friendlys) Then
                    C0l = cfg.Friendly_Color
                    'Box
                    If (cfg.Boxes) Then
                        m_renderer.Draw_Rect(New Size(60, 160), location, C0l, cfg.Fill_Color)
                    End If
                    'Name
                    If (cfg.Names) Then
                        m_renderer.Draw_Text(10.75, New Point(location.X + (60 / 2) - (7 / 2), location.Y - 25), C0l, name, True)
                    End If
                    'Head Circle
                    If (cfg.Head) Then
                        m_renderer.Draw_Circle(7, New Point(location.X + (60 / 2) - (7 / 2), location.Y + (20)), C0l, 7)
                    End If
                    'health
                    If (cfg.Health) Then
                        m_renderer.Draw_Rect(New Drawing.Size(10, 160), New Point(location.X - 15, location.Y), Color.Black, hptocol(hp))
                        m_renderer.Draw_Rect(New Drawing.Size(10, 160 - ((160 * hp) / 100)), New Point(location.X - 15, location.Y), Color.Black, Color.Black)
                    End If
                    'armor
                    If (cfg.Armor) Then
                        m_renderer.Draw_Rect(New Drawing.Size(10, 160), New Point(location.X + 65, location.Y), Color.Black, Color.DodgerBlue)
                        m_renderer.Draw_Rect(New Drawing.Size(10, 160 - ((160 * armor) / 100)), New Point(location.X + 65, location.Y), Color.Black, Color.Black)
                    End If
                    'weapon
                    If (cfg.Weapon) Then
                        m_renderer.Draw_Text(9.75, New Point(location.X + (60 / 2) - (7 / 2), location.Y + 160), Color.Gray, wpn & " (" & amo & "/" & mamo & ")", True)
                    End If
                    'SnapLines
                    If (cfg.Snaplines) Then
                        m_renderer.Draw_Line(New Point(Width / 2, Height), New Point(location.X + 30, location.Y + 162), C0l)
                    End If
                End If
            Else
                C0l = cfg.Other_Color
                m_renderer.Draw_Text(12.75, New Point(location.X + (60 / 2) - (7 / 2), location.Y), C0l, name, True)
            End If

        End If
        Return 1
    End Function

    Dim show_friendlys
    Public Function Entity_Looper()


        Dim max_players As Int32 = objIniFile.GetInteger("General", "Max_Entitys", 0)
        show_friendlys = objIniFile.GetInteger("General", "Show_Friendlys", 0)
        Dim px, py, pname, pstate, palive, phealth, current_entity, parmor, pweapon, pammo, pmaxammo

        For index As Integer = 1 To max_players
            current_entity = index
            px = objIniFile.GetInteger("Entity_" & current_entity, "x", 0)
            py = objIniFile.GetInteger("Entity_" & current_entity, "y", 0)
            pstate = objIniFile.GetString("Entity_" & current_entity, "state", "")
            pname = objIniFile.GetString("Entity_" & current_entity, "name", "")
            palive = objIniFile.GetInteger("Entity_" & current_entity, "alive", 0)
            phealth = objIniFile.GetInteger("Entity_" & current_entity, "health", 0)
            parmor = objIniFile.GetInteger("Entity_" & current_entity, "armor", 0)
            pweapon = objIniFile.GetString("Entity_" & current_entity, "weapon", "")
            pammo = objIniFile.GetInteger("Entity_" & current_entity, "ammo", 0)
            pmaxammo = objIniFile.GetInteger("Entity_" & current_entity, "maxammo", 0)

            If (palive > 0) Then
                If (cfg.Esp) Then
                    Player(New Point(px, py), pstate, pname, phealth, parmor, pweapon, pammo, pmaxammo)
                End If
            End If
        Next
        Return 1
    End Function
    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        m_renderer.Init(e.Graphics)
        If (loaded = 1) Then
            Entity_Looper()
        End If
    End Sub
End Class
