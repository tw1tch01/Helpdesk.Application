Endpoints for a simple Helpdesk application using ASP.NET Core Web API. This project is merely a presentation layer implementation of the `Helpdesk.Core` system.

## Introduction

Thank you for your interest in the `Helpdesk.WebAPI` API application. This API is documented in OpenAPI format and is an implementation of the `Helpdesk.Core` system found [here](https://github.com/tw1tch01/Helpdesk.Core). 

This document serves as an outline for some basic concepts used across the API, as well as define all resource endpoints that can be consumed to use this system.

## Data Types

### Date Times

All dates that are sent to the API must be in IS0 8601  format (YYYY-MM-DDTHH:MM:SS+-hh:mm). e.g. 2020-08-03T14:15:44-05:00. Dates and times that the API replies with will use this format.

`Helpdesk.Core` systems operate on Coordinated Universal Time (UTC) therefore all dates are converted to this timezone.

**Important**: Dates and times that are sent without timezone information (i.e. without 'Z' or '±hh:mm') are assumed to be in Coordinated Universal Time (UTC).

For brevity, it is recommended that dates and times are sent without timezone information so that the `Helpdesk.Core` systems automatically treats them in the preferred local time.

### Identifiers

All records in the `Helpdesk.Core` use GUIDs (uuid) as primary keys.

### Enumerations

All enumerations are sent/received as string representations of its corresponding value.

## Pagination

Pagination is handled by sending through the following headers in the request,


| Request Header | Description | DataType |
|--------|-------------|------|
| `X-Page-Number` | The page number you wish to view. **NOTE**: First page will always start at `0` | `int` |
| `X-Page-Size` | The number of records you would like returned for each page. | `int` |

If you request a pagination endpoint and do not supply the above headers, the request will set these values to their corresponding default values. Each resource has its own default and maximum page sizes. If a page size greater than the maximum page size is request, the system will set it to its maximum value.
For none paged requests, use the defined `{resource}/all` endpoint.

## Cross-Origin Resource Sharing

This API features Cross-Origin Resource Sharing (CORS) implemented in compliance with W3C spec. And that allows cross-domain communication from the browser. All responses have a wildcard same-origin which makes them completely public and accessible to everyone, including any code on any site.

## Authentication

There is currently no authentication setup for this implementation of the `Helpdesk.Core` system. Will be implemented when Clients/Users are setup.
