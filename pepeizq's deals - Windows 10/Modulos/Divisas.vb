﻿Imports Microsoft.Toolkit.Uwp.Helpers
Imports Newtonsoft.Json
Imports Windows.Storage

Module Divisas

    Dim dolar, libra As Moneda
    Dim buscarDolar, buscarLibra As Boolean
    Dim WithEvents bw As New BackgroundWorker

    Public Async Sub Generar()

        buscarDolar = False
        buscarLibra = False

        Dim helper As New LocalObjectStorageHelper
        Dim config As ApplicationDataContainer = ApplicationData.Current.LocalSettings

        If Await helper.FileExistsAsync("monedas") Then
            Dim monedas As Monedas = Await helper.ReadFileAsync(Of Monedas)("monedas")

            If monedas Is Nothing Then
                buscarDolar = True
                buscarLibra = True
            Else
                If Not monedas.Dolar Is Nothing Then
                    If Not monedas.Dolar.Valor Is Nothing Then
                        config.Values("dolar") = monedas.Dolar.Valor
                        dolar = New Moneda(monedas.Dolar.Valor, monedas.Dolar.Fecha)

                        If monedas.Dolar.Fecha = FechaHoy() Then
                            buscarDolar = False
                        Else
                            buscarDolar = True
                        End If
                    Else
                        buscarDolar = True
                    End If
                Else
                    buscarDolar = True
                End If

                If Not monedas.Libra Is Nothing Then
                    If Not monedas.Libra.Valor Is Nothing Then
                        config.Values("libra") = monedas.Libra.Valor
                        libra = New Moneda(monedas.Libra.Valor, monedas.Libra.Fecha)

                        If monedas.Libra.Fecha = FechaHoy() Then
                            buscarLibra = False
                        Else
                            buscarLibra = True
                        End If
                    Else
                        buscarLibra = True
                    End If
                Else
                    buscarLibra = True
                End If
            End If
        Else
            buscarDolar = True
            buscarLibra = True
        End If

        If bw.IsBusy = False Then
            bw.RunWorkerAsync()
        End If

    End Sub

    Private Sub Bw_DoWork(sender As Object, e As DoWorkEventArgs) Handles bw.DoWork

        If buscarDolar = True Then
            Dim html_ As Task(Of String) = Decompiladores.HttpClient(New Uri("https://free.currconv.com/api/v7/convert?q=EUR_USD&compact=ultra&apiKey=9e979a16c42f161c4724"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim cambioDolar As CambioDolar = JsonConvert.DeserializeObject(Of CambioDolar)(html)

                If Not cambioDolar Is Nothing Then
                    dolar = New Moneda(cambioDolar.Dolar, FechaHoy)
                End If
            End If
        End If

        If buscarLibra = True Then
            Dim html_ As Task(Of String) = Decompiladores.HttpClient(New Uri("https://free.currconv.com/api/v7/convert?q=EUR_GBP&compact=ultra&apiKey=9e979a16c42f161c4724"))
            Dim html As String = html_.Result

            If Not html = Nothing Then
                Dim cambioLibra As CambioLibra = JsonConvert.DeserializeObject(Of CambioLibra)(html)

                If Not cambioLibra Is Nothing Then
                    libra = New Moneda(cambioLibra.Libra, FechaHoy)
                End If
            End If
        End If

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bw.RunWorkerCompleted

        Dim helper As New LocalObjectStorageHelper
        Dim config As ApplicationDataContainer = ApplicationData.Current.LocalSettings

        Dim monedas As Monedas = Nothing

        If Await helper.FileExistsAsync("monedas") Then
            monedas = Await helper.ReadFileAsync(Of Monedas)("monedas")

            If Not monedas Is Nothing Then
                If Not dolar Is Nothing Then
                    monedas.Dolar = dolar
                End If

                If Not libra Is Nothing Then
                    monedas.Libra = libra
                End If
            End If
        Else
            monedas = New Monedas(dolar, libra)
        End If

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        If Not monedas Is Nothing Then
            If Not monedas.Dolar Is Nothing Then
                config.Values("dolar") = monedas.Dolar.Valor
            End If

            If Not monedas.Libra Is Nothing Then
                config.Values("libra") = monedas.Libra.Valor
            End If

            Await helper.SaveFileAsync(Of Monedas)("monedas", monedas)
        End If

    End Sub

    Public Function CambioMoneda(precio As String, moneda As String) As String

        Dim temporalEuros As String = Nothing

        If Not moneda = Nothing Then
            If Not precio = Nothing Then
                If moneda.Length > 0 And precio.Length > 0 Then
                    precio = precio.Replace("₽", Nothing)
                    moneda = moneda.Replace("$", Nothing)
                    moneda = moneda.Replace("£", Nothing)
                    moneda = moneda.Replace(".", ",")
                    moneda = moneda.Trim

                    precio = precio.Replace("₽", Nothing)
                    precio = precio.Replace("$", Nothing)
                    precio = precio.Replace("£", Nothing)
                    precio = precio.Replace(".", ",")
                    precio = precio.Trim

                    Dim dou, dou2, resultado As Double

                    If moneda.Length > 0 And precio.Length > 0 Then
                        dou = CDbl(moneda)

                        Try
                            dou2 = CDbl(precio)
                        Catch ex As Exception

                        End Try

                        resultado = dou2 / dou

                        temporalEuros = Math.Round(resultado, 2).ToString

                        If Not temporalEuros.Contains(",") Then
                            temporalEuros = temporalEuros + ",00"
                        End If

                        temporalEuros = temporalEuros + " €"
                    End If
                End If
            End If
        End If

        Return temporalEuros
    End Function

    Private Function FechaHoy()
        Dim fecha As String = Date.Now.Day.ToString + "/" + Date.Now.Month.ToString + "/" + Date.Now.Year.ToString
        Return fecha
    End Function

End Module

Public Class Monedas

    Public Dolar As Moneda
    Public Libra As Moneda

    Public Sub New(ByVal dolar As Moneda, ByVal libra As Moneda)
        Me.Dolar = dolar
        Me.Libra = libra
    End Sub

End Class

Public Class Moneda

    Public Valor As String
    Public Fecha As String

    Public Sub New(ByVal valor As String, ByVal fecha As String)
        Me.Valor = valor
        Me.Fecha = fecha
    End Sub

End Class

Public Class CambioDolar

    <JsonProperty("EUR_USD")>
    Public Dolar As String

End Class

Public Class CambioLibra

    <JsonProperty("EUR_GBP")>
    Public Libra As String

End Class

Public Class CambioRublo

    <JsonProperty("EUR_RUB")>
    Public Rublo As String

End Class
