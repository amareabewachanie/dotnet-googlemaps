# PowerShell script to add GoogleMaps configuration to appsettings.json
# This script runs after package installation

param($installPath, $toolsPath, $package, $project)

Write-Host "Dotnet.GoogleMaps: Checking appsettings.json configuration..." -ForegroundColor Cyan

$appSettingsPath = Join-Path (Split-Path $project.FullName) "appsettings.json"
$appSettingsDevelopmentPath = Join-Path (Split-Path $project.FullName) "appsettings.Development.json"
$templatePath = Join-Path $installPath "contentFiles\any\any\appsettings.GoogleMaps.template.json"

$googleMapsConfig = @"
  "GoogleMaps": {
    "ApiKey": "YOUR_API_KEY_HERE",
    "BaseUrl": "https://maps.googleapis.com",
    "TimeoutSeconds": 30,
    "Resiliency": {
      "EnableRetry": true,
      "MaxRetryAttempts": 3,
      "RetryBaseDelaySeconds": 1.0,
      "RetryMaxDelaySeconds": 30.0,
      "EnableCircuitBreaker": true,
      "CircuitBreakerFailureThreshold": 5,
      "CircuitBreakerDurationOfBreakSeconds": 30.0,
      "EnableTimeout": true,
      "TimeoutSeconds": 30,
      "RetryableStatusCodes": [ 408, 429, 500, 502, 503, 504 ]
    }
  }
"@

function Add-GoogleMapsConfig {
    param($filePath)
    
    if (Test-Path $filePath) {
        $content = Get-Content $filePath -Raw
        
        if ($content -match '"GoogleMaps"') {
            Write-Host "Dotnet.GoogleMaps: GoogleMaps section already exists in $(Split-Path $filePath -Leaf). Skipping..." -ForegroundColor Yellow
            return
        }
        
        # Try to add the configuration
        try {
            $json = $content | ConvertFrom-Json
            
            # Add GoogleMaps property
            $googleMapsObj = $googleMapsConfig | ConvertFrom-Json
            $json | Add-Member -MemberType NoteProperty -Name "GoogleMaps" -Value $googleMapsObj.GoogleMaps -Force
            
            $json | ConvertTo-Json -Depth 10 | Set-Content $filePath
            Write-Host "Dotnet.GoogleMaps: Added GoogleMaps configuration to $(Split-Path $filePath -Leaf)" -ForegroundColor Green
        }
        catch {
            Write-Host "Dotnet.GoogleMaps: Could not automatically add configuration to $(Split-Path $filePath -Leaf). Please add it manually." -ForegroundColor Yellow
            Write-Host "Template available at: $templatePath" -ForegroundColor Yellow
        }
    }
    else {
        Write-Host "Dotnet.GoogleMaps: $(Split-Path $filePath -Leaf) not found. Creating it..." -ForegroundColor Yellow
        
        $newJson = @{
            GoogleMaps = @{
                ApiKey = "YOUR_API_KEY_HERE"
                BaseUrl = "https://maps.googleapis.com"
                TimeoutSeconds = 30
                Resiliency = @{
                    EnableRetry = $true
                    MaxRetryAttempts = 3
                    RetryBaseDelaySeconds = 1.0
                    RetryMaxDelaySeconds = 30.0
                    EnableCircuitBreaker = $true
                    CircuitBreakerFailureThreshold = 5
                    CircuitBreakerDurationOfBreakSeconds = 30.0
                    EnableTimeout = $true
                    TimeoutSeconds = 30
                    RetryableStatusCodes = @(408, 429, 500, 502, 503, 504)
                }
            }
        } | ConvertTo-Json -Depth 10
        
        $newJson | Set-Content $filePath
        Write-Host "Dotnet.GoogleMaps: Created $(Split-Path $filePath -Leaf) with GoogleMaps configuration" -ForegroundColor Green
    }
}

# Add configuration to appsettings.json
Add-GoogleMapsConfig -filePath $appSettingsPath

# Optionally add to appsettings.Development.json
# Add-GoogleMapsConfig -filePath $appSettingsDevelopmentPath

Write-Host "Dotnet.GoogleMaps: Installation complete!" -ForegroundColor Green
