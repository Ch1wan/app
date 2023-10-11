Imports System.Data.Odbc
Public Class Login


    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        lbl_tanggal.Text = Format(Today)
        teks2 = runningteks2.Text
        teks = runningteks1.Text
    End Sub
    Dim percobaan As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If id.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Data Belum Lengkap Coyyy........", MsgBoxStyle.Information, "PERINGATAN")
            id.Focus()
            Exit Sub
        End If
        Call koneksi()
        cmd = New OdbcCommand("select * from user where id_user='" & id.Text & "' and pwd_user='" & TextBox2.Text & "' and status_user='" & ComboBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            Me.Visible = False
            menuutama.Show()
            menuutama.Panel1.Text = dr.Item("id_user")
            menuutama.Panel2.Text = dr.Item("nama_user")
            menuutama.Panel3.Text = dr.Item("status_user")
            If menuutama.Panel3.Text = "Admin" Then

            ElseIf menuutama.Panel3.Text = "Operator" Then
                menuutama.TransaksiToolStripMenuItem.Enabled = False
                menuutama.LaporanToolStripMenuItem.Enabled = False
                menuutama.GroupBox9.Enabled = False
                menuutama.GroupBox10.Enabled = False
            ElseIf menuutama.Panel3.Text = "Kasir" Then
                menuutama.FileToolStripMenuItem.Enabled = False
                menuutama.GroupBox8.Enabled = False
            End If
            Call simpanlog()
        Else
            percobaan = percobaan + 1
            MsgBox("Login Gagal.....", MsgBoxStyle.Information, "PERINGATAN")
            Call kosongkan()
            If percobaan > 2 Then
                MsgBox("Anda Telah Gagal Melakukan Login Sebanyak 3x, Silahkan Login Kembali", MsgBoxStyle.Critical, "INFORMATION")
                End
            End If
        End If
    End Sub
    Sub kosongkan()
        id.Clear()
        TextBox2.Clear()
        ComboBox1.Text = ""
        id.Focus()
    End Sub
    Sub simpanlog()
        Call koneksi()
        Dim simpanlog As String = "insert into log_aktivitas values('""','" & menuutama.Panel1.Text & "','login','" & Format(DateValue(lbl_tanggal.Text), "yyyy-mm-dd") & "','" & lbl_jam.Text & "')"
        cmd = New OdbcCommand(simpanlog, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data Log Berhasil Disimpan", MsgBoxStyle.Information)
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lbl_jam.Text = Format(TimeOfDay)

    End Sub
    Dim bergerak As Integer
    Dim teks As String

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        runningteks1.Text = bergerak
        teks = Microsoft.VisualBasic.Right(teks, Len(teks) - 3) & Microsoft.VisualBasic.Left(teks, 3)
        runningteks1.Text = teks
    End Sub
    Dim bergerak2 As Integer
    Dim teks2 As String

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        runningteks2.Text = bergerak2
        teks2 = Microsoft.VisualBasic.Right(teks2, Len(teks2) - 2) & Microsoft.VisualBasic.Left(teks2, 2)
        runningteks2.Text = teks2
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles id.KeyPress
        id.MaxLength = 11
        If e.KeyChar = Chr(13) Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        TextBox2.MaxLength = 8
        If e.KeyChar = Chr(13) Then
            ComboBox1.Focus()
        End If
    End Sub

    Private Sub PictureBox2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox2.MouseDown
        TextBox2.UseSystemPasswordChar = False

    End Sub

    Private Sub PictureBox2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox2.MouseUp
        TextBox2.UseSystemPasswordChar = True

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Button1.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles id.TextChanged

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

    End Sub
End Class