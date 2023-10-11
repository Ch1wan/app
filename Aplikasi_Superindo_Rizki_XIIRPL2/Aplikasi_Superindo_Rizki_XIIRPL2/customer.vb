Imports System.Data.Odbc

Public Class customer
    Private Sub customer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call grid()
    End Sub
    Sub hapus()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
    End Sub
    Sub grid()
        Call koneksi()
        da = New OdbcDataAdapter("select * from customer", conn)
        ds = New DataSet
        da.Fill(ds)
        dgv.DataSource = ds.Tables(0)
        dgv.ReadOnly = True
    End Sub
    Sub panggil_kode()
        Call koneksi()
        Cmd = New OdbcCommand("select * from customer where id_customer='" & TextBox1.Text & "'", conn)
        dr = Cmd.ExecuteReader
        dr.Read()
    End Sub
    Sub panggil_data()
        On Error Resume Next
        TextBox2.Text = dr.Item(1)
        TextBox3.Text = dr.Item(2)
        If RadioButton1.Text = dr.Item(3) Then
            RadioButton1.Checked = True
        ElseIf RadioButton2.Text = dr.Item(3) Then
            RadioButton2.Checked = True
        End If
    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        On Error Resume Next
        TextBox1.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        Call panggil_kode()
        If dr.HasRows Then
            Call panggil_data()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data Belum Lengkap")
            Exit Sub
        Else
            If MessageBox.Show("Yakin Akan Dihapus", "PEINGATAN", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim delete As String = "delete from customer where id_customer='" & TextBox1.Text & "'"
                cmd = New OdbcCommand(delete, conn)
                cmd.ExecuteNonQuery()
                Call grid()
                Call hapus()
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data belum lengkap!!", MsgBoxStyle.Critical, "INFORMASI")
            Exit Sub
        Else
            Call panggil_kode()
            Dim ju As String
            If RadioButton1.Checked = True Then
                ju = RadioButton1.Text
            ElseIf RadioButton2.Checked = True Then
                ju = RadioButton2.Text

            End If
            If Not dr.HasRows Then
                'simpan
                Dim simpan As String = "insert into customer values ('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & ju & "')"
                cmd = New OdbcCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil Di Simpan!", MsgBoxStyle.Information, "INFORMASI")
            Else
                'edit
                Dim edit As String = "update customer set nama_customer='" & TextBox2.Text & "', alamat_customer='" & TextBox3.Text & "', jenis_usaha='" & ju & "' where id_customer='" & TextBox1.Text & "'"
                cmd = New OdbcCommand(edit, conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data Berhasi Di Edit!", MsgBoxStyle.Information, "INFORMASI")

            End If
            Call grid()
            Call hapus()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call hapus()
        TextBox1.Focus()
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
        TextBox2.MaxLength = 30
        If e.KeyChar = Chr(13) Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub
    Sub caridata()
        Call koneksi()
        da = New OdbcDataAdapter("select * from customer where nama_customer like '%" & TextBox4.Text & "%' or id_customer like '%" & TextBox4.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds)
        dgv.DataSource = ds.Tables(0)
        dgv.ReadOnly = True
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        Call caridata()

    End Sub
End Class