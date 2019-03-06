Option Strict On

' Author:       Anastasiia Kononirenko
' Student ID:   100717670
' Date:         06/03/2019
' Purpose:      Lab_4_Car_Inventory
' Description:  A windows forms application to keep a list of cars and information about them.

Public Class frmCarInventory

#Region "Declarations"
    Private carList As New SortedList   ' collection of Car objects
    Private selectedCarIdentificationNumber As String = String.Empty   ' current selected car's id
    Private editMode As Boolean = False ' a variable to identify whether the edit mode is on or off
#End Region

#Region "Event Handlers"
    ''' <summary>
    ''' Click event of the btnEnter button that validates user input. If it's valid,
    ''' a Car object will be either created or updated. Then user input will be displayed 
    ''' in the ListView.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click

        Dim car As Car  ' declare a Car class
        ' validate the user input
        If IsValidInput() Then
            editMode = True    ' turn the edit mode on
            ' if the selected car identification number has no value, then it's a new item 
            If (selectedCarIdentificationNumber.Trim.Length = 0) Then
                ' create a new car object using a parameterized constructor
                car = New Car(cmbMake.Text, txtModel.Text.Trim, cmbYear.Text, Convert.ToDouble(txtPrice.Text.Trim), chkNew.Checked)
                ' add the car to the carList ]collection
                carList.Add(car.IdentificationNumber.ToString(), car)
                ' if the selected car identification number has a value, then it's an existing item
            Else
                car = CType(carList.Item(selectedCarIdentificationNumber), Car)  ' get the car from the collection
                car.Make() = cmbMake.Text        ' update the car's make
                car.Model() = txtModel.Text.Trim ' update the car's model
                car.Year() = cmbYear.Text        ' update the car's year
                car.Price() = Convert.ToDouble(txtPrice.Text.Trim)  ' update the car's price
                car.NewStatus() = chkNew.Checked ' update the car's status
            End If

            lvwCars.Items.Clear()   ' clear the items from a ListView control

            ' loop through the carList collection and populate the ListView
            For Each carEntry As DictionaryEntry In carList

                Dim carItem As New ListViewItem     ' declare a ListViewItem class
                car = CType(carEntry.Value, Car)    ' get the car from the list

                carItem.Checked = car.NewStatus()   ' assign the value checked control
                ' assign values to the subitems
                carItem.SubItems.Add(car.IdentificationNumber.ToString())
                carItem.SubItems.Add(car.Make)
                carItem.SubItems.Add(car.Model)
                carItem.SubItems.Add(car.Year)
                carItem.SubItems.Add(car.Price().ToString("c"))

                lvwCars.Items.Add(carItem)  ' add the ListViewItem to the ListView control
            Next

            Reset() ' reset the form

        End If
    End Sub

    ''' <summary>
    ''' lvwCars_SelectedIndexChanged - when a row in the ListView control is selected, it will populate the fields for editing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub lvwCars_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwCars.SelectedIndexChanged

        ' set the BackColor of each row to White
        For Each carItem As ListViewItem In Me.lvwCars.Items
            carItem.BackColor = Color.White
        Next

        Const subItemIndex As Integer = 1   ' constant that keeps the index of the subitem that hold the car id

        selectedCarIdentificationNumber = lvwCars.Items(lvwCars.FocusedItem.Index).SubItems(subItemIndex).Text  ' get the car's id

        Dim car As Car = CType(carList.Item(selectedCarIdentificationNumber), Car)  ' get the car from the collection using the car's id

        cmbMake.Text = car.Make()   ' get the car's make and set the combo box
        txtModel.Text = car.Model() ' get the car's model and set the text box
        cmbYear.Text = car.Year()   ' get the car's year and set the combo box
        txtPrice.Text = car.Price().ToString()  ' get the car's price and set the text box
        chkNew.Checked = car.NewStatus()   ' get the car's status and set the check box

        Me.lvwCars.FocusedItem.BackColor = Color.LightBlue  ' set the BackColor of the focused item to LightBlue
        Me.lvwCars.FocusedItem.Selected = False     ' set the Selected property of the focused item to False

    End Sub

    ''' <summary>
    ''' Click event of the Reset button that resets the form using the subroutine
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    ''' <summary>
    ''' Click event of the Exit button that exits the form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' lvwCars_ItemCheck - used to prevent the user from checking the check box in the list view if it is not in edit mode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub lvwCars_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lvwCars.ItemCheck
        If (editMode = False) Then
            e.NewValue = e.CurrentValue
        End If
    End Sub

    ''' <summary>
    ''' Click event of the frmCarInventory that resets the form if the item in the list view is not focused
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmCarInventory_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        ' if the item in the list view is not focused, reset the form
        If (selectedCarIdentificationNumber <> String.Empty) Then
            Reset()
        End If
    End Sub

#End Region

#Region "Functions/Methods"
    ''' <summary>
    ''' IsValidInput - validates the data in each control to ensure that the user has entered appropriate values
    ''' </summary>
    ''' <returns></returns>
    Private Function IsValidInput() As Boolean

        Dim returnValue As Boolean = True   ' a function return value
        Dim errorMessage As String = String.Empty   ' holds an error message for the user
        Dim price As Double ' holds the price of the car

        ' check if the car make has been selected
        If (cmbMake.SelectedIndex = -1) Then
            errorMessage += "Please select the car's make" & vbCrLf ' if not set the error message
        End If

        ' check if the model has been entered
        If (txtModel.Text.Trim.Length = 0) Then
            errorMessage += "Please enter the car's model" & vbCrLf ' If not set the error message
        End If

        ' check if the year has been selected
        If (cmbYear.SelectedIndex = -1) Then
            errorMessage += "Please select the car's year" & vbCrLf ' If not set the error message
        End If

        ' check if the price has been entered
        If (txtPrice.Text.Trim.Length = 0) Then
            errorMessage += "Please enter the price of the car" & vbCrLf ' If not set the error message
        Else
            ' check if user input for the price is a real number
            If (Double.TryParse(txtPrice.Text.Trim, price) = False OrElse price < 0.0) Then
                txtPrice.Clear()   ' clear the text box 
                errorMessage += "Please enter a real positive number for the car's price" ' If not set the error message
            End If
        End If

        ' if there are some errors, then
        If (errorMessage <> String.Empty) Then
            returnValue = False   ' set the value of returnValue to False
            lblError.Text = "ERROR(s)" & vbCrLf & errorMessage  ' display the error message in the lblError
        End If

        Return returnValue  ' return the boolean value (True - if there no errors; False - if there are some errors)

    End Function

    ''' <summary>
    ''' Resets the form to its initial state
    ''' </summary>
    Private Sub Reset()

        cmbMake.SelectedIndex = -1     ' set the index of the cmbMake combo box to -1
        txtModel.Text = String.Empty   ' set the Text of the txtModel text box to an empty string
        cmbYear.SelectedIndex = -1     ' set the index of the cmbYear combo box to -1
        txtPrice.Text = String.Empty   ' set the Text of the txtPrice text box to an empty string
        chkNew.Checked = False         ' uncheck the chkNew check box
        lblError.Text = String.Empty   ' set the Text of the lblError label to an empty string
        cmbMake.Select()               ' set the Focus to the cmbMale combo box
        editMode = False               ' turn the edit mode off

        ' set the BackColor of each row of the ListView control to White
        For Each carItem As ListViewItem In Me.lvwCars.Items
            carItem.BackColor = Color.White
        Next

        selectedCarIdentificationNumber = String.Empty  ' set the current selected car's id to an empty string
    End Sub

#End Region

End Class
