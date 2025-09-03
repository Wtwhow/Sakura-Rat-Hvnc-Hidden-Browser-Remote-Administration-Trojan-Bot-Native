﻿Module Helper

    Function SB(ByVal s As String) As Byte()
        Return Text.Encoding.Default.GetBytes(s)
    End Function

    Function BS(ByVal b As Byte()) As String
        Return Text.Encoding.Default.GetString(b)
    End Function

    Function _Size(ByVal Size As String) As String
        Try
            If (Size.ToString.Length < 4) Then
                Return (CInt(Size) & " Bytes")
            End If
            Dim str As String = String.Empty
            Dim num As Double = CDbl(Size) / 1024
            If (num < 1024) Then
                str = " KB"
            Else
                num = (num / 1024)
                If (num < 1024) Then
                    str = " MB"
                Else
                    num = (num / 1024)
                    str = " GB"
                End If
            End If
            Return (num.ToString(".0") & str)
        Catch ex As Exception
            Debug.WriteLine("_Size" + ex.Message)
        End Try
    End Function

    Function AES_Encryptor(ByVal input As Byte()) As Byte()
        Dim AES As New Security.Cryptography.RijndaelManaged
        Dim Hash As New Security.Cryptography.MD5CryptoServiceProvider
        Dim ciphertext As String = ""
        Try
            AES.Key = Hash.ComputeHash(SB(Settings.KEY))
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = input
            Return DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length)
        Catch ex As Exception
            Debug.WriteLine("AES_Encryptor" + ex.Message)
        End Try
    End Function

    Function AES_Decryptor(ByVal input As Byte(), Optional C As Client = Nothing) As Byte()
        Dim AES As New Security.Cryptography.RijndaelManaged
        Dim Hash As New Security.Cryptography.MD5CryptoServiceProvider
        Try
            AES.Key = Hash.ComputeHash(SB(Settings.KEY))
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = input
            Return DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length)
        Catch ex As Exception
            Debug.WriteLine("AES_Decryptor" + ex.Message)
            If C.IsConnected Then
                C.S.Blocked.Add(C.IP.ToString.Split(":")(0))
                C.isDisconnected()
            End If
        End Try
        Return Nothing
    End Function

    Public rand As New Random()
    Function Randomi(ByVal lenght As Integer) As String
        Dim Chr As String = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψω"
        Dim sb As New Text.StringBuilder()
        For i As Integer = 1 To lenght
            Dim idx As Integer = rand.Next(0, Chr.Length)
            sb.Append(Chr.Substring(idx, 1))
        Next
        Return sb.ToString
    End Function

End Module