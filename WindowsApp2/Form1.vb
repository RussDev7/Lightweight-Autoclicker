Public Class Form1

    Public ReadOnly HelloThere = "Hello There Fellow Decompiler, This Program Was Made By D.RUSS#2430 (xXCrypticNightXx)."

#Region "Define Variables"

    ' Define imports and variables.
    ReadOnly Timer0 As New Timer
    ReadOnly Run0 As Integer
    Dim Ready As Integer

    ReadOnly Press1 As Integer
    ReadOnly Time1 As Integer
    Dim hotkey1 As Boolean

    ReadOnly Press2 As Integer
    ReadOnly Time2 As Integer
    Dim hotkey2 As Boolean

    Declare Sub mouse_event Lib "user32.dll" Alias "mouse_event" (ByVal dwFlags As Int32, ByVal dx As Int32, ByVal dy As Int32, ByVal cButtons As Int32, ByVal dwExtraInfo As Int32)
    Public Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Long, ByVal wMapType As Long) As Byte

    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Private Shared Function GetAsyncKeyState(ByVal vkey As System.Windows.Forms.Keys) As Short
    End Function

    Private Declare Sub keybd_event Lib "user32" (
    ByVal bVk As Byte,
    ByVal bScan As Byte,
    ByVal dwFlags As Integer,
    ByVal dwExtraInfo As Integer
)
    Private Const VK_ESCAPE As Integer = &H1B
    Private Const KEYEVENTF_EXTENDEDKEY As Integer = &H1
    Private Const KEYEVENTF_KEYUP As Integer = &H2

    <Flags()>
    Public Enum MouseEventFlags As UInteger
        MOUSEEVENTF_ABSOLUTE = &H8000
        MOUSEEVENTF_LEFTDOWN = &H2
        MOUSEEVENTF_LEFTUP = &H4
        MOUSEEVENTF_MIDDLEDOWN = &H20
        MOUSEEVENTF_MIDDLEUP = &H40
        MOUSEEVENTF_MOVE = &H1
        MOUSEEVENTF_RIGHTDOWN = &H8
        MOUSEEVENTF_RIGHTUP = &H10
        MOUSEEVENTF_XDOWN = &H80
        MOUSEEVENTF_XUP = &H100
        MOUSEEVENTF_WHEEL = &H800
        MOUSEEVENTF_HWHEEL = &H1000
    End Enum

    Private WithEvents Tmr As New Timer With {.Interval = 2000}

    Private Const MOUSEEVENTF_WHEEL As Integer = &H800 'The amount of movement is specified in mouseData.
    Private Const MOUSEEVENTF_HWHEEL As Integer = &H1000 'Not available for 2000 or XP

    Private Sub MouseScroll(ByVal up As Boolean, ByVal clicks As Integer)
        If up Then
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (clicks * 120), 0)
        Else
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -(clicks * 120), 0)
        End If
    End Sub

#End Region

    ' Do form laoding events.
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        Tmr.Start()
    End Sub

    ' Start the move and execute features.
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Ready = 1
    End Sub

    ' Enable / Disable Textbox.
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            TextBox3.Enabled = True
        Else
            TextBox3.Enabled = False
        End If
    End Sub

    ' Enable / Disable NumericUPDOWN.
    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            NumericUpDown2.Enabled = True
        Else
            NumericUpDown2.Enabled = False
        End If
    End Sub

