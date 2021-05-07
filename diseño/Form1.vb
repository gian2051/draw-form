Public Class Form1

    Private controlActual As Control = Nothing
    Private mouseInicial As Point
    Private ubicaInicial As Point

#Region "Eventos de la plantilla de contorles"

    Private Sub presionarClic(sender As Object, e As MouseEventArgs)
        'al hacer clic sin soltar el clic izquierdo
        If e.Button = MouseButtons.Left Then
            controlActual = sender 'contenedor.Controls.Find(sender.ToString, False)(0)
            ubicaInicial = controlActual.Location
            mouseInicial = e.Location
        End If
    End Sub

    Private Sub moverMouse(sender As Object, e As MouseEventArgs)
        'al mover el mouse, solo se activa si se mantiene presionado el clic izquierdo
        If controlActual IsNot Nothing Then
            Dim puntoControl As Point = controlActual.Location 'ubicacion del control
            Dim puntoCursor As Point = e.Location 'donde esta el 

            Dim nuevaUbicacion As Point
            nuevaUbicacion.X = puntoControl.X + puntoCursor.X - mouseInicial.X
            nuevaUbicacion.Y = puntoControl.Y + puntoCursor.Y - mouseInicial.Y

            controlActual.Location = nuevaUbicacion
            lblControl.Text = controlActual.Location.ToString

            If contenidoEnDiseñador() Then
                controlActual.BringToFront()
            End If
        End If

    End Sub

    Private Sub soltarMouse(sender As Object, e As MouseEventArgs)
        'se ejecuta cuando se suelta el boton del mouse
        If controlActual Is Nothing Then
            Exit Sub
        End If

        Dim nuevaUbica As Point = controlActual.Location
        nuevaUbica.X -= SplitContainer1.Panel1.Size.Width
        Dim mm As Object = controlActual.GetType()
        Dim coordenada As Point = controlActual.Location
        If contenidoEnDiseñador() Then
            crear(controlActual)
            Dim nuevo As Control = crear(controlActual)
            nuevo.Location = nuevaUbica
            SplitContainer1.Panel2.Controls.Add(nuevo)
            controles.Add(nuevo)
        End If

        controlActual.Location = ubicaInicial
        controlActual = Nothing

        lblControl.Text = coordenada.ToString
    End Sub

    Private Function crear(ByVal original As Control) As Control
        Dim nuevo As Control
        Select Case TypeName(controlActual)
            Case "TextBox"
                nuevo = New TextBox
            Case "LinkLabel"
                nuevo = New LinkLabel
            Case "CheckBox"
                nuevo = New CheckBox
            Case "RadioButton"
                nuevo = New RadioButton
            Case "Button"
                nuevo = New Button
            Case Else
                nuevo = New Label
        End Select

        nuevo.Name = "dibuja" & controles.Count + 1
        nuevo.Size = original.Size
        nuevo.ForeColor = original.ForeColor
        nuevo.BackColor = original.BackColor
        nuevo.Text = original.Text

        Return nuevo
    End Function

#End Region

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        contenedor = SplitContainer1.Panel2
        corregirX = SplitContainer1.Panel1.Size.Width

        AddHandler Label1.MouseDown, AddressOf presionarClic
        AddHandler Label1.MouseMove, AddressOf moverMouse
        AddHandler Label1.MouseUp, AddressOf soltarMouse

        AddHandler LinkLabel1.MouseDown, AddressOf presionarClic
        AddHandler LinkLabel1.MouseMove, AddressOf moverMouse
        AddHandler LinkLabel1.MouseUp, AddressOf soltarMouse

        AddHandler CheckBox1.MouseDown, AddressOf presionarClic
        AddHandler CheckBox1.MouseMove, AddressOf moverMouse
        AddHandler CheckBox1.MouseUp, AddressOf soltarMouse

        AddHandler RadioButton1.MouseDown, AddressOf presionarClic
        AddHandler RadioButton1.MouseMove, AddressOf moverMouse
        AddHandler RadioButton1.MouseUp, AddressOf soltarMouse

        AddHandler TextBox1.MouseDown, AddressOf presionarClic
        AddHandler TextBox1.MouseMove, AddressOf moverMouse
        AddHandler TextBox1.MouseUp, AddressOf soltarMouse

        AddHandler Button1.MouseDown, AddressOf presionarClic
        AddHandler Button1.MouseMove, AddressOf moverMouse
        AddHandler Button1.MouseUp, AddressOf soltarMouse
    End Sub

    'siempre que los controles esten a la izquierda y el diseño del form a la derecha
    Private controles As New List(Of Control)
    Private contenedor As Control
    Private corregirX As Integer

    Private Function contenidoEnDiseñador()
        If controlActual Is Nothing Then
            Return False
        End If

        Dim ini As Point = contenedor.Location
        Dim fin As Point = contenedor.Size
        fin.X += ini.X
        fin.Y += ini.Y

        Dim coordenada As Point = controlActual.Location
        If coordenada.X >= ini.X AndAlso coordenada.X <= fin.X Then
            If coordenada.Y <= fin.Y AndAlso coordenada.Y >= ini.Y Then
                Return True
            End If
        End If

        Return False
    End Function

End Class

Class prueba
    'MouseCaptureChanged= "mouse capture changed"
    'MouseClick= "mouse clic"
    'MouseDoubleClick= "mouse doble clic"
    'MouseEnter= "cursor entra al control"
    'MouseHover= "cursor entra al control"
    'MouseLeave= "cursosr sale del control"
End Class