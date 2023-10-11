Imports System.Data.Odbc

Module Module1
    Public conn As OdbcConnection
    Public ds As DataSet
    Public cmd As OdbcCommand
    Public da As OdbcDataAdapter
    Public dr As OdbcDataReader

    Public Sub koneksi()
        conn = New OdbcConnection("Dsn=penjualan_rizki_xiirpl2")
        conn.Open()
    End Sub
End Module
