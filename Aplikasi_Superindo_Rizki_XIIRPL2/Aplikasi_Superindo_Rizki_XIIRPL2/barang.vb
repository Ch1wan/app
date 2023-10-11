Imports System.Data.Odbc

Public Class barang
    Private Sub barang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call grid()
    End Sub
    Sub grid()
        Call koneksi()
        da = New OdbcDataAdapter("select * from barang", conn)
        ds = New DataSet()
        da.Fill(ds)
        dgv.DataSource = ds.Tables(0)
        dgv.ReadOnly = True
    End Sub
    Sub panggil_data()
        On Error Resume Next
        TextBox2.Text = dr.Item("nama_barang")
        ComboBox1.Text = dr.Item("satuan")
        TextBox3.Text = dr.Item("harga_jual")
        TextBox4.Text = dr.Item("harga_beli")
        TextBox6.Text = dr.Item("stok")
    End Sub
    Sub panggil_kode()
        Call koneksi()
        Cmd = New OdbcCommand("SELECT * FROM barang WHERE id_barang='" & TextBox1.Text & "'", conn)
        dr = Cmd.ExecuteReader
        dr.Read()
    End Sub
    Sub hapus()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox6.Clear()
        ComboBox1.Text = ""
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

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then
            ComboBox1.Focus()
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox6.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Data Belum Lengkap Wirr...!!", MsgBoxStyle.Critical, "INFORMASI")
            Exit Sub
        Else
            Call panggil_kode()
            If Not dr.HasRows Then
                'simpan 
                Dim SIMPAN As String = "INSERT INTO barang values('" & TextBox1.Text & "','" & TextBox2.Text & "', '" & ComboBox1.Text & "', '" & TextBox3.Text & "' , '" & TextBox4.Text & "' , '" & TextBox6.Text & "')"
                cmd = New OdbcCommand(SIMPAN, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil Di Simpan..", MsgBoxStyle.Information, "INFORMASI")
            Else
                'edit
                Dim EDIT As String = "UPDATE barang set nama_barang='" & TextBox2.Text & "', satuan='" & ComboBox1.Text & "', harga_jual='" & TextBox4.Text & "', harga_beli='" & TextBox3.Text & "' where id_barang='" & TextBox1.Text & "'"
                cmd = New OdbcCommand(EDIT, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil Diedit...", MsgBoxStyle.Information, "INFORMASI")
            End If
            Call grid()
            Call hapus()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Call hapus()
        TextBox1.Focus()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox6.Text = "" Then
            MsgBox("Data Belum Lengkap")
            Exit Sub
        Else
            If MessageBox.Show("Yakin Akan Dihapus", "PEINGATAN", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim delete As String = "delete from barang where id_barang='" & TextBox1.Text & "'"
                cmd = New OdbcCommand(delete, conn)
                cmd.ExecuteNonQuery()
                Call grid()
                Call hapus()
            End If
        End If
    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        On Error Resume Next
        TextBox1.Text = dgv.Rows(e.RowIndex).Cells(0).Value()
        Call panggil_kode()
        If dr.HasRows Then
            Call panggil_data()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub
    Sub caridata()
        Call koneksi()
        da = New OdbcDataAdapter("select * from barang where nama_barang like '%" & TextBox5.Text & "%' or id_barang like '%" & TextBox5.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds)
        dgv.DataSource = ds.Tables(0)
        dgv.ReadOnly = True
    End Sub
    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        Call caridata()

    End Sub
End Class