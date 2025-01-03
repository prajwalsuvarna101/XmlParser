# Project Name: XmlParser

## Project Overview

**XmlParser** is a .NET MVC application that fetches XML data from a specified external URL, parses it, and displays the results in a tabular format on a webpage. The application allows you to dynamically retrieve XML data and presents it with relevant details such as element names and descriptions.

### Features:
- Fetch XML data from a remote API.
- Parse XML content to extract meaningful information.
- Display parsed data in an interactive table using DataTables.
- Supports secure configuration management using `appsettings.json` and environment variables.

---

## Configuration with `appsettings.json`

In order to configure the application, we use the `appsettings.json` file. This file stores the application's settings such as logging levels, allowed hosts, and external API URLs. The `appsettings.json` file is essential for defining both non-sensitive and sensitive configuration data.

### Example of `appsettings.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ApiSettings": {
    "XmlApiUrl": "{GIVE THE API!!}"
  },
  "AllowedHosts": "*"
}
