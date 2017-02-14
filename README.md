# reeleezee-api-csharp
This repository contains sample code for the Reeleezee API. These samples are explained at the [Reeleezee API Documentation ](http://developer.reeleezee.nl/docs/api/) site.

**Note**: the samples are designed for educational purposes and not per se for commercial implementation.

#### Dependencies
The samples use the following external References

- [RestSharp ](http://restsharp.org/) HTTP API Client for .NET
- [Json.NET](http://www.newtonsoft.com/json) JSON Framework for .NET

These assemblies can be placed in the examples\lib folder of your project.

#### Getting Started
To run the sample programs you need to:

- Have a [Reeleezee administration](https://www.reeleezee.nl)
- Modify the settings file with your credential information and check the uri

```xml
<?xml version='1.0' encoding='utf-8'?>
<SettingsFile xmlns="http://schemas.microsoft.com/VisualStudio/2004/01/settings" CurrentProfile="(Default)" GeneratedClassNamespace="ReeleezeeAPI" GeneratedClassName="Settings">
  <Profiles />
  <Settings>
    <Setting Name="Uri" Type="System.String" Scope="Application">
      <Value Profile="(Default)">https://test.reeleezee.nl/api/v1</Value>
    </Setting>
    <Setting Name="UserName" Type="System.String" Scope="Application">
      <Value Profile="(Default)">username</Value>
    </Setting>
    <Setting Name="Password" Type="System.String" Scope="Application">
      <Value Profile="(Default)">password</Value>
    </Setting>
  </Settings>
</SettingsFile>
```

After that you're all set to run the samples.