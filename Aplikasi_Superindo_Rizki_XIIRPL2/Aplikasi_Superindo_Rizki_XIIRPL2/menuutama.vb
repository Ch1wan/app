Imports System.Data.Odbc
Public Class menuutama

    Private Sub menuutama_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call biodata()
    End Sub

    Sub biodata()
        Call koneksi()
        cmd = New OdbcCommand("select * from user where id_user='" & Login.id.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            nama.Text = dr.Item("nama_user")
            Status.Text = dr.Item("status_user")
            TextBox1.Text = dr.Item("foto")
            Call gambar()
        End If
    End Sub
    Sub gambar()
        PictureBox1.Load(TextBox1.Text)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Status.Click

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        If MessageBox.Show("Yakin Akan Keluar ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
            Login.Show()
            Login.kosongkan()
            Call simpanlog_logout()

        Else

        End If
    End Sub
    Sub simpanlog_logout()
        Call koneksi()
        Dim simpanlog As String = "insert into log_aktivitas values('""','" & Panel1.Text & "','Logout','" & Format(DateValue(Login.lbl_tanggal.Text), "yyyy-mm-dd") & "','" & Login.lbl_jam.Text & "')"
        cmd = New OdbcCommand(simpanlog, conn)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click

        End
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        If MessageBox.Show("Yakin Akan Keluar ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
            Login.Show()
            Login.kosongkan()
            Call simpanlog_logout()
        Else
        End If
    End Sub

    Private Sub FormToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormToolStripMenuItem1.Click
        user.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        user.Show()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        customer.Show()
    End Sub

    Private Sub FormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormToolStripMenuItem.Click
        customer.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        barang.Show()
    End Sub

    Private Sub FormBarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormBarangToolStripMenuItem.Click
        barang.Show()
    End Sub
End Class