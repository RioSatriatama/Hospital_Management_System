Imports System.Data
Imports System.Data.OleDb

Public Class OurStaff

    Dim con As OleDb.OleDbConnection
    Dim da2 As New OleDb.OleDbDataAdapter
    Dim ds As DataSet
    Dim cmdbld As OleDb.OleDbCommandBuilder
    Dim dv As DataView
    Dim crmgr As CurrencyManager

    Private Sub OurStaff_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con = New OleDb.OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\admin\Desktop\VPM\VS2010 Projects\MP\Outpatient_Hospital_Facility.accdb")
        con.Open()
        da2 = New OleDb.OleDbDataAdapter("SELECT * FROM doctors", con)
        ds = New DataSet
        da2.Fill(ds, "doctors")
        cmdbld = New OleDb.OleDbCommandBuilder(da2)
        dv = New DataView(ds.Tables(0))
        DataGridView1.DataSource = dv
        crmgr = CType(Me.BindingContext(dv), CurrencyManager)
        con.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
        Form2.Show()
    End Sub
End Class