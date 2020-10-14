﻿Namespace pepeizq.Editor.pepeizqdeals
    Module Referidos

        Public Function Generar(enlace As String)

            If Not enlace = Nothing Then
                If enlace.Contains("store.steampowered.com") Then
                    Dim referido As String = "?curator_clanid=33500256"

                    If Not enlace.Contains(referido) Then
                        If Not enlace.Contains("?") Then
                            enlace = enlace + referido
                        End If
                    End If
                ElseIf enlace.Contains("gamesplanet.com") Then
                    Dim referido As String = "?ref=pepeizq"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("gamersgate.com") Then
                    Dim referido As String = "?caff=6704538"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("wingamestore.com") Then
                    Dim referido As String = "?ars=pepeizqdeals"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("macgamestore.com") Then
                    Dim referido As String = "?ars=pepeizqdeals"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("amazon.com") Then
                    Dim referido As String = "tag=ofedeunpan-20"

                    If Not enlace.Contains(referido) Then
                        If Not enlace.Contains("?") Then
                            enlace = enlace + "?" + referido
                        Else
                            enlace = enlace + "&" + referido
                        End If
                    End If
                ElseIf enlace.Contains("amazon.es") Then
                    Dim referido As String = "tag=ofedeunpan-21"

                    If Not enlace.Contains(referido) Then
                        If Not enlace.Contains("?") Then
                            enlace = enlace + "?" + referido
                        Else
                            enlace = enlace + "&" + referido
                        End If
                    End If
                ElseIf enlace.Contains("humblebundle.com") Then
                    Dim referido As String = "?partner=pepeizq"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("fanatical.com") Then
                    Dim referido As String = "?ref=pepeizq"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("indiegala.com") Then
                    Dim referido As String = "?ref=pepeizq"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("greenmangaming.com") Then
                    Dim referido As String = "?tap_a=1964-996bbb&tap_s=608263-a851ee"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("voidu.com") Then
                    Dim referido As String = "?ref=e8f2c4e5-81e9"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("yuplay.ru") Then
                    Dim referido As String = "?partner=19b1d908fe49e597"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                ElseIf enlace.Contains("microsoft.com") Then
                    Dim referido As String = "https://click.linksynergy.com/deeplink?id=AlDdpr80Ueo&mid=46134&murl="

                    If Not enlace.Contains(referido) Then
                        enlace = enlace.Replace("/", "%2F")
                        enlace = enlace.Replace(":", "%3A")

                        enlace = referido + enlace
                    End If
                ElseIf enlace.Contains("gamebillet.com") Then
                    Dim referido As String = "?affiliate=64e186aa-fb0e-436f-a000-069090c06fe9"

                    If Not enlace.Contains(referido) Then
                        enlace = enlace + referido
                    End If
                End If
            End If

            Return enlace

        End Function

    End Module
End Namespace

