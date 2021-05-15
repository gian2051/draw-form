Public Class Form1
    Private controlActual As Control = Nothing

    Private inicioEnDiseñador As Boolean = False
    Private mouseInicial As Point
    Private ubicaInicial As Point

#Region "Eventos de Movimiento o desplazamiento"

    Private Sub presionarClic(sender As Object, e As MouseEventArgs)
        'al hacer clic sin soltar el clic izquierdo
        If e.Button = MouseButtons.Left Then
            controlActual = sender 'contenedor.Controls.Find(sender.ToString, False)(0)

            inicioEnDiseñador = SplitContainer1.Panel2.Contains(controlActual)
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

            controlActual.BringToFront()
        End If

    End Sub

    Private Sub soltarMouse(sender As Object, e As MouseEventArgs)
        'se ejecuta cuando se suelta el boton del mouse
        If controlActual Is Nothing Or e.Button <> MouseButtons.Left Then
            Exit Sub
        End If

        If contenidoEnDiseñador(controlActual.Location, SplitContainer1.Panel2, inicioEnDiseñador) Then 'si actualmente se encuentra en el diseñador
            If Not inicioEnDiseñador Then
                Dim nuevo As Control = crear(controlActual)
                nuevo.Location = New Point(controlActual.Location.X - SplitContainer1.Panel1.Size.Width, controlActual.Location.Y)
                SplitContainer1.Panel2.Controls.Add(nuevo)
                controlActual.Location = ubicaInicial
            End If
        Else
            controlActual.Location = ubicaInicial
        End If

        controlActual = Nothing
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
        nuevo.ContextMenuStrip = ContextMenuStrip1

        AddHandler nuevo.MouseDown, AddressOf presionarClic
        AddHandler nuevo.MouseMove, AddressOf moverMouse
        AddHandler nuevo.MouseUp, AddressOf soltarMouse

        Return nuevo
    End Function

#End Region

#Region "Eventos de los controles diseñados"

    Private Sub diseño_ClicDerecho(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            controlActual = sender 'contenedor.Controls.Find(sender.ToString, False)(0)
            ubicaInicial = controlActual.Location
            mouseInicial = e.Location
        End If
    End Sub

#End Region
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
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
    Private corregirX As Integer

    Private Function contenidoEnDiseñador(ByVal aEvaluar As Point, ByVal contenedor As Control, ByVal ubicacionCero As Boolean) As Boolean
        Dim ini As Point = contenedor.Location
        Dim fin As Point = contenedor.Size

        If ubicacionCero Then
            ini = New Point(0, 0)
        Else
            fin.X += ini.X
            fin.Y += ini.Y
        End If

        If aEvaluar.X >= ini.X AndAlso aEvaluar.X <= fin.X Then
            If aEvaluar.Y <= fin.Y AndAlso aEvaluar.Y >= ini.Y Then
                Return True
            End If
        End If

        Return False
    End Function

    Private Sub PropiedadesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PropiedadesToolStripMenuItem.Click
        Dim ee As Control = ContextMenuStrip1.SourceControl
    End Sub

End Class

Class prueba
    'MouseCaptureChanged= "mouse capture changed"
    'MouseClick= "mouse clic"
    'MouseDoubleClick= "mouse doble clic"
    'MouseEnter= "cursor entra al control"
    'MouseHover= "cursor entra al control"
    'MouseLeave= "cursosr sale del control"
End Class