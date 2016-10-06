$token = 'Fails'
$headers = @{
 
  "Authorization" = "Bearer $token"
  "Content-type" = "application/json"
}

try {
        Invoke-RestMethod -Uri 'http://localhost:50699/api/values' -Headers $headers  -Method Get
         

}
catch {
        Write-Host "error"
        Write-Host $_.Exception.Response.StatusCode

        $result = $_.Exception.Response.GetResponseStream()
        $reader = New-Object System.IO.StreamReader($result)
        $reader.BaseStream.Position = 0
        $reader.DiscardBufferedData()
        $responseBody = $reader.ReadToEnd();
        #Write-Host $responseBody
        Write-Host $result

}

 