Imports System.IO

Public Class Form1
    Dim controles As List(Of Control)
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
        Dim texto As String = vbNewLine & "novo" & vbNewLine & " - lines:"
        Dim linha As String = ""
        Dim ndl As Integer = 1

        For i As Integer = 1 To 400
            If ndl = 20 Then
                ndl = 1
                texto = texto & vbNewLine & linha
                linha = ""
            Else
                Dim nome As String = "pic" & i
                linha &= retornarcor(Me.Controls.Find(nome, True)(0))
                ndl += 1
            End If
        Next

        If File.Exists(CurDir() & "/tenata/resultado.txt") = True Then
            IO.File.AppendAllText(CurDir() & "/tenata/resultado.txt", texto)
        Else
            If Not Path.Exists(CurDir() & "/tenata/") Then
                MkDir(CurDir() & "/tenata/")
            End If
            File.Create(CurDir() & "/tenata/resultado.txt").Close()
                IO.File.CreateText(CurDir() & "/tenata/resultado.txt").Close()
                IO.File.AppendAllText(CurDir() & "/tenata/resultado.txt", texto)
            End If

    End Sub

    Public Function retornarcor(c As Control)
        Dim cor As String = ""
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
            Case Else
                cor = "f"
        End Select
        Return "&" & cor & "▉"
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        PictureBox1.Left = Cursor.Position.X - Me.Left - 15
        PictureBox1.Top = Cursor.Position.Y - Me.Top - 40

    End Sub

    Private Sub PictureBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseClick
        Try

            For Each c As Control In Me.Controls
                If c.Tag <> vbNullString Then
                    If PictureBox1.Bounds.IntersectsWith(c.Bounds) Then
                        If c.Tag.ToString.Contains("cor") Then
                            PictureBox1.BackColor = c.BackColor
                        Else
                            If e.Button = MouseButtons.Right Then
                                c.BackColor = Color.White
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
End Class
