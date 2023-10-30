Imports System.Data.DataTable
Imports System.Data
Imports System.Data.OleDb

Public Class EditProfile
    Dim cmdbld As OleDb.OleDbCommandBuilder
    Dim da As OleDb.OleDbDataAdapter
    Dim ds As DataSet
    Dim dv As DataView
    Dim crmgr As CurrencyManager
    Dim t As New DataTable
    Dim con As OleDb.OleDbConnection
    Dim cmd As OleDb.OleDbCommand

    Private Sub EditProfile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        con = New OleDb.OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\admin\Desktop\VPM\VS2010 Projects\MP\Outpatient_Hospital_Facility.accdb")

        con.Open()

        TextBox10.DataBindings.Clear()
        TextBox1.DataBindings.Clear()
        TextBox2.DataBindings.Clear()
        TextBox3.DataBindings.Clear()
        TextBox5.DataBindings.Clear()
        TextBox6.DataBindings.Clear()
        TextBox7.DataBindings.Clear()
        ComboBox4.DataBindings.Clear()

        Dim da1 As New OleDb.OleDbDataAdapter("SELECT * FROM patients where patient_no = " & Val(Label20.Text) & "", con)
        ds = New DataSet
        da1.Fill(ds)
        cmdbld = New OleDb.OleDbCommandBuilder(da1)
        dv = New DataView(ds.Tables(0))

        crmgr = CType(Me.BindingContext(dv), CurrencyManager)
        TextBox10.DataBindings.Add("text", dv, "patient_ID")
        TextBox1.DataBindings.Add("text", dv, "patient_name")
        TextBox2.DataBindings.Add("text", dv, "age")
        TextBox5.DataBindings.Add("text", dv, "address")
        TextBox3.DataBindings.Add("text", dv, "contact_no")
        TextBox6.DataBindings.Add("text", dv, "email_ID")
        TextBox7.DataBindings.Add("text", dv, "DOB")
        ComboBox4.DataBindings.Add("text", dv, "sex")

        con.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        con.Open()
        cmd = New OleDb.OleDbCommand("UPDATE patients SET patient_name = '" & TextBox1.Text & "', age = '" & TextBox2.Text & "',sex = '" & ComboBox4.Text & "', contact_no = '" & TextBox3.Text & "', address = '" & TextBox5.Text & "', email_ID = '" & TextBox6.Text & "' WHERE patient_ID = '" & TextBox10.Text & "' ", con)
        cmd.ExecuteNonQuery()
        MsgBox("Record for Reg. No.: " & Label20.Text & " updated successfully. Log In again to verify the changes.")
        con.Close()

        Me.Close()
        PatientLogIn.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        MsgBox("Cancelled")
        Form2.Show()
    End Sub
End Class