#Region "Timers"

    ' X, Y coords bottom toolbar.
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        MyX_MyY.Text = " X:" & Cursor.Position.X & ", Y:" & Cursor.Position.Y
    End Sub

    ' Timer for start.
    Sub BASE_Tick() Handles BASE.Tick
        If Ready = 1 Then
            Me.Button3.Text = "PRESS F1 TO START"
            If Press2 = 0 Then
                hotkey1 = GetAsyncKeyState(Keys.F1) ' Start Timer 4
                If hotkey1 = True Then
                    Timer4.Enabled = True
                End If
            End If
        End If
    End Sub

    ' Do the main keypress functions.
    Sub Timer4_Tick() Handles Timer4.Tick
        Timer4.Interval = 1

        ' Dims
        Dim Sec As Integer ' 500 milliseconds = 0.5 seconds
        Sec = NumericUpDown1.Text

        Static count As Integer ' Counter

        If Press1 = 0 Then
            hotkey1 = GetAsyncKeyState(Keys.F2)
            If hotkey1 = True Then ' Stop

                Me.Button3.Text = "START"

                Timer4.Enabled = False
                Button3.Enabled = True

                If CheckBox3.Checked = True Then
                    keybd_event(Keys.ShiftKey, MapVirtualKey(Keys.ShiftKey, 0), KEYEVENTF_KEYUP, 0) ' SHIFTUP
                End If

                Ready = 0

            Else
                ' Do mouse click multiplier.
                If CheckBox4.Checked = True Then

                    ' Rapid Fire
                    For value As Integer = 0 To NumericUpDown2.Value Step 1

                        ' Control Edits
                        Button3.Enabled = False

                        ' Ensure mouse button press is enabled.
                        If Not RadioButton5.Checked Then
                            If RadioButton1.Checked = True Then ' Press mouse left click.
                                Threading.Thread.Sleep(Sec)
                                mouse_event(&H2, 0, 0, 0, 1)
                                Threading.Thread.Sleep(Sec)
                                mouse_event(&H4, 0, 0, 0, 1)
                            ElseIf RadioButton2.Checked = True Then ' Press mouse right click.
                                Threading.Thread.Sleep(Sec)
                                mouse_event(&H8, 0, 0, 0, 1)
                                Threading.Thread.Sleep(Sec)
                                mouse_event(&H10, 0, 0, 0, 1)
                            ElseIf RadioButton3.Checked = True Then ' Press mouse middle click.
                                Threading.Thread.Sleep(Sec)
                                mouse_event(&H20, 0, 0, 0, 1)
                                Threading.Thread.Sleep(Sec)
                                mouse_event(&H40, 0, 0, 0, 1)
                            ElseIf RadioButton4.Checked = True Then ' Press mouse left and right click.
                                Threading.Thread.Sleep(Sec)
                                mouse_event(&H2, 0, 0, 0, 1)
                                Threading.Thread.Sleep(Sec)
                                mouse_event(&H4, 0, 0, 0, 1)
                                Threading.Thread.Sleep(Sec / 2)
                                mouse_event(&H8, 0, 0, 0, 1)
                                Threading.Thread.Sleep(Sec / 2)
                                mouse_event(&H10, 0, 0, 0, 1)
                            End If
                        End If

                        ' Hold the shift key down.
                        If CheckBox3.Checked = True Then
                            keybd_event(Keys.ShiftKey, MapVirtualKey(Keys.ShiftKey, 0), KEYEVENTF_EXTENDEDKEY Or 0, 0)
                        End If

                        ' If scroll wheel is enabled, scroll 1 wheel clicks down.
                        If CheckBox1.Checked Then
                            MouseScroll(True, 1)
                        End If

                        ' If spam key is enabled send a desired key.
                        If CheckBox2.Checked Then
                            My.Computer.Keyboard.SendKeys(TextBox3.Text)
                        End If

                        ' Add one to count and update textbox.
                        count += 1
                        TextBox5.Text = count

                    Next

                Else
                    ' Do without the multipler feature.
                    ' Control Edits
                    Button3.Enabled = False

                    ' Ensure mouse button press is enabled.
                    If Not RadioButton5.Checked Then
                        If RadioButton1.Checked = True Then ' Press mouse left click.
                            Threading.Thread.Sleep(Sec)
                            mouse_event(&H2, 0, 0, 0, 1)
                            Threading.Thread.Sleep(Sec)
                            mouse_event(&H4, 0, 0, 0, 1)
                        ElseIf RadioButton2.Checked = True Then ' Press mouse right click.
                            Threading.Thread.Sleep(Sec)
                            mouse_event(&H8, 0, 0, 0, 1)
                            Threading.Thread.Sleep(Sec)
                            mouse_event(&H10, 0, 0, 0, 1)
                        ElseIf RadioButton3.Checked = True Then ' Press mouse middle click.
                            Threading.Thread.Sleep(Sec)
                            mouse_event(&H20, 0, 0, 0, 1)
                            Threading.Thread.Sleep(Sec)
                            mouse_event(&H40, 0, 0, 0, 1)
                        ElseIf RadioButton4.Checked = True Then ' Press mouse left and right click.
                            Threading.Thread.Sleep(Sec)
                            mouse_event(&H2, 0, 0, 0, 1)
                            Threading.Thread.Sleep(Sec)
                            mouse_event(&H4, 0, 0, 0, 1)
                            Threading.Thread.Sleep(Sec / 2)
                            mouse_event(&H8, 0, 0, 0, 1)
                            Threading.Thread.Sleep(Sec / 2)
                            mouse_event(&H10, 0, 0, 0, 1)
                        End If
                    End If

                    ' Hold the shift key down.
                    If CheckBox3.Checked = True Then
                        keybd_event(Keys.ShiftKey, MapVirtualKey(Keys.ShiftKey, 0), KEYEVENTF_EXTENDEDKEY Or 0, 0)
                    End If

                    ' If scroll wheel is enabled, scroll 1 wheel clicks down.
                    If CheckBox1.Checked Then
                        MouseScroll(True, 1)
                    End If

                    ' If spam key is enabled send a desired key.
                    If CheckBox2.Checked Then
                        My.Computer.Keyboard.SendKeys(TextBox3.Text)
                    End If

                    ' Add one to count and update textbox.
                    count += 1
                    TextBox5.Text = count

                End If
            End If
        End If
    End Sub

    ' +/- Clock for the numpad feature.
    Dim lock1 As Integer = 0
    Dim lock2 As Integer = 0
    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick

        ' Minus 1.
        hotkey1 = GetAsyncKeyState(Keys.Left)
        If hotkey1 = True Then
            If lock1 = 0 Then
                lock1 = 1

                If NumericUpDown1.Value.ToString > 1 Then
                    NumericUpDown1.Value = NumericUpDown1.Value.ToString - 1
                End If

            End If
        Else
            lock1 = 0
        End If

        ' Plus 1.
        hotkey2 = GetAsyncKeyState(Keys.Right)
        If hotkey2 = True Then
            If lock2 = 0 Then
                lock2 = 1
                NumericUpDown1.Value = NumericUpDown1.Value.ToString + 1
            End If
        Else
            lock2 = 0
        End If
    End Sub
#End Region

End Class
