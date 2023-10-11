Imports System.Data.Odbc
Public Class user
    Private Sub user_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call tampil_grid()
        Call tampil_status()
    End Sub
    Sub hapus()
        TextBox2.Clear()
        TextBox3.Clear()
        ComboBox1.Text = ""
        TextBox4.Clear()
        TextBox5.Clear()
        PictureBox3.Refresh()
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Data Belum Lengkap")
            Exit Sub
        Else
            If MessageBox.Show("Yakin Akan Dihapus", "PEINGATAN", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim delete As String = "delete from user where id_user='" & TextBox1.Text & "'"
                cmd = New OdbcCommand(delete, conn)
                cmd.ExecuteNonQuery()
                Call tampil_grid()
                Call hapus()
            End If
        End If
    End Sub
    Sub panggil_kode()
        Call koneksi()
        cmd = New OdbcCommand("select * from user where id_user='" & TextBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub
    Sub panggil_data()
        On Error Resume Next
        TextBox2.Text = dr.Item("nama_user")
        TextBox3.Text = dr.Item(2)
        ComboBox1.Text = dr.Item("status_user")
        TextBox4.Text = dr.Item("foto")
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Call hapus()
        TextBox1.Focus()
    End Sub
    Sub tampil_grid()
        Call koneksi()
        da = New OdbcDataAdapter("select * from user", conn)
        ds = New DataSet
        da.Fill(ds)
        dgv.DataSource = ds.Tables(0)
        dgv.ReadOnly = True

    End Sub
    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        On Error Resume Next
        TextBox1.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        Call panggil_kode()
        If dr.HasRows Then
            Call panggil_data()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        TextBox1.MaxLength = 10
        If e.KeyChar = Chr(13) Then
            TextBox2.Focus()
            Call panggil_kode()
            If dr.HasRows Then
                Call panggil_data()
            Else
                Call hapus()
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        TextBox2.MaxLength = 30
        If e.KeyChar = Chr(13) Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        TextBox3.MaxLength = 12
        If e.KeyChar = Chr(13) Then
            ComboBox1.Focus()
        End If
    End Sub
    Sub tampil_status()
        Call koneksi()
        cmd = New OdbcCommand("select distinct status_user from user", conn)
        dr = cmd.ExecuteReader
        ComboBox1.Items.Clear()
        Do While dr.Read
            ComboBox1.Items.Add(dr.Item("status_user"))
        Loop
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
        OpenFileDialog1.Filter = "(*.jpg) | *.jpg"
        PictureBox3.Text = PictureBox3.Text + "<img>" + OpenFileDialog1.FileName + "</img>"
        TextBox4.Text = OpenFileDialog1.FileName
    End Sub
    Sub gambar()
        On Error Resume Next
        PictureBox3.Load(TextBox4.Text)
        PictureBox3.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        Call gambar()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Data belum lengkap!", MsgBoxStyle.Critical, "INFORMATION")
            Exit Sub
        Else
            Call panggil_kode()
            Dim newtext As String
            Dim oldtext As String = TextBox4.Text
            newtext = oldtext.Replace("\", "\\")

            If Not dr.HasRows Then
                Dim simpa As String = "insert into user values ('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & ComboBox1.Text & "', '" & newtext & "')"
                cmd = New OdbcCommand(simpa, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil Di input", MsgBoxStyle.Information, "INFORMATION")
            Else
                Dim edit As String = "update user set nama_user='" & TextBox2.Text & "', pwd_user='" & TextBox3.Text & "', status_user='" & ComboBox1.Text & "', foto='" & newtext & "' where id_user='" & TextBox1.Text & "'"
                cmd = New OdbcCommand(edit, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil Diedit", MsgBoxStyle.Information, "INFORMATION")
            End If
            Call tampil_grid()
            Call hapus()
            Call tampil_status()
        End If
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        TextBox3.UseSystemPasswordChar = False
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        TextBox3.UseSystemPasswordChar = True
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
    Sub caridata()
        Call koneksi()
        da = New OdbcDataAdapter("select * from user where nama_user like '%" & TextBox5.Text & "%' or id_user like '%" & TextBox5.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds)
        dgv.DataSource = ds.Tables(0)
        dgv.ReadOnly = True
    End Sub
    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        Call caridata()
    End Sub
End Class