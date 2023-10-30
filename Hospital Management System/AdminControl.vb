Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.DataTable
Public Class AdminControl
    Dim con As OleDb.OleDbConnection
    Dim cmd, cmd2 As OleDb.OleDbCommand
    Dim cmdbld As OleDb.OleDbCommandBuilder
    Dim da1, da2, da3, da4, da5 As OleDb.OleDbDataAdapter
    Dim ds As DataSet
    Dim dv As DataView
    Dim dt As New DataTable
    Dim crmgr1, crmgr2, crmgr3, crmgr4, crmgr5 As CurrencyManager
    Dim total As Integer

    Private Sub AdminControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label13.Text = Date.Now
        con = New OleDb.OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\admin\Desktop\VPM\VS2010 Projects\MP\Outpatient_Hospital_Facility.accdb")
        con.Open()

        da1 = New OleDb.OleDbDataAdapter("SELECT * FROM patients", con)
        ds = New DataSet
        da1.Fill(ds, "patients")
        cmdbld = New OleDb.OleDbCommandBuilder(da1)
        dv = New DataView(ds.Tables(0))
        DataGridView1.DataSource = dv
        crmgr1 = CType(Me.BindingContext(dv), CurrencyManager)
        TextBox1.DataBindings.Add("text", dv, "patient_ID")
        TextBox2.DataBindings.Add("text", dv, "patient_name")

        da2 = New OleDb.OleDbDataAdapter("SELECT * FROM doctors", con)
        ds = New DataSet
        da2.Fill(ds, "doctors")
        cmdbld = New OleDb.OleDbCommandBuilder(da2)
        dv = New DataView(ds.Tables(0))
        DataGridView2.DataSource = dv
        crmgr2 = CType(Me.BindingContext(dv), CurrencyManager)
        TextBox3.DataBindings.Add("text", dv, "doctor_ID")
        TextBox4.DataBindings.Add("text", dv, "doctor_name")

        da3 = New OleDb.OleDbDataAdapter("SELECT * FROM department", con)
        ds = New DataSet
        da3.Fill(ds, "department")
        cmdbld = New OleDb.OleDbCommandBuilder(da3)
        dv = New DataView(ds.Tables(0))
        DataGridView3.DataSource = dv
        crmgr3 = CType(Me.BindingContext(dv), CurrencyManager)

        da4 = New OleDb.OleDbDataAdapter("SELECT * FROM slots", con)
        ds = New DataSet
        da4.Fill(ds, "slots")
        cmdbld = New OleDb.OleDbCommandBuilder(da4)
        dv = New DataView(ds.Tables(0))
        DataGridView4.DataSource = dv
        crmgr4 = CType(Me.BindingContext(dv), CurrencyManager)


        da5 = New OleDb.OleDbDataAdapter("SELECT * FROM bill", con)
        ds = New DataSet
        da5.Fill(ds, "bill")
        cmdbld = New OleDb.OleDbCommandBuilder(da5)
        dv = New DataView(ds.Tables(0))
        DataGridView5.DataSource = dv
        crmgr5 = CType(Me.BindingContext(dv), CurrencyManager)
        TextBox5.DataBindings.Add("text", dv, "bill_ID")
        TextBox6.DataBindings.Add("text", dv, "patient_ID")
        TextBox7.DataBindings.Add("text", dv, "doctor_ID")
        TextBox9.DataBindings.Add("text", dv, "doctor_charges")
        TextBox10.DataBindings.Add("text", dv, "test_and_lab_charges")
        TextBox11.DataBindings.Add("text", dv, "medicine_charges")

        con.Close()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        total = Val(TextBox9.Text) + Val(TextBox10.Text) + Val(TextBox11.Text)
        Label12.Text = total
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        crmgr1.Position = crmgr1.Position - 1
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        crmgr1.Position = crmgr1.Position + 1
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        If DataGridView1.SelectedRows.Count > 0 Then
            For i As Integer = DataGridView1.SelectedRows.Count - 1 To 0 Step -1
                DataGridView1.Rows.RemoveAt(DataGridView1.SelectedRows(i).Index)
            Next
        Else
            MessageBox.Show("please select rows to delete")
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        RegistrationForm.Show()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        Try
            con.Open()
            da1 = New OleDb.OleDbDataAdapter("SELECT * FROM patients where patient_ID = '" & TextBox1.Text & "'", con)
            da1.Fill(dt)
            DataGridView1.DataSource = dt
            TextBox2.Text = ""

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)

        End Try
        con.Close()

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Me.Hide()
        selectUser.Show()
    End Sub

    
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim i As Integer = e.RowIndex
        Dim j As Integer = e.ColumnIndex
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox1.Text = DataGridView1.Rows(i).Cells(j).Value
        TextBox2.Text = DataGridView1.Rows(i).Cells(j + 3).Value

    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        TextBox1.DataBindings.Clear()
        TextBox2.DataBindings.Clear()
        da1 = New OleDb.OleDbDataAdapter("SELECT * FROM patients", con)
        ds = New DataSet
        da1.Fill(ds, "patients")
        cmdbld = New OleDb.OleDbCommandBuilder(da1)
        dv = New DataView(ds.Tables(0))
        DataGridView1.DataSource = dv
        crmgr1 = CType(Me.BindingContext(dv), CurrencyManager)
        TextBox1.DataBindings.Add("text", dv, "patient_ID")
        TextBox2.DataBindings.Add("text", dv, "patient_name")
        con.Close()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            con.Open()
            da2 = New OleDb.OleDbDataAdapter("SELECT * FROM doctors where doctor_ID = '" & TextBox3.Text & "'", con)
            da2.Fill(dt)
            DataGridView2.DataSource = dt
            TextBox4.Text = ""

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)

        End Try
        con.Close()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        TextBox3.DataBindings.Clear()
        TextBox4.DataBindings.Clear()
        da2 = New OleDb.OleDbDataAdapter("SELECT * FROM doctors", con)
        ds = New DataSet
        da2.Fill(ds, "doctors")
        cmdbld = New OleDb.OleDbCommandBuilder(da2)
        dv = New DataView(ds.Tables(0))
        DataGridView2.DataSource = dv
        crmgr2 = CType(Me.BindingContext(dv), CurrencyManager)
        TextBox3.DataBindings.Add("text", dv, "doctor_ID")
        TextBox4.DataBindings.Add("text", dv, "doctor_name")
        con.Close()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        crmgr2.Position = crmgr2.Position - 1
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        crmgr2.Position = crmgr2.Position + 1
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        crmgr5.Position = crmgr5.Position - 1
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        crmgr5.Position = crmgr5.Position + 1
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        cmd2 = New OleDbCommand("DELETE * FROM patients WHERE patient_ID = '" & TextBox1.Text & "'", con)
        con.Open()
        cmd2.ExecuteNonQuery()
        MsgBox("Record for " & TextBox1.Text & " deleted")
        TextBox1.DataBindings.Clear()
        TextBox2.DataBindings.Clear()
        da1 = New OleDb.OleDbDataAdapter("SELECT * FROM patients", con)
        ds = New DataSet
        da1.Fill(ds, "patients")
        cmdbld = New OleDb.OleDbCommandBuilder(da1)
        dv = New DataView(ds.Tables(0))
        DataGridView1.DataSource = dv
        crmgr1 = CType(Me.BindingContext(dv), CurrencyManager)
        TextBox1.DataBindings.Add("text", dv, "patient_ID")
        TextBox2.DataBindings.Add("text", dv, "patient_name")
        con.Close()
    End Sub
End Class