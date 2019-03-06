Option Strict On
Public Class Car

    Private Shared carCount As Integer = 0
    Private carIdentificationNumber As Integer = 0
    Private carMake As String = String.Empty
    Private carModel As String = String.Empty
    Private carYear As String = String.Empty
    Private carPrice As Double = 0.0
    Private carNewStatus As Boolean = False

#Region "Constructors"
    Public Sub New()
        carCount += 1
        carIdentificationNumber = carCount
    End Sub

    Public Sub New(make As String, model As String, year As String, price As Double, status As Boolean)
        Me.New()
        carMake = make
        carModel = model
        carYear = year
        carPrice = Math.Round(price, 2)
        carNewStatus = status
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property Count() As Integer
        Get
            Return carCount
        End Get
    End Property

    Public ReadOnly Property IdentificationNumber() As Integer
        Get
            Return carIdentificationNumber
        End Get
    End Property

    Public Property Make() As String
        Get
            Return carMake
        End Get
        Set(value As String)
            carMake = value
        End Set
    End Property

    Public Property Model() As String
        Get
            Return carModel
        End Get
        Set(value As String)
            carModel = value
        End Set
    End Property

    Public Property Year() As String
        Get
            Return carYear
        End Get
        Set(value As String)
            carYear = value
        End Set
    End Property

    Public Property Price() As Double
        Get
            Return carPrice
        End Get
        Set(value As Double)
            carPrice = Math.Round(value, 2)
        End Set
    End Property

    Public Property NewStatus() As Boolean
        Get
            Return carNewStatus
        End Get
        Set(value As Boolean)
            carNewStatus = value
        End Set
    End Property
#End Region

#Region "Methods"
    Public Function GetCarData() As String
        Return "The car's ID is " & carIdentificationNumber.ToString() & ". The car's make is " & carMake & ". The car's model is " & carModel & ". The car's year is " & carYear & ". The car's price is " & carPrice.ToString("c") & ". The car is " & If(carNewStatus = True, "new.", "used.").ToString()
    End Function
#End Region

End Class
