Imports System.Data
Imports System.Data.OleDb

Public Class RegistrationForm
    Dim con As New OleDb.OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\admin\Desktop\VPM\VS2010 Projects\MP\Outpatient_Hospital_Facility.accdb")
    Dim cmd As OleDb.OleDbCommand
    Dim cmdbld As OleDb.OleDbCommandBuilder
    Dim da1, da2 As New OleDb.OleDbDataAdapter
    Dim ds As New DataSet
    Dim dbcmd As String


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Or TextBox8.Text = "" Or TextBox9.Text = "" Or TextBox10.Text = "" Then
            MessageBox.Show("Please enter all fields to confirm registration", "Registration Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
        Else
            Dim da1 As New OleDb.OleDbDataAdapter("SELECT * FROM patients where patient_name = '" & TextBox1.Text & "'", con)
            Dim ds1 As New DataSet()
            da1.Fill(ds1)
            If ds1.Tables(0).Rows.Count <> 0 Then
                MessageBox.Show("This patient has already registered.")
            Else
                Label15.Text = ComboBox1.Text & "/" & ComboBox2.Text & "/" & ComboBox3.Text
                Dim dbcmd As String = "INSERT INTO patients(patient_ID, patient_name, age, sex, DOB, contact_no, address, email_ID, slot_ID, disease, pin)" & "VALUES('" & TextBox10.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & ComboBox4.Text & "', '" & Label15.Text & "','" & TextBox3.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "','" & TextBox8.Text & "','" & TextBox9.Text & "')"
                Dim da As New OleDb.OleDbDataAdapter(dbcmd, con)
                Dim ds As New DataSet()
                Try
                    da.Fill(ds)
                    MessageBox.Show("Patient Registered successfully. ")
                    MsgBox("Hello " & TextBox1.Text & ". Your Registration Number is " & TextBox4.Text & " and your pin is " & TextBox9.Text & ". You may now Log In.")
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Information)
                End Try

            End If
        End If
        con.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        con.Open()
        Try
            cmd = New OleDbCommand("DELETE * FROM patients WHERE [patient_no] = @no", con)
            cmd.Parameters.AddWithValue("@no", TextBox4.Text)
            da1 = New OleDbDataAdapter(cmd)
            Dim dt As New DataTable
            da1.Fill(dt)
            cmd.ExecuteNonQuery()
            con.Close()
            MsgBox("Registration for Reg. No. " & TextBox4.Text & " Cancelled.")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Me.Close()
    End Sub

    Private Sub RegistrationForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label22.Text = Date.Now
        con = New OleDb.OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\admin\Desktop\VPM\VS2010 Projects\MP\Outpatient_Hospital_Facility.accdb")
        con.Open()

        Try
            cmd = New OleDb.OleDbCommand("SELECT * FROM patients ORDER BY patient_no DESC ", con)
            Dim dr As OleDbDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.Read = True Then
                TextBox4.Text = Val(dr(0)) + 1
            Else
                TextBox4.Text = 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Label20.Text = TextBox4.Text
        cmd.Dispose()
        con.Close()
    End Sub
End Class