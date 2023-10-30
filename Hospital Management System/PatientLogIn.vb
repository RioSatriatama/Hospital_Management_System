Imports System.Data.DataTable
Imports System.Data
Imports System.Data.OleDb
Public Class PatientLogIn
    Dim cmdbld As OleDb.OleDbCommandBuilder
    Dim da As OleDb.OleDbDataAdapter
    Dim ds As DataSet
    Dim dv As DataView
    Dim crmgr As CurrencyManager
    Dim t As New DataTable

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        RegistrationForm.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim con As OleDb.OleDbConnection
        Dim cmd, cmd1, cmd2 As OleDb.OleDbCommand
        If TextBox1.Text = "" Or TextBox3.Text = "" Then
            MsgBox("enter all fields")

        Else
            con = New OleDb.OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\admin\Desktop\VPM\VS2010 Projects\MP\Outpatient_Hospital_Facility.accdb")

            cmd = New OleDb.OleDbCommand("SELECT * FROM patients where patient_no = @no AND pin = '" & TextBox3.Text & "' ", con)
            cmd.Parameters.AddWithValue("@no", Val(TextBox1.Text))

            cmd1 = New OleDb.OleDbCommand("SELECT * FROM bill WHERE [patient_ID] = (SELECT patient_ID FROM patients WHERE patient_no = @no) ", con)
            cmd1.Parameters.AddWithValue("@no", Val(TextBox1.Text))

            Dim dname, bid As String

            Dim name, pid, emailid, age, sex, contact, sid, dob As String
            Dim addr As String
            con.Open()

            Dim dr As OleDbDataReader = cmd.ExecuteReader()
            Dim dr1 As OleDbDataReader = cmd1.ExecuteReader()

            If dr.Read = False Then
                MessageBox.Show("Invalid log in credentials. If you are new try registering first.", "Log in error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            Else
                name = dr("patient_name")
                pid = dr("patient_ID")
                emailid = dr("email_ID")
                age = dr("age")
                sex = dr("sex")
                contact = dr("contact_no")
                sid = dr("slot_ID")
                addr = dr("address")
                dob = dr("DOB").ToString
                MsgBox("Welcome " & name & " logged in successfully. ")
                Form2.Show()
                Form2.Label5.Text = TextBox1.Text
                EditProfile.Label20.Text = Form2.Label5.Text
                EditProfile.TextBox4.Text = Form2.Label5.Text
                Form2.Label15.Text = pid

                cmd2 = New OleDbCommand("SELECT * FROM doctors WHERE doctor_ID = (SELECT doctor_ID from bill where patient_ID = '" & Form2.Label15.Text & "')", con)
                Dim dr2 As OleDbDataReader = cmd2.ExecuteReader()

                Form2.Label16.Text = name
                Form2.Label17.Text = emailid
                Form2.Label18.Text = age
                Form2.Label19.Text = sex
                Form2.Label20.Text = contact
                Form2.Label23.Text = sid

                With EditProfile
                    .TextBox1.Text = name
                    .TextBox2.Text = age
                    .TextBox5.Text = addr
                    .TextBox3.Text = contact
                    .TextBox6.Text = emailid
                    .ComboBox4.Text = sex
                    .TextBox10.Text = pid
                    .TextBox7.Text = dob
                End With

                If dr1.Read = True And dr2.Read = True Then
                    dname = dr2("doctor_name")
                    bid = dr1("bill_ID")
                    Form2.Label22.Text = bid
                    Form2.Label21.Text = dname
                End If
                con.Close()
                Me.Close()
                End If

        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox3.PasswordChar = ""
        Else
            TextBox3.PasswordChar = "*"c
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        selectUser.Show()
    End Sub

    Private Sub PatientLogIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckBox1.Checked = False
        TextBox3.PasswordChar = "*"c
    End Sub

End Class