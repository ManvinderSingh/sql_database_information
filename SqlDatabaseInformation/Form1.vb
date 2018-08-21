Imports System.Data.SqlClient

Public Class Form1

    Dim _connection As SqlConnection


    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        If (String.IsNullOrWhiteSpace(txtConnectionString.Text)) Then
            MessageBox.Show("Please paste valid connection string")
            Exit Sub
        End If

        _connection = New SqlConnection(Me.txtConnectionString.Text)
        Try
            Dim adapter As New SqlDataAdapter("select name,id from sysobjects where type='u' and name!='sysdiagrams'", _connection)
            Dim dt As New DataTable
            adapter.Fill(dt)

            Me.lstTables.DisplayMember = "name"
            Me.lstTables.ValueMember = "name"
            Me.lstTables.DataSource = dt

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try


    End Sub

    Private Sub lstTables_SelectedValueChanged(sender As Object, e As EventArgs) Handles lstTables.SelectedValueChanged
        If lstTables.SelectedValue Is Nothing Then
            Exit Sub
        End If

        Try
            Dim adapter As New SqlDataAdapter("select * from INFORMATION_SCHEMA.COLUMNS where table_name='" + lstTables.SelectedValue + "'", _connection)
            Dim dt As New DataTable
            adapter.Fill(dt)

            Me.lstColumns.DisplayMember = "column_name"
            Me.lstColumns.DataSource = dt

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

    End Sub
End Class
