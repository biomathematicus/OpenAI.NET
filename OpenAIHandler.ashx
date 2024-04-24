<%@ WebHandler Language="VB" class="OpenAIHandler"%>
'AutoEventWireup="false" CodeFile="OAI.asax.vb" Inherits="OAI_OAI" 
Imports System.Net
Imports System.IO
Imports System.Web

Public Class OpenAIHandler : Implements IHttpHandler

	Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
		' Retrieve the API key from the environment variable
		ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
		context.Response.ContentType = "application/json"
		Dim apiKey As String = System.Environment.GetEnvironmentVariable("OPENAI_API_KEY")

		If String.IsNullOrEmpty(apiKey) Then
			context.Response.Write("API Key is not configured properly.")
			context.Response.End()
			Return
		End If

		Dim prompt As String = context.Request("prompt")

		' Create the web request to OpenAI's API
		Dim request As HttpWebRequest = DirectCast(WebRequest.Create("https://api.openai.com/v1/chat/completions"), HttpWebRequest)
		request.Method = "POST"
		request.Headers("Authorization") = "Bearer " & apiKey
		request.ContentType = "application/json"

		' Prepare the data to send in the request with the 'model' parameter included
		Dim postData As String = "{""model"": ""gpt-4"", ""messages"": [{""role"": ""user"", ""content"": """ & prompt & """}]}"
		Using streamWriter As New StreamWriter(request.GetRequestStream())
			streamWriter.Write(postData)
			streamWriter.Flush()
			streamWriter.Close()
		End Using


		' Execute the request and obtain the response
		Try
			Dim httpResponse As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
			Using streamReader As New StreamReader(httpResponse.GetResponseStream())
				Dim result As String = streamReader.ReadToEnd()
				context.Response.ContentType = "application/json"
				context.Response.Write(result)
			End Using
		Catch ex As WebException
			If ex.Response IsNot Nothing Then
				Using stream As Stream = ex.Response.GetResponseStream()
					Using reader As New StreamReader(stream)
						Dim responseText As String = reader.ReadToEnd()
						context.Response.Write("Error: " & responseText)
					End Using
				End Using
			Else
				context.Response.Write("Error: " & ex.Message)
			End If
		End Try
	End Sub

	Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
		Get
			Return False
		End Get
	End Property
End Class
