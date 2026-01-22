# Publishing to NuGet.org Guide

This guide will help you publish the Dotnet.GoogleMaps package to NuGet.org.

## Prerequisites

1. **NuGet.org Account**
   - Go to [NuGet.org](https://www.nuget.org)
   - Sign up or sign in
   - Verify your email address

2. **API Key**
   - Go to [NuGet.org Account Settings](https://www.nuget.org/account/apikeys)
   - Click **Create**
   - Name: `Dotnet.GoogleMaps Publishing Key`
   - Select **Select scopes** → **Push new packages and package versions**
   - Set expiration (recommended: 1 year)
   - Click **Create**
   - **Copy the API key** (you won't see it again!)

3. **Package Requirements**
   - Package ID must be unique (we're using `Dotnet.GoogleMaps`)
   - Package must build successfully
   - All required metadata must be present

## Step 1: Build the Package

```bash
cd c:\Users\OMEN\Documents\niyat\axumite\Axumite.GoogleMaps

# Clean previous builds
dotnet clean

# Build in Release mode
dotnet build -c Release

# Create the NuGet package
dotnet pack -c Release --no-build
```

The package will be created at:
```
bin\Release\Dotnet.GoogleMaps.1.0.0.nupkg
```

## Step 2: Verify the Package

Before publishing, verify the package:

```bash
# Check package contents
dotnet nuget verify bin\Release\Dotnet.GoogleMaps.1.0.0.nupkg

# Or use NuGet.exe
nuget verify -All bin\Release\Dotnet.GoogleMaps.1.0.0.nupkg
```

## Step 3: Test the Package Locally (Recommended)

Test the package locally before publishing:

```bash
# Create a local NuGet feed
mkdir C:\LocalNuGetFeed

# Add package to local feed
dotnet nuget add source C:\LocalNuGetFeed --name LocalFeed

# Copy package to local feed
copy bin\Release\Dotnet.GoogleMaps.1.0.0.nupkg C:\LocalNuGetFeed\

# Test in a sample project
dotnet new console -n TestProject
cd TestProject
dotnet add package Dotnet.GoogleMaps --source LocalFeed
dotnet build
```

## Step 4: Configure NuGet API Key

Store your API key securely:

### Option A: Using NuGet CLI (Recommended)

```bash
# Add API key to NuGet configuration
dotnet nuget add source https://api.nuget.org/v3/index.json --name nuget.org

# Store API key (Windows)
dotnet nuget update source nuget.org --username YOUR_USERNAME --password YOUR_API_KEY --store-password-in-clear-text

# Or use environment variable (more secure)
$env:NUGET_API_KEY = "YOUR_API_KEY"
```

### Option B: Using nuget.exe

```bash
# Set API key
nuget setApiKey YOUR_API_KEY -Source https://api.nuget.org/v3/index.json
```

## Step 5: Publish to NuGet.org

### Option A: Using dotnet CLI (Recommended)

```bash
# Publish the package
dotnet nuget push bin\Release\Dotnet.GoogleMaps.1.0.0.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json

# Or if API key is stored
dotnet nuget push bin\Release\Dotnet.GoogleMaps.1.0.0.nupkg --source nuget.org
```

### Option B: Using NuGet.exe

```bash
nuget push bin\Release\Dotnet.GoogleMaps.1.0.0.nupkg -ApiKey YOUR_API_KEY -Source https://api.nuget.org/v3/index.json
```

### Option C: Using NuGet.org Web Interface

1. Go to [NuGet.org Upload](https://www.nuget.org/packages/manage/upload)
2. Drag and drop your `.nupkg` file
3. Click **Submit**

## Step 6: Verify Publication

1. Go to [NuGet.org](https://www.nuget.org)
2. Search for `Dotnet.GoogleMaps`
3. Your package should appear (may take a few minutes)

## Step 7: Test Installation

Test that the package can be installed:

```bash
# Create a test project
dotnet new console -n TestInstall
cd TestInstall

# Install the package
dotnet add package Dotnet.GoogleMaps

# Verify installation
dotnet list package
```

## Updating the Package

When you need to publish an update:

1. **Update Version** in `Axumite.GoogleMaps.csproj`:
   ```xml
   <Version>1.0.1</Version>
   ```

2. **Build and Pack**:
   ```bash
   dotnet build -c Release
   dotnet pack -c Release --no-build
   ```

3. **Publish**:
   ```bash
   dotnet nuget push bin\Release\Dotnet.GoogleMaps.1.0.1.nupkg --source nuget.org
   ```

## Versioning Guidelines

Follow [Semantic Versioning](https://semver.org/):
- **MAJOR.MINOR.PATCH** (e.g., 1.0.0)
- **MAJOR**: Breaking changes
- **MINOR**: New features (backward compatible)
- **PATCH**: Bug fixes (backward compatible)

## Best Practices

1. **Always test locally** before publishing
2. **Use pre-release versions** for testing:
   ```xml
   <Version>1.0.1-alpha</Version>
   ```
3. **Include release notes** in package description
4. **Tag releases** in GitHub
5. **Update README** with each release

## Troubleshooting

### Error: "Package validation failed"

- Check that package ID is unique
- Verify all required metadata is present
- Ensure package builds successfully

### Error: "API key is invalid"

- Verify API key is correct
- Check API key hasn't expired
- Ensure API key has push permissions

### Error: "Package already exists"

- Increment version number
- Or delete the existing package (if it's a pre-release)

## Security Considerations

1. **Never commit API keys** to source control
2. **Use environment variables** or secure storage
3. **Rotate API keys** periodically
4. **Use scoped API keys** with minimal permissions

## Additional Resources

- [NuGet Package Creation](https://learn.microsoft.com/en-us/nuget/create-packages/overview-and-workflow)
- [NuGet Publishing Guide](https://learn.microsoft.com/en-us/nuget/nuget-org/publish-a-package)
- [NuGet CLI Reference](https://learn.microsoft.com/en-us/nuget/reference/cli-reference/cli-ref-push)
