Option Strict On
Public Class frmCarInventory

    Private carList As New SortedList
    Private selectedCarIdentificationNumber As String = String.Empty
    Private editMode As Boolean = False

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click

        Dim car As Car

        If IsValidInput() Then
            editMode = True
            If (selectedCarIdentificationNumber.Trim.Length = 0) Then
                car = New Car(cmbMake.Text, txtModel.Text.Trim, cmbYear.Text, Convert.ToDouble(txtPrice.Text.Trim), chkNew.Checked)
                carList.Add(car.IdentificationNumber.ToString(), car)
            Else
                car = CType(carList.Item(selectedCarIdentificationNumber), Car)
                car.Make() = cmbMake.Text
                car.Model() = txtModel.Text.Trim
                car.Year() = cmbYear.Text
                car.Price() = Convert.ToDouble(txtPrice.Text.Trim)
                car.NewStatus() = chkNew.Checked
            End If

            lvwCars.Items.Clear()

            For Each carEntry As DictionaryEntry In carList

                Dim carItem As New ListViewItem
                car = CType(carEntry.Value, Car)

                carItem.Checked = car.NewStatus()
                carItem.SubItems.Add(car.IdentificationNumber.ToString())
                carItem.SubItems.Add(car.Make)
                carItem.SubItems.Add(car.Model)
                carItem.SubItems.Add(car.Year)
                carItem.SubItems.Add(car.Price().ToString("c"))

                lvwCars.Items.Add(carItem)
            Next

            Reset()

        End If
    End Sub

    Private Function IsValidInput() As Boolean

        Dim returnValue As Boolean = True
        Dim errorMessage As String = String.Empty
        Dim price As Double

        If (cmbMake.SelectedIndex = -1) Then
            errorMessage += "Please select the car's make" & vbCrLf
        End If

        If (txtModel.Text.Trim.Length = 0) Then
            errorMessage += "Please enter the car's model" & vbCrLf
        End If

        If (cmbYear.SelectedIndex = -1) Then
            errorMessage += "Please select the car's year" & vbCrLf
        End If

        If (txtPrice.Text.Trim.Length = 0) Then
            errorMessage += "Please enter the price of the car" & vbCrLf
        Else
            If (Double.TryParse(txtPrice.Text.Trim, price) = False OrElse price < 0.0) Then
                txtPrice.Clear()
                errorMessage += "Please enter a real positive number for the car's price"
            End If
        End If

        If (errorMessage <> String.Empty) Then
            returnValue = False
            lblError.Text = "ERROR(s)" & vbCrLf & errorMessage
        End If

        Return returnValue

    End Function

    Private Sub Reset()
        cmbMake.SelectedIndex = -1
        txtModel.Text = String.Empty
        cmbYear.SelectedIndex = -1
        txtPrice.Text = String.Empty
        chkNew.Checked = False
        lblError.Text = String.Empty
        cmbMake.Select()
        editMode = False

        selectedCarIdentificationNumber = String.Empty
    End Sub

    Private Sub lvwCars_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwCars.SelectedIndexChanged

        Const subItemIndex As Integer = 1

        selectedCarIdentificationNumber = lvwCars.Items(lvwCars.FocusedItem.Index).SubItems(subItemIndex).Text

        Dim car As Car = CType(carList.Item(selectedCarIdentificationNumber), Car)

        cmbMake.Text = car.Make()
        txtModel.Text = car.Model()
        cmbYear.Text = car.Year()
        txtPrice.Text = car.Price().ToString()
        chkNew.Checked = car.NewStatus()


    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub lvwCars_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lvwCars.ItemCheck
        If (editMode = False) Then
            e.NewValue = e.CurrentValue
        End If
    End Sub

End Class
