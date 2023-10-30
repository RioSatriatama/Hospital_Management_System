Public Class adminLogIn

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "admin" And TextBox2.Text = "12345" Then
            MessageBox.Show("You Logged in as adminstrator", "Hospital Management System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Hide()
            AdminControl.Show()
        Else
            MessageBox.Show("Invalid log in credentials", "Log in error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        selectUser.Show()
    End Sub

    Private Sub adminLogIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox2.PasswordChar = "*"c
    End Sub
End Class