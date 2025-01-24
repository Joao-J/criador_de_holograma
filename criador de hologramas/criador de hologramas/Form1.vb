Imports System.IO
Imports Microsoft.VisualBasic.Devices

Public Class Form1
    Dim localizacao_arquivo = CurDir() & "/tenata/resultado.txt"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ndp As Integer = 1

        For i As Integer = 1 To 20
            For i2 As Integer = 1 To 20
                Dim pixel As New PictureBox
                With pixel
                    .Width = 20
                    .Height = 20
                    .BackColor = Color.White
                    .Tag = "pic"
                    .Top = i * 20 + (1 * i)
                    .Left = i2 * 20 + (1 * i2)
                    .Tag &= ndp
                    .Name = .Tag
                End With
                Me.Controls.Add(pixel)
                ndp += 1
            Next
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim texto = vbNewLine & "novo" & vbNewLine & " - lines:"
        Dim linha = ""
        Dim ndl = 1

        For i = 1 To 400
            Dim nome = "pic" & i
            If ndl = 20 Then
                ndl = 1
                linha &= retornarcor(Controls.Find(nome, True)(0))
                texto = texto & vbNewLine & "  - content: '" & linha & "'" & vbNewLine & "    height: 0.3"
                linha = ""
            Else
                linha &= retornarcor(Controls.Find(nome, True)(0))
                ndl += 1
            End If
        Next

        If File.Exists(localizacao_arquivo) = True Then
            File.AppendAllText(localizacao_arquivo, texto)
        Else
            If Not Path.Exists(localizacao_arquivo) Then
                MkDir(localizacao_arquivo)
            End If
            File.Create(localizacao_arquivo).Close()
            File.CreateText(localizacao_arquivo).Close()
            File.AppendAllText(localizacao_arquivo, texto)
        End If

    End Sub

    Public Function retornarcor(c As Control)
        Dim cor As String = ""
        Dim caractere As String = "▉"
        Select Case c.BackColor
            Case Color.White
                cor = "f"
            Case Color.Blue
                cor = "1"
            Case Color.Red
                cor = "4"
            Case Color.Green
                cor = "2"
            Case Color.Yellow
                cor = "e"
            Case Color.Orange
                cor = "6"
            Case Color.Gray
                cor = "8"
            Case Color.Lime
                cor = "a"
            Case Color.Magenta
                cor = "5"
            Case Color.Pink
                cor = "d"
            Case Color.Cyan
                cor = "3"
            Case Color.Black
                cor = "0"
            Case Me.BackColor
                caractere = " "
                cor = "f"
            Case Else
                cor = "f"
        End Select
        Return "&" & cor & caractere
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        PictureBox1.Left = MousePosition.X - Me.Left - 15
        PictureBox1.Top = MousePosition.Y - Me.Top - 40
    End Sub

    Private Sub PictureBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseClick
        pintar(e)
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        pintar(e)
    End Sub

    Public Sub pintar(e As MouseEventArgs)
        Try
            For Each c As Control In Me.Controls
                If c.Tag <> vbNullString Then
                    If PictureBox1.Bounds.IntersectsWith(c.Bounds) Then
                        If c.Tag.ToString.Contains("cor") Then
                            If e.Button = MouseButtons.Left Then
                                PictureBox1.BackColor = c.BackColor
                            End If

                        Else
                                If e.Button = MouseButtons.Right Then
                                c.BackColor = Me.BackColor
                            ElseIf e.Button = MouseButtons.Left Then
                                c.BackColor = PictureBox1.BackColor
                            End If
                        End If
                    End If
                End If

            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If File.Exists(localizacao_arquivo) = True Then
            Process.Start("notepad.exe", localizacao_arquivo)
        Else
            If Not Path.Exists(CurDir() & "/tenata/") Then
                MkDir(CurDir() & "/tenata/")
            End If
            File.Create(CurDir() & "/tenata/resultado.txt").Close()
            File.CreateText(CurDir() & "/tenata/resultado.txt").Close()
        End If

    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        If PictureBox1.Height >= 50 Then
            PictureBox1.Height = 10
            PictureBox1.Width = PictureBox1.Height
        Else
            PictureBox1.Height += 10
            PictureBox1.Width = PictureBox1.Height
        End If
    End Sub
End Class